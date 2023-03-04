using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointListener : MonoBehaviour {



    PlayerController player;
    CarController car;

    bool broken;

	// Use this for initialization
	void Start () {

        player = transform.root.GetChild(0).GetComponent<PlayerController>();
        car = transform.root.GetChild(1).GetChild(0).GetComponent<CarController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

   
 
    void OnJointBreak2D(Joint2D brokenJoint)
    {
        Debug.Log("Break= " + brokenJoint.gameObject.name);
        player.canTakeDamage = true;
        car.BreakParticles();
       
        if (!broken)
        {
            AchievementManger.instance.AddBrokenTransportPart();
            broken = true;
        }

    }
}
