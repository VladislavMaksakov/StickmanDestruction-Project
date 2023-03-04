using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShopItem : MonoBehaviour {

    public int id;
    public int price;
    public bool opened;
    public itemType type;
    public GameObject lockObject;
    bool increment = false;
    Image lockImage;
    Text priceText;
    public new string name;
    int playerGold;
    GameObject localItem;

    public enum itemType
    {
        Character,
        LevelObject,
        Transport,
        Level
    }


	// Use this for initialization
	void Start () {
        name = transform.GetChild(0).GetComponent<Text>().text;
        playerGold = PlayerPrefs.GetInt("Gold", 0);
        GetLockObject();
        if (lockObject)
        {
            priceText = lockObject.transform.GetChild(0).GetComponent<Text>();
            lockImage= lockObject.transform.GetChild(1).GetComponent<Image>();
            priceText.text = price.ToString();
           
           
        }


        
    }


    private void OnEnable()
    {
      
        if (PlayerPrefs.GetInt("Gold", 0) >= price)
        {
            StartIlluminate();
        }
    }

    private void OnDisable()
    {
        StopIlluminate();
    }



    // Update is called once per frame
    void Update () {
		
	}

   public void ResetName()
   {
        name = transform.GetChild(0).gameObject.name;
   }

    void GetLockObject()
    {
        if (transform.childCount >= 4)
        {
            lockObject = transform.GetChild(3).gameObject;
        }
    }


    public void SelectItem(string levelName)
    {
        ResetName();
        if (gameObject.name == "Level1")
        {
            opened = true;
            
        }

        if (opened)
        {
            
            MenuUI.instance.LoadLevel(levelName);

        }
        else
        {
            //buy
            if (PlayerPrefs.GetInt("Gold", 0) >= price)
            {

               MenuUI.instance.AskForBuy(BuyItem, this);
            }
            else
            {
                StartCoroutine(NotEnoughGold());
            }
        }

    }

    public void SelectItem(GameObject item)
    {
       
            localItem = item;
   
            if (gameObject.name == "None"|| gameObject.name =="Default")
            {
            Debug.Log("none");
                opened = true;
            }
            
            if (opened)
            {
                switch (type)
                {
                    case itemType.Transport:
                        if (id == 0)
                        {
                        PlayerPrefs.SetInt("PreviousTransportId", id);
                        GameUI.instance.CharacterSelect();

                        }
                        else
                        {
                            GameUI.instance.TransportSelect(item);
                        PlayerPrefs.SetInt("PreviousTransportId", id);
                        }
                
                    break;

                    case itemType.LevelObject:
                    if (id == 0)
                    {
                        GameUI.instance.SelectNoObject();
                    }
                    else
                    {
                        GameUI.instance.SelectObject(item);
                    }
                        break;

                    case itemType.Character:

                        GameUI.instance.CharacterSelect();
                        break;
               
            }

            }

            else
            {
                //buy
                if (PlayerPrefs.GetInt("Gold", 0) >= price)
                {
                   
                    GameUI.instance.AskForBuy(BuyItem, this);
                }
                else
                {
                    StartCoroutine(NotEnoughGold());
                }
            }
        
    }



    public IEnumerator NotEnoughGold()
    {
        if (!ProgressManager.instance.levelsFolderObject)
        {
            AudioManager.instance.PlayNotEnoughGold();
            GameUI.instance.goldText.color = Color.red;
            priceText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            GameUI.instance.goldText.color = Color.yellow;
            priceText.color = Color.yellow;
        }
        else
        {
          //  AudioManager.instance.PlayNotEnoughGold();
            MenuUI.instance.goldText.color = Color.red;
            priceText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            MenuUI.instance.goldText.color = Color.yellow;
            priceText.color = Color.yellow;
        }

    }
    
    
    public void CheckAvailability()
    {
        GetLockObject();
        if (lockObject)
        {
            if (opened)
            {
                lockObject.SetActive(false);
            }
            else
            {
                lockObject.SetActive(true);
            }
        }
    }

    public void StartIlluminate()
    {
       
        if (lockObject)
        {
            lockImage = lockObject.transform.GetChild(1).GetComponent<Image>();
            InvokeRepeating("IlluminateItem", 0, 0.05f);
        }
    }

    public void StopIlluminate()
    {
        if(lockImage)
        lockImage.color = new Color(1, 1, 1, 1);
        CancelInvoke("IlluminateItem");
    }


    void IlluminateItem()
    {
        if (lockImage)
        {
            if (lockImage.gameObject.activeInHierarchy)
            {
                float alpha = lockImage.color.a;
                if (increment)
                {
                    if (alpha < 1)
                    {
                        alpha += 0.06f;
                    }
                    else
                    {
                        increment = false;
                    }
                }
                else
                {
                    if (alpha > 0.15f)
                    {
                        alpha -= 0.06f;
                    }
                    else
                    {
                        increment = true;
                    }
                }
                Debug.Log(alpha);

                lockImage.color = new Color(1, 1, 1, alpha);
            }
        }
    }


    void BuyItem()
    {
        if (PlayerPrefs.GetInt("Gold", 0) >= price)
        {
            if (!ProgressManager.instance.levelsFolderObject)
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold", 0) - price);
                GameUI.instance.goldText.text = PlayerPrefs.GetInt("Gold", 0).ToString();
                opened = true;
             

                ProgressManager.instance.SaveData(type);

                CheckAvailability();
                GameUI.instance.askForItemBuyView.SetActive(false);
                SelectItem(localItem);
               
            }
            else
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold", 0) - price);
                MenuUI.instance.goldText.text = PlayerPrefs.GetInt("Gold", 0).ToString();
                opened = true;
                Debug.Log(ProgressManager.instance.gameObject.name);

                ProgressManager.instance.SaveData(type);
                CheckAvailability();
                MenuUI.instance.askForItemBuyView.SetActive(false);
              
            }
        }
    }


}
