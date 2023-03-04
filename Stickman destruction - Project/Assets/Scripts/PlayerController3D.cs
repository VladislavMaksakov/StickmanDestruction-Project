using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour {



    public float jumpForce;
    public float speed=1;

    public Rigidbody rig;

    public JoystickController joystick;


    void Awake()
    {
        
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Move();
	}


    void Move()
    {
      
        rig.AddForce(joystick.inputVector*speed, ForceMode.Force);
    }


    void Jump()
    {
        rig.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
    }
}
