using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameUI : MonoBehaviour
{
    
    public bool ended = false;
    public bool started = false;

    public int stageEnemiesCount = 1;

    public int victoryGoldAmount;

    public int score;
    bool objectSelectMode;

    public static int restartCount;
    CameraController camera;

    public GameObject victoryView;
    public GameObject defeatView;
    public GameObject pauseView;
    public Transform playerPosSpawn;//
    public GameObject startButton;

    public static GameUI instance;

    float previousTimeScale = 1;

    bool dangerMusic = false;

    public int stickamPoseId = 0;
    public List<GameObject> stickmanPoses;

    public GameObject victoryParticles;
    public GameObject victoryText;

    public GameObject loadingView;
    public GameObject achievementView;

    public GameObject mainUI;


    public Text victoryGoldText;
    public Text goldText;


    public Text scoreText;
    public Text bestScoreText;

    public Text timer;

    public List<Transform> objectPoints;
    public Transform selectedObjectPoint;


    public float attackBoostMulriplier = 1;

    public List<GameObject> boosters;



    public Image loadingTitleLogo;
    public Sprite ruLogo;
    public Sprite enLogo;

    public Image pushButtonLoader;
    public Button pushButton;
    public Button transportStartButton;
    public GameObject pressHoldText;

    public GameObject nextPoseButton;
    public GameObject prevPoseButton;

    public GameObject withTransportButtons;

    public GameObject singleCharactertButtons;

    public GameObject pushOutTransportButton;
    public GameObject closeObjectModeButton;

    public GameObject singleCharacter;
    public GameObject transportCharacter;

    public Button chracterSelectButton;
    public Button transportSelectButton;
    public Button objectSelectButton;

    public GameObject replayButton;
    public GameObject shareButton;


    public GameObject transportSelectView;
    public GameObject characterSelectView;
    public GameObject objectSelectView;

    public GameObject askForItemBuyView;
    public Text askForItemBuyText;
    public Button acceptItemBuyButton;

    int highScore;

    int prevTranspotId;

    [HideInInspector]
    public PlayerController player;

    [HideInInspector]
    public CarController car;


    string afterInterstitialSceneName;

    // Use this for initialization
    void Awake()
    {
        instance = this;
        //Time.timeScale = 0;

    }


    void Start()
    {


        SetLogos();
        camera = Camera.main.GetComponent<CameraController>();
        camera.followUpSpeed = 8;
        goldText.text = PlayerPrefs.GetInt("Gold", 0).ToString();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreText.text = Localisation.GetString("HighScore") + highScore;

        SetPreviousTransport();
        //if (Everyplay.IsSupported())
        //{
        //    Everyplay.ReadyForRecording += OnReadyForRecording;
        //}
        //Invoke("CheckAvailables", 0.1f);
        //PlayerPrefs.SetInt("Gold", 99999);
        // PlayerPrefs.DeleteAll();
        // Invoke("EnableToCheckAvailableItems", 0.1f);

    }

    public void OnReadyForRecording(bool enabled)
    {
        if (enabled)
        {
            // Everyplay.StartRecording();

        }
    }


    void OnEnable()
    {
        instance = this;

    }

    #region Interstitial callback handlers
    public void onInterstitialLoaded(bool isPrecache) { print("Interstitial loaded"); }
    public void onInterstitialFailedToLoad() { SceneManager.LoadScene(afterInterstitialSceneName); }
    public void onInterstitialShown() { print("Interstitial opened"); }
    public void onInterstitialClosed()
    {
        SceneManager.LoadScene(afterInterstitialSceneName);
    }
    public void onInterstitialClicked() { SceneManager.LoadScene(afterInterstitialSceneName); }
    #endregion



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeFunctions();
        }
        //SetCameraOnObjects();
    }

    public void EscapeFunctions()
    {
        if (!defeatView.activeSelf)
        {
            if (askForItemBuyView.activeSelf)
            {
                askForItemBuyView.SetActive(false);
            }

            else if (objectSelectView.activeSelf)
            {
                objectSelectView.SetActive(false);
            }
            else if (mainUI.activeSelf == false && !pauseView.activeSelf == true)
            {
                CloseObjectSelectView();
            }
            else if (transportSelectView.activeSelf)
            {
                transportSelectView.SetActive(false);
                scoreText.gameObject.SetActive(true);
                EnableStartButtons(true);
                if (singleCharactertButtons.activeSelf)
                {
                    if (!started)
                    {
                        nextPoseButton.SetActive(true);
                        prevPoseButton.SetActive(true);
                    }
                }


            }
            else if (characterSelectView.activeSelf)
            {
                characterSelectView.SetActive(false);
                EnableStartButtons(true);
                scoreText.gameObject.SetActive(true);
                if (singleCharactertButtons.activeSelf)
                {
                    if (!started)
                    {
                        nextPoseButton.SetActive(true);
                        prevPoseButton.SetActive(true);
                    }
                }

            }
            else if (startButton.activeSelf == false)
            {
                Pause();
            }
        }
    }


    void SetPreviousTransport()
    {

        prevTranspotId = PlayerPrefs.GetInt("PreviousTransportId", 0);
        if (prevTranspotId >= 0)
        {
            ProgressManager.instance.transportsFolderObject.transform.GetChild(prevTranspotId).GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            SetStickmanPose(prevTranspotId);
        }
    }

    void SetStickmanPose(int poseId)
    {
        poseId *= -1;
        if (singleCharacter.activeSelf)
        {
            stickamPoseId = poseId;
            stickmanPoses[0].SetActive(false);

            stickmanPoses[poseId].SetActive(true);

            singleCharacter = stickmanPoses[poseId];
            camera.player = singleCharacter.transform.GetChild(0).gameObject;
        }
    }


    public void SetScore(int newScore)
    {

        scoreText.text = Localisation.GetString("Score:") + newScore;

    }

    public void CheckHighScore(int endScore)
    {
        if (endScore > highScore)
        {
            highScore = endScore;
            bestScoreText.text = Localisation.GetString("HighScore") + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    void HideAchievements()
    {
        achievementView.SetActive(false);
    }


    void EnableStartButtons(bool enable)
    {
        pushButton.gameObject.SetActive(enable);
        transportStartButton.gameObject.SetActive(enable);
        pressHoldText.SetActive(enable);

    }

    public void StartSingleCharacter(float power)
    {
        started = true;
        camera.followUpSpeed = 30;
        player.canTakeDamage = true;
        PushPlayer(power * player.jumpForce);
        StartRecording();
        chracterSelectButton.interactable = false;
        transportSelectButton.interactable = false;
        objectSelectButton.interactable = false;
        nextPoseButton.SetActive(false);
        prevPoseButton.SetActive(false);
        HideAchievements();
        InvokeRepeating("CheckForGameOver", 4f, 1f);

    }


    public void StartWithTransportCharacter(float power)
    {
        started = true;
        camera.followUpSpeed = 30;
        car.SetSpeed(power * 700f);
        StartRecording();
        pushOutTransportButton.SetActive(true);
        chracterSelectButton.interactable = false;
        transportSelectButton.interactable = false;
        objectSelectButton.interactable = false;
        nextPoseButton.SetActive(false);
        prevPoseButton.SetActive(false);
        HideAchievements();
        InvokeRepeating("CheckForGameOver", 4f, 1f);
    }

    void StartRecording()
    {
        //if (Everyplay.IsReadyForRecording())
        //{

        //    Everyplay.StartRecording();
        //}
    }

    public void PlayReplay()
    {
        //Everyplay.PlayLastRecording();
    }

    public void ShareReplay()
    {
        //Everyplay.ShowSharingModal();
    }

    void CheckForGameOver()
    {
        if (player.rig.velocity.magnitude <= 0.1f)
        {
            StartCoroutine(EndTimer());
            ended = true;
            //if (Everyplay.IsRecording())
            //{
            //    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
            //    Everyplay.StopRecording();

            //    shareButton.SetActive(true);
            //    replayButton.SetActive(true);
            //}

            Invoke("ShowGameOver", 3f);
            CancelInvoke("CheckForGameOver");
        }
    }

    IEnumerator EndTimer()
    {
        timer.gameObject.SetActive(true);
        timer.text = "3";
        yield return new WaitForSeconds(1f);
        timer.text = "2";
        yield return new WaitForSeconds(1f);
        timer.text = "1";
        yield return new WaitForSeconds(0.9f);
        timer.gameObject.SetActive(false);


    }

    void ShowGameOver()
    {
        AchievementManger.instance.ShowEndResult();
        defeatView.SetActive(true);
        achievementView.SetActive(true);
        mainUI.SetActive(false);
    }



    void SetLogos()
    {
        if (Localisation.CurrentLanguage == Languages.Russian)
        {

            loadingTitleLogo.sprite = ruLogo;
        }
        else
        {

            loadingTitleLogo.sprite = enLogo;
        }

    }

    bool pushed = false;
    public void PushPlayer(float power)
    {
        if (!pushed && !ended)
        {
            player.EnablePhysics();
            player.Jump(power);
            Invoke("ActivateFeet", 1f);
            pushButton.interactable = false;
            pushed = true;
        }

    }


    void ActivateFeet()
    {
        player.ActivateFeetDamageTakers();
    }


    public void BeginLevel()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
    }




    public void OpenObjectsSelectionMode()
    {
        characterSelectView.SetActive(false);
        transportSelectView.SetActive(false);
        askForItemBuyView.SetActive(false);
        objectSelectMode = true;
        mainUI.SetActive(false);
        closeObjectModeButton.SetActive(true);
        SetCameraOnObjects();
    }

    GameObject beforeObjectsPlayer;

    void SetCameraOnObjects()
    {
        ActivateObjectPoints(true);
        float koef = camera.GetComponent<Camera>().orthographicSize / camera.transform.GetChild(0).localScale.x;
        Vector3 centerPosition;
        centerPosition = (objectPoints[0].position + objectPoints[objectPoints.Count - 1].position) / 2;
        GameObject centerPoint = new GameObject("CenterOfObjects");
        centerPoint.transform.position = centerPosition;
        beforeObjectsPlayer = camera.player;
        camera.player = centerPoint;
        if (SceneManager.GetActiveScene().name == "Level 5" || SceneManager.GetActiveScene().name == "Level 4")
        {
            camera.GetComponent<Camera>().orthographicSize = Vector3.Distance(objectPoints[0].position, objectPoints[objectPoints.Count - 1].position) / 0.8f;
        }
        else
        {
            camera.GetComponent<Camera>().orthographicSize = Vector3.Distance(objectPoints[0].position, objectPoints[objectPoints.Count - 1].position) / 2.25f;
        }


        camera.transform.GetChild(0).localScale = new Vector3(camera.GetComponent<Camera>().orthographicSize / koef, camera.GetComponent<Camera>().orthographicSize / koef);
    }

    void ActivateObjectPoints(bool active)
    {
        foreach (Transform point in objectPoints)
        {
            point.GetComponent<SpriteRenderer>().enabled = active;
            point.gameObject.SetActive(true);
        }
    }


    public void SelectObject(GameObject mapObject)
    {


        foreach (Transform child in selectedObjectPoint)
        {
            Destroy(child.gameObject);
        }
        GameObject createdObject = Instantiate(mapObject, selectedObjectPoint, true);
        Debug.Log("Created object" + createdObject.name);
        createdObject.transform.localPosition = Vector3.zero;
        createdObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        objectSelectView.SetActive(false);

    }


    public void SelectNoObject()
    {
        foreach (Transform child in selectedObjectPoint)
        {
            Destroy(child.gameObject);
        }
        objectSelectView.SetActive(false);
    }

    public void CloseObjectSelectView()
    {
        objectSelectMode = false;
        characterSelectView.SetActive(false);
        transportSelectView.SetActive(false);
        objectSelectView.SetActive(false);
        ActivateObjectPoints(false);
        mainUI.SetActive(true);
        EnableStartButtons(true);
        scoreText.gameObject.SetActive(true);
        if (singleCharactertButtons.activeSelf)
        {
            nextPoseButton.SetActive(true);
            prevPoseButton.SetActive(true);
        }
        camera.player = beforeObjectsPlayer;
        camera.GetComponent<Camera>().orthographicSize = 16;
        camera.transform.GetChild(0).localScale = new Vector3(5, 5, 5);
        closeObjectModeButton.SetActive(false);
    }

    public void OpenTransportSelect()
    {
        if (transportSelectView.activeSelf)
        {
            transportSelectView.SetActive(false);
            EnableStartButtons(true);
            scoreText.gameObject.SetActive(true);
            nextPoseButton.SetActive(true);
            prevPoseButton.SetActive(true);
        }
        else
        {
            transportSelectView.SetActive(true);
            nextPoseButton.SetActive(false);
            prevPoseButton.SetActive(false);
            characterSelectView.SetActive(false);
            scoreText.gameObject.SetActive(false);
            EnableStartButtons(false);
        }
    }



    public void OpenCharacterSelect()
    {
        if (characterSelectView.activeSelf)
        {
            characterSelectView.SetActive(false);
            scoreText.gameObject.SetActive(true);
            EnableStartButtons(true);
            nextPoseButton.SetActive(true);
            prevPoseButton.SetActive(true);
        }
        else
        {
            transportSelectView.SetActive(false);
            characterSelectView.SetActive(true);
            scoreText.gameObject.SetActive(false);
            EnableStartButtons(false);
            nextPoseButton.SetActive(false);
            prevPoseButton.SetActive(false);
        }
    }


    public void CharacterSelect()
    {
        characterSelectView.SetActive(false);
        transportSelectView.SetActive(false);
        EnableStartButtons(true);
        scoreText.gameObject.SetActive(true);
        nextPoseButton.SetActive(true);
        prevPoseButton.SetActive(true);
        if (!singleCharacter.activeSelf)
        {
            singleCharacter.SetActive(true);
            singleCharactertButtons.SetActive(true);
            if (transportCharacter)
            {
                transportCharacter.SetActive(false);
            }
            withTransportButtons.SetActive(false);

            PlayerPrefs.SetInt("PreviousTransportId", stickamPoseId * -1);
            player = singleCharacter.GetComponent<PlayerController>();
            camera.player = player.transform.GetChild(0).gameObject;
            nextPoseButton.SetActive(true);
            prevPoseButton.SetActive(true);
            scoreText.gameObject.SetActive(true);
        }

    }

    public void TransportSelect(GameObject transport)
    {
        transportSelectView.SetActive(false);
        EnableStartButtons(true);
        if (transportCharacter)
            transportCharacter.SetActive(false);

        transportCharacter = Instantiate(transport);
        transportCharacter.SetActive(true);
        //my code
        transportCharacter.transform.position = playerPosSpawn.position;
        //
        withTransportButtons.SetActive(true);
        singleCharacter.SetActive(false);
        singleCharactertButtons.SetActive(false);
        player = transportCharacter.transform.GetChild(0).GetComponent<PlayerController>();
        camera.player = player.transform.GetChild(0).gameObject;

        nextPoseButton.SetActive(false);
        prevPoseButton.SetActive(false);
        scoreText.gameObject.SetActive(true);

    }



    public void SetNextPose()
    {
        if (singleCharacter.activeSelf)
        {
            stickmanPoses[stickamPoseId].SetActive(false);

            if (stickamPoseId < stickmanPoses.Count - 1)
            {

                stickamPoseId++;

            }
            else
            {
                stickamPoseId = 0;
            }

            stickmanPoses[stickamPoseId].SetActive(true);
            PlayerPrefs.SetInt("PreviousTransportId", stickamPoseId * -1);
            singleCharacter = stickmanPoses[stickamPoseId];
            camera.player = singleCharacter.transform.GetChild(0).gameObject;
        }
    }



    public void SetPrevPose()
    {
        if (singleCharacter.activeSelf)
        {
            stickmanPoses[stickamPoseId].SetActive(false);

            if (stickamPoseId > 0)
            {

                stickamPoseId--;

            }
            else
            {
                stickamPoseId = stickmanPoses.Count - 1;
            }

            stickmanPoses[stickamPoseId].SetActive(true);
            PlayerPrefs.SetInt("PreviousTransportId", stickamPoseId * -1);
            singleCharacter = stickmanPoses[stickamPoseId];
            camera.player = singleCharacter.transform.GetChild(0).gameObject;
        }
    }


    public void Win()
    {
        if (!ended)
        {
            Debug.Log("Victory");
            ended = true;
            // Invoke("StopTime", 1f);

            if (victoryGoldAmount > 0)
            {
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold", 0) + victoryGoldAmount);
                victoryGoldText.text = "+ " + victoryGoldAmount + " Gold";
            }


            victoryParticles.SetActive(true);
            victoryText.SetActive(true);
            player.VictoryActivation();
            Camera.main.GetComponent<CameraController>().enabled = false;
            Camera.main.transform.position = new Vector3(0, 0, -10);
            Invoke("DissableAllStickmans", 8f);
            Invoke("ActivateVictory", 8f);

            if (SceneManager.GetActiveScene().name.Contains("Single"))
            {
                Debug.Log("Next Level Added");
                if ((PlayerPrefs.GetInt("LastSingleOpen", 0) + 1) < 10)
                    PlayerPrefs.SetInt("LastSingleOpen", PlayerPrefs.GetInt("LastSingleOpen", 0) + 1);
            }
            else if (SceneManager.GetActiveScene().name.Contains("Duo"))
            {
                if ((PlayerPrefs.GetInt("LastDuoOpen", 0) + 1) < 5)
                    PlayerPrefs.SetInt("LastDuoOpen", PlayerPrefs.GetInt("LastDuoOpen", 0) + 1);
            }
        }
    }


    void ActivateVictory()
    {
        victoryView.SetActive(true);
        victoryText.SetActive(false);
    }


    public void DecreaseEnemiesLeft()
    {
        stageEnemiesCount--;
        if (stageEnemiesCount <= 0)
        {
            Win();
        }
    }

    public void Lose()
    {

        if (!ended)
        {
            ended = true;
            Invoke("StopTime", 1f);
            defeatView.SetActive(true);
        }
    }



    void StopTime()
    {
        Time.timeScale = 0;
    }

    public void Pause()
    {
        if (!ended)
        {
            if (Time.timeScale == 0)
            {
                pauseView.SetActive(false);
                //if (Everyplay.IsRecordingSupported()&&started)
                //{
                //    Everyplay.ResumeRecording();
                //}
                if (started)
                {
                    achievementView.SetActive(false);
                }
                if (!objectSelectMode)
                {

                    mainUI.SetActive(true);
                    EnableStartButtons(true);
                    scoreText.gameObject.SetActive(true);
                    if (singleCharactertButtons.activeSelf)
                    {
                        if (!started)
                        {
                            prevPoseButton.SetActive(true);
                            nextPoseButton.SetActive(true);
                        }

                    }
                }
                if (dangerMusic)
                {
                    if (PlayerPrefs.GetInt("MusicMute", 0) == 0)
                        AudioManager.instance.DangerMusic();
                }
                else
                {
                    if (PlayerPrefs.GetInt("MusicMute", 0) == 0)
                        AudioManager.instance.MainMusic();
                }

                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;

            }
            else
            {
                pauseView.SetActive(true);
                achievementView.SetActive(true);
                mainUI.SetActive(false);
                //if (Everyplay.IsRecordingSupported() && started)
                //{
                //    Everyplay.PauseRecording();
                //}
                if (transportSelectView.activeSelf)
                {
                    transportSelectView.SetActive(false);
                }
                if (characterSelectView.activeSelf)
                {
                    characterSelectView.SetActive(false);
                }
                if (objectSelectView.activeSelf)
                {
                    objectSelectView.SetActive(false);
                }

                if (AudioManager.instance.mainAudio.clip == AudioManager.instance.dangerSong)
                {
                    dangerMusic = true;
                }
                if (PlayerPrefs.GetInt("MusicMute", 0) == 0)
                    AudioManager.instance.PauseMusic();
                Time.timeScale = 0;

                // Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
        }
    }



    public void Restart()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        if (pauseView.activeSelf == true)
        {

            loadingView.SetActive(true);
            //if (Everyplay.IsSupported())
            //{
            //    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
            //    Everyplay.StopRecording();
            //}

            Invoke("ReloadLevel", 1f);
        }
        else
        {

            loadingView.SetActive(true);
            //if (Everyplay.IsSupported())
            //{
            //    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
            //    Everyplay.StopRecording();
            //}

            Invoke("ReloadLevel", 2f);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        restartCount++;

       
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        if (pauseView.activeSelf == true)
        {
            loadingView.SetActive(true);
            //if (Everyplay.IsSupported())
            //{
            //    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
            //    Everyplay.StopRecording();
            //}
            SceneManager.LoadScene("Menu");
        }
        else
        {
            /*
            if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            {
                loadingView.SetActive(true);
                afterInterstitialSceneName = "Menu";
                if (Everyplay.IsSupported())
                {
                    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
                    Everyplay.StopRecording();
                }
                Invoke("ShowInterstitial", 0.5f);

            }
            else
            {
            */
            loadingView.SetActive(true);
            //if (Everyplay.IsSupported())
            //{
            //    Everyplay.SetMetadata("Level", SceneManager.GetActiveScene().name);
            //    Everyplay.StopRecording();
            //}
            SceneManager.LoadScene("Menu");
            //}
        }
    }

    public void SetGold(int goldToAdd)
    {
        int gold = PlayerPrefs.GetInt("Gold", 0) + goldToAdd;
        goldText.text = gold.ToString();
        PlayerPrefs.SetInt("Gold", gold);

    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        string sceneName;

        if (SceneManager.GetActiveScene().name.Contains("Single"))
        {
            sceneName = "Single" + " " + PlayerPrefs.GetInt("LastSingleOpen", 0);
        }
        else if (SceneManager.GetActiveScene().name.Contains("Duo"))
        {
            sceneName = "Duo" + " " + PlayerPrefs.GetInt("LastDuoOpen", 0);
        }
        else
        {
            sceneName = SceneManager.GetActiveScene().name;
        }


        loadingView.SetActive(true);
        SceneManager.LoadScene(sceneName);


    }



    public void SetVictoryText(string text)
    {
        victoryText.GetComponent<Text>().text = text;
    }


    public void AskForBuy(UnityEngine.Events.UnityAction buyItemFunction, ShopItem item)
    {
        acceptItemBuyButton.onClick.RemoveAllListeners();
        acceptItemBuyButton.onClick.AddListener(buyItemFunction);
        Debug.Log(item.gameObject.name);
        askForItemBuyText.text = Localisation.GetString("Do you wanna buy") + " ''" + Localisation.GetString(item.gameObject.name) + "'' " + Localisation.GetString("for") + " " + item.price + " " + Localisation.GetString("gold") + "?";
        askForItemBuyView.SetActive(true);

    }


    //no
    public void CancelItemBuy()
    {
        askForItemBuyView.SetActive(false);

    }


}
