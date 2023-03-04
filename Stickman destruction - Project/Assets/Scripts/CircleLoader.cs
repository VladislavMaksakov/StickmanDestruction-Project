using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleLoader : MonoBehaviour {

    public Image loaderImage;
    public GameObject pressHoldText;

	// Use this for initialization
	void Start () {
        if (GetComponent<StartButton>())
        {
            GetComponent<StartButton>().loader = loaderImage;
            GetComponent<StartButton>().pressHoldText = pressHoldText;
        }
        else
        {
            if (GetComponent<SpeedUpButton>())
            {
                GetComponent<SpeedUpButton>().loader = loaderImage;

                GetComponent<SpeedUpButton>().pressHoldText = pressHoldText;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
