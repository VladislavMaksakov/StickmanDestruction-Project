using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailablesCount : MonoBehaviour {


    public ShopItem.itemType type;
    Transform parent;
    public int count;
    Image image;
    Text countText;

    // Use this for initialization
    void Start () {

        parent = GetFolder();
        image = GetComponent<Image>();
        countText = transform.GetChild(0).GetComponent<Text>();
        CheckAvailability();

    }
    
	
    Transform GetFolder()
    {
        Transform folder=null;
        switch (type)
        {
            case ShopItem.itemType.Transport:
                folder = ProgressManager.instance.transportsFolderObject.transform;
                break;

            case ShopItem.itemType.LevelObject:
                folder = ProgressManager.instance.levelObjectsFolderObject.transform;
                break;

            case ShopItem.itemType.Character:
                folder = ProgressManager.instance.characterFolderObject.transform;

                break;

        }
        return folder;
    }


   public void CheckAvailability()
    {
        int gold = PlayerPrefs.GetInt("Gold", 0);
        count = 0;
        image.enabled = false;
        countText.text = "";
        switch (type)
        {
            case ShopItem.itemType.Transport:
                
                foreach (ShopItem item in ProgressManager.instance.transportItems)
                {
                    if (gold >= item.price && !item.opened && item.id != 0)
                    {
                        count++;
                     //   item.StartIlluminate();
                        image.enabled = true;
                        countText.text = count.ToString();
                    }
                    else
                    {
                        item.StopIlluminate();
                    }
                }
                break;

            case ShopItem.itemType.LevelObject:
                foreach (ShopItem item in ProgressManager.instance.levelObjectsItems)
                {
                    if (gold >= item.price && !item.opened && item.id != 0)
                    {
                        count++;
                      //  item.StartIlluminate();
                        image.enabled = true;
                        countText.text = count.ToString();
                    }
                    else
                    {
                        item.StopIlluminate();
                    }
                }
                break;

            case ShopItem.itemType.Character:
                foreach (ShopItem item in ProgressManager.instance.characterItems)
                {
                    if (gold >= item.price && !item.opened)
                    {
                        count++;
                      //  item.StartIlluminate();
                        image.enabled = true;
                        countText.text = count.ToString();
                    }
                    else
                    {
                        item.StopIlluminate();
                    }
                }

                break;

        }
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
