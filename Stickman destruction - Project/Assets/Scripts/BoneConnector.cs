using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneConnector : MonoBehaviour {


    public Transform connectToBone;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (connectToBone != null)
        {
            transform.localPosition = connectToBone.position;
            transform.localRotation = connectToBone.rotation;
        }
    }
}
