using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Online : MonoBehaviour {


    public Text playerIdText;
    public Text startText;
    public GameObject searchingView;
    public GameObject errorView;
    public GameObject lostConnectionView;
    public GameObject startView;
    public Text dotsText;
    bool search = false;
    public Enemy enemy;

    void Awake()
    {
    
    }

    // Use this for initialization
    void Start () {
      
        int id= Random.Range(1, 99999);
        playerIdText.text = "#" + id;
        startText.text = Localisation.GetString("Player")+ " VS " + Localisation.GetString("Player")+" #" + id;
        StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                StartCoroutine(Searching());
                StartCoroutine(EndSearch());
                InvokeRepeating("RepeatingCheckConnection", 5f, 5f);
            }
            else
            {
                errorView.SetActive(true);
            }
        }
        ));
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void RepeatingCheckConnection()
    {
        StartCoroutine(checkInternetConnection((isConnected) =>
        {
            if (!isConnected)
            {
                Time.timeScale = 0;
                startView.SetActive(true);
                lostConnectionView.SetActive(true);
            }
        }));

        }

    IEnumerator Searching()
    {
        search = true;
        int i = 0;
        dotsText.text = "";
        while (search)
        {
           
            if (i == 0)
            {
                dotsText.text = "";
            }
            else
            {
                dotsText.text += ".";
            }
            i++;
            if (i == 4)
            {
                i = 0;
            }
           
            yield return new WaitForSecondsRealtime(0.5f);
        }
       
        
    }

    IEnumerator EndSearch()
    {
        yield return new WaitForSecondsRealtime(Random.Range(2f, 6f));
        Debug.Log("End Search");
        searchingView.SetActive(false);
        search = false;
        StopCoroutine(Searching());
        StopCoroutine(EndSearch());
        
    }


    IEnumerator checkInternetConnection(System.Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
         
        }
        else
        {
            action(true);
          
        }
    }

}
