using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public WheelJoint2D wheelRight;
    public WheelJoint2D wheelLeft;
    public ParticleSystem detailsPS;

    public bool boosted;

    bool set;
    void Awake() {
      
    }

    void OnEnable()
    {
        GameUI.instance.car = this;
    }

    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void BreakParticles()
    {
        detailsPS.Stop();
        detailsPS.Play();
    }

    public void SetSpeed(float speed)
    {
        if (!set)
        {
            set = true;
            JointMotor2D localMotor = wheelLeft.motor;
            localMotor.motorSpeed = speed;
            wheelLeft.motor = localMotor;
            ////////////////////////////
            localMotor = wheelRight.motor;
            localMotor.motorSpeed = speed;
            wheelRight.motor = localMotor;
        }

    }

}
