using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        if (!GameUI.instance.started)
        {

            GameUI.instance.selectedObjectPoint = transform.root;
            GameUI.instance.objectSelectView.SetActive(true);
        }
    }
}
