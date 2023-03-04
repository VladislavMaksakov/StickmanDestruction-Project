using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    public Sprite ruLogo;
    public Sprite enLogo;

    public SpriteRenderer logo;


    // Use this for initialization
    void Start () {
        Localisation.DetectLanguage();
        if (Localisation.CurrentLanguage == Languages.Russian)
        {
            logo.sprite = ruLogo;
        }
        else
        {
            logo.sprite = enLogo;
        }
        StartCoroutine(ShowLogo());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ShowLogo()
    {
        for (int i = 0; i < 255; i++)
        {
            logo.color += new Color32(0,0,0,1);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Menu");
    }

    
}
