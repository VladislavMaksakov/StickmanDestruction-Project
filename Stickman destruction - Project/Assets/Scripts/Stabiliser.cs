using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabiliser : MonoBehaviour {

    public float defaultAngle;

    public int stabilisationSpeed;
    Rigidbody2D rig;

    public Transform forcePoint;
    Vector3 forcePosition;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        forcePosition = forcePoint.position;
	}
	
	// Update is called once per frame
	void Update () {
        Stabilise();
    }


    void Stabilise()
    {
        Debug.Log(transform.localRotation.eulerAngles.z);
        if (transform.localRotation.eulerAngles.z >= defaultAngle+5)
        {
            rig.AddForceAtPosition(new Vector2(0, stabilisationSpeed),forcePosition,ForceMode2D.Force);
        }
        else if (transform.localRotation.eulerAngles.z < defaultAngle-5)
        {
            rig.AddForceAtPosition(new Vector2(0, -stabilisationSpeed), forcePosition, ForceMode2D.Force);
        }
        
    }
}
