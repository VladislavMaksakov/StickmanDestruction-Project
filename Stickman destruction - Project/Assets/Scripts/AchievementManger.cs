using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementManger : MonoBehaviour {


    public static AchievementManger instance;


    public int score;
    public int gold;

    [Space]
    [Header("End Results")]
    public int damageTaken;
    public int airScore;
    public int brokenBones;
    public int brokenTransport;
    public int headPunches;
   [Space]

    public Text goldEarned;
    public Text damageTakenText;
    public Text headPunchesText;
    public Text airScoreText;
    public Text brokenBonesText;
    public Text brokenTransportText;

    [Space]
    [Header("Achievements")]
    
    public Text achievementText;
    public Text achievementRewardText;
    int achievementTaskNumber;
    int achievementGoldReward;
    int achievementTaskScore;

    // Use this for initialization
    void Start () {
        instance = this;
        //PlayerPrefs.DeleteKey("Achievement");
        LoadAchievement();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadAchievement()
    {
        achievementTaskNumber = PlayerPrefs.GetInt("Achievement", 1);
       // Debug.Log(" achievementTaskNumber "+achievementTaskNumber);
        achievementGoldReward = achievementTaskNumber * 100;
        achievementTaskScore = achievementTaskNumber * 500;
        achievementText.text = Localisation.GetString("Get ") + achievementTaskScore + Localisation.GetString(" score in one Game.");
        achievementRewardText.text = achievementGoldReward.ToString();

    }

    int CheckAchievement(int endScore)
    {
        int goldReward = 0;
        if (endScore >= achievementTaskScore)
        {
            Debug.Log("AchievementComplete");
            goldReward = achievementGoldReward;
            PlayerPrefs.SetInt("Achievement", achievementTaskNumber+1);
            achievementText.color = Color.green;
            achievementText.text = Localisation.GetString("Achievement completed");
           // LoadAchievement();
        }
        return goldReward;
    }

    public void AddDamageTaken(int damageToAdd)
    {
        damageTaken += damageToAdd;
        CalculateScore();
    }

    public void AddAirScore(int airScoreToAdd)
    {
        airScore += airScoreToAdd;
        CalculateScore();
    }

    public void AddHeadPunches()
    {
        headPunches++;
        CalculateScore();
    }

    public void AddBrokenBone()
    {
        brokenBones++; 
        CalculateScore();
    }

    public void AddBrokenTransportPart()
    {
        brokenTransport++;
        CalculateScore();
    }

    public void ShowEndResult()
    {
        int achievementGold=0;
        CalculateScore();
        damageTakenText.text = damageTaken.ToString();
        headPunchesText.text = headPunches.ToString();
       // airScoreText.text = airScore.ToString();
        brokenBonesText.text = brokenBones.ToString();
        brokenTransportText.text = brokenTransport.ToString();
        achievementGold= CheckAchievement(score);
        goldEarned.text = (gold+achievementGold).ToString();
        gold += achievementGold;
        //Everyplay.SetMetadata("Score", score);
        GameUI.instance.SetGold(gold);
        GameUI.instance.CheckHighScore(score);
    }

    void CalculateScore()
    {
        score = damageTaken + (headPunches*200) + (brokenBones * 25) + (brokenTransport * 45); 
        gold =(int) (20 + (score / 15.5f));
        goldEarned.text = gold.ToString();
        GameUI.instance.SetScore(score);
     
       // Debug.Log("Gold earned "+gold); ;
    }
}
