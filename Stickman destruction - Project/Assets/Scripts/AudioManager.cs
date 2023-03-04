using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {



    public AudioSource playerDamageAudio;
    public AudioSource enemyDamageAudio;

    public AudioSource mainAudio;

    public AudioSource explosionAudio;

    public AudioSource notEnoughGold;

    [Space]
    public AudioClip headPunch;
    public AudioClip bodyPunch;
    public AudioClip enemyBodyPunch;

    [Space]
    public AudioClip mainSong;
    public AudioClip dangerSong;
    public AudioClip pauseSong;

    public static AudioManager instance;

    // Use this for initialization
    void Start () {
        instance = this;
	}


    void OnEnable()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update () {
		
	}


    public void PlayExplosionSound()
    {
        explosionAudio.Stop();
        explosionAudio.Play();
    }


   public void DamageHead(string stickman)
    {
        if (stickman == "Player")
        {
            playerDamageAudio.clip = headPunch;
            playerDamageAudio.Play();
        }
        else
        {
            enemyDamageAudio.clip = headPunch;
            enemyDamageAudio.Play();
        }
    
    }

   public void DamageBody(string stickman)
    {
        if (stickman == "Player")
        {
            playerDamageAudio.clip = bodyPunch;
            playerDamageAudio.Play();
        }
        else
        {
            enemyDamageAudio.clip = enemyBodyPunch;
            enemyDamageAudio.Play();
        }

    }




    public void PauseMusic()
    {
        mainAudio.Stop();
        mainAudio.clip = pauseSong;
        mainAudio.Play();

    }

    public void PlayNotEnoughGold()
    {
        notEnoughGold.Stop();
        notEnoughGold.Play();
    }

    public void MainMusic()
    {
        mainAudio.Stop();
        mainAudio.clip = mainSong;
        mainAudio.Play();

    }

    public void DangerMusic()
    {
        if (mainAudio.clip != dangerSong)
        {
            mainAudio.Stop();
            mainAudio.clip = dangerSong;
            mainAudio.Play();
        }
    }
}
