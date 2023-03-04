using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUI : MonoBehaviour
{


    public static MenuUI instance;

    public GameObject loading;
    public GameObject settings;
    public GameObject boosters;

    public GameObject menu;

    public GameObject askForExitView;
    public GameObject askForItemBuyView;
    public GameObject askForDelete;
    public GameObject askForReward;
    public GameObject selectLevelView;

    public GameObject rewardedButton;

    public Image SoundSettings;
    public Image MusicSettings;

    public Sprite soundOn;
    public Sprite soundOff;

    public Button acceptItemBuyButton;




    public Image joystickImage;

    public Sprite joystickLeft;
    public Sprite joystickRight;

    public Text askForItemBuyText;

    public Text healsCount;
    public Text attackCount;
    public Text oneshotCount;


    public Image titleLogo;
    public Image loadingTitleLogo;
    public Sprite ruLogo;
    public Sprite enLogo;

    public Text goldText;
    public Text healPriceText;
    public Text attackPriceText;
    public Text oneshotPriceText;
    int gold;
    public int healPrice;
    public int attackPrice;
    public int oneShotPrice;

    public AudioSource failAudio;
    Color32 colorBuff;

    [HideInInspector]
    public string afterInterstitialSceneName;

    // Use this for initialization
    void Start()
    {


        GameUI.restartCount = 0;
        CheckMusicSettings();
        CheckJoystick();
        // healsCount.text= ""+ PlayerPrefs.GetInt("HealBoost", 0);
        //oneshotCount.text = "" + PlayerPrefs.GetInt("KillBoost", 0);
        // attackCount.text = "" + PlayerPrefs.GetInt("AttackBoost", 0);
        SetLogos();
        var pm = FindObjectOfType<ProgressManager>();
        askForDelete.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(pm.ResetProgress);
        gold = PlayerPrefs.GetInt("Gold", 999);
        SetGold();
        colorBuff = goldText.color;
        instance = this;
        PlayerPrefs.SetInt("PreviousTransportId", 0);
        //CheatGold();




        int musicSetting = PlayerPrefs.GetInt("MusicSettings");
        if (musicSetting == 0)
        {
            AudioListener.pause = true;
            MusicSettings.sprite = soundOff;
        }
        else
        {
            AudioListener.pause = false;
            MusicSettings.sprite = soundOn;
        }
        PlayerPrefs.Save();
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (boosters.activeSelf)
            {
                // CloseBoosters();
            }
            else if (settings.activeSelf)
            {
                CloseSettings();
            }
            else if (askForDelete.activeSelf)
            {
                DontDelete();
            }
            else if (askForExitView.activeInHierarchy)
            {
                DontExit();
            }
            else
            {
                AskForExit();

            }
        }
    }


    void CheatGold()
    {
        gold = 999;
        SetGold();
    }

    void SetGold()
    {
        goldText.text = gold + " G";
        PlayerPrefs.SetInt("Gold", gold);
    }

    void SetLogos()
    {
        if (Localisation.CurrentLanguage == Languages.Russian)
        {
            titleLogo.sprite = ruLogo;
            loadingTitleLogo.sprite = ruLogo;
        }
        else
        {
            titleLogo.sprite = enLogo;
            loadingTitleLogo.sprite = enLogo;
        }

    }

    public void AskForExit()
    {
        askForExitView.SetActive(true);
        menu.SetActive(false);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DontExit()
    {
        askForExitView.SetActive(false);
        menu.SetActive(true);
    }


    void CheckMusicSettings()
    {
        if (PlayerPrefs.GetInt("MusicMute", 0) == 0)
        {
            AudioListener.pause = false;
            MusicSettings.sprite = soundOn;
        }
        else if (PlayerPrefs.GetInt("MusicMute", 0) == 1)
        {
            AudioListener.pause = true;
            MusicSettings.sprite = soundOff;
        }
    }

    public void SwitchMusic()
    {
        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
            MusicSettings.sprite = soundOff;
            PlayerPrefs.SetInt("MusicSettings", 0);
        }
        else
        {
            AudioListener.pause = false;
            MusicSettings.sprite = soundOn;
            PlayerPrefs.SetInt("MusicSettings", 1);
        }
    }


    public void AskForBuy(UnityEngine.Events.UnityAction buyItemFunction, ShopItem item)
    {
        acceptItemBuyButton.onClick.RemoveAllListeners();
        acceptItemBuyButton.onClick.AddListener(buyItemFunction);

        askForItemBuyText.text = Localisation.GetString("Do you wanna open") + " " + Localisation.GetString(item.name) + " " + Localisation.GetString("for") + " " + item.price + " " + Localisation.GetString("gold") + "?";
        askForItemBuyView.SetActive(true);

    }

    public void CancelItemBuy()
    {
        askForItemBuyView.SetActive(false);

    }


    public void SwitchJoystick()
    {
        if (PlayerPrefs.GetString("Joystick", "Right") == "Right")
        {
            joystickImage.sprite = joystickLeft;
            PlayerPrefs.SetString("Joystick", "Left");
        }
        else
        {
            joystickImage.sprite = joystickRight;
            PlayerPrefs.SetString("Joystick", "Right");
        }
    }



    public void CloseLevelView()
    {
        selectLevelView.SetActive(false);
    }



    void CheckJoystick()
    {
        if (PlayerPrefs.GetString("Joystick", "Right") == "Right")
        {
            joystickImage.sprite = joystickRight;

        }
        else
        {
            joystickImage.sprite = joystickLeft;

        }
    }


    public void SwitchSounds()
    {
        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
            SoundSettings.sprite = soundOff;
        }
        else
        {
            AudioListener.pause = false;
            SoundSettings.sprite = soundOn;
        }
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Single");
    }

    public void LoadLevel(string sceneName)
    {

        loading.SetActive(true);

        /*
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            afterInterstitialSceneName = sceneName;
            Invoke("ShowInterstitial", 0.5f);
          
        }
        else
        {
        */
        SceneManager.LoadScene(sceneName);
        // }

    }





    public void AskForDelete()
    {
        askForDelete.SetActive(true);
        settings.SetActive(false);
    }

    public void DontDelete()
    {
        askForDelete.SetActive(false);
        settings.SetActive(true);
    }

    
    public void DeleteAllData()
    {
        int g = PlayerPrefs.GetInt("Gold", 0);
        int heals = PlayerPrefs.GetInt("HealBoost", 0);
        int attacks = PlayerPrefs.GetInt("AttackBoost", 0);
        int oneshoot = PlayerPrefs.GetInt("KillBoost", 0);
        PlayerPrefs.DeleteAll();


        PlayerPrefs.SetInt("Gold", g);
        PlayerPrefs.SetInt("HealBoost", heals);
        PlayerPrefs.SetInt("AttackBoost", attacks);
        PlayerPrefs.SetInt("KillBoost", oneshoot);


        askForDelete.SetActive(false);
        settings.SetActive(true);
        loading.SetActive(true);
        StartCoroutine("ReloadMenuScene", 1f);
    }

    public void OpenReplays()
    {
        //Everyplay.Show();
    }

    void ReloadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BuyHeal()
    {

        if (gold >= healPrice)
        {
            gold -= healPrice;
            SetGold();
            int heals = PlayerPrefs.GetInt("HealBoost", 0);
            heals++;
            PlayerPrefs.SetInt("HealBoost", heals);
            healsCount.text = heals.ToString();
        }
        else
        {

            goldText.color = Color.red;
            healPriceText.color = Color.red;
            failAudio.Play();
            Invoke("ResetGoldColors", 1f);
        }
    }


    public void BuyAttack()
    {
        if (gold >= attackPrice)
        {
            gold -= attackPrice;
            SetGold();
            int attacks = PlayerPrefs.GetInt("AttackBoost", 0);
            attacks++;
            PlayerPrefs.SetInt("AttackBoost", attacks);
            attackCount.text = "" + attacks;
        }
        else
        {

            goldText.color = Color.red;
            attackPriceText.color = Color.red;
            failAudio.Play();
            Invoke("ResetGoldColors", 1f);
        }
    }

    public void BuyKill()
    {
        if (gold >= oneShotPrice)
        {
            gold -= oneShotPrice;
            SetGold();
            int oneshoot = PlayerPrefs.GetInt("KillBoost", 0);
            oneshoot++;
            PlayerPrefs.SetInt("KillBoost", oneshoot);
            oneshotCount.text = oneshoot.ToString();
        }
        else
        {

            goldText.color = Color.red;
            oneshotPriceText.color = Color.red;
            failAudio.Play();
            Invoke("ResetGoldColors", 1f);
        }
    }


    public void OpenLevelSelection()
    {
        selectLevelView.SetActive(true);
    }


    void ResetGoldColors()
    {
        goldText.color = colorBuff;
        healPriceText.color = colorBuff;
        attackPriceText.color = colorBuff;
        oneshotPriceText.color = colorBuff;
    }
}
