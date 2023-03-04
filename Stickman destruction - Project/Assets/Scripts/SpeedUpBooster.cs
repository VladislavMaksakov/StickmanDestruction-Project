using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedUpBooster : MonoBehaviour
{

    public float impulsePower;
    bool boosted = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!boosted)
        {
            if (collision.transform.root.GetChild(1).GetChild(0).GetComponent<CarController>())
            {

                Debug.Log("Car");
                Debug.Log(collision.tag);
                CarController car = collision.transform.root.GetChild(1).GetChild(0).GetComponent<CarController>();
                if (car.wheelLeft != null && car.wheelRight != null)
                {
                    if (!car.boosted)
                    {
                        if (collision.tag == "Car")
                        {
                            JointMotor2D motor = car.wheelLeft.motor;
                            motor.motorSpeed *= 2f;
                            car.wheelLeft.motor = motor;
                            car.wheelRight.motor = motor;
                            collision.transform.root.GetChild(1).GetChild(0).GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 0), ForceMode2D.Impulse);
                            car.boosted = true;
                        }
                        else
                        if (collision.tag == "Player")
                        {

                            Debug.Log("Player");
                            PlayerController player = collision.transform.root.GetChild(0).GetComponent<PlayerController>();
                            player.rig.AddForce(new Vector2(impulsePower / 15, 0), ForceMode2D.Impulse);
                            boosted = true;
                            gameObject.SetActive(false);

                        }

                    }

                }
                else
                 if (collision.tag == "Player")
                {

                    Debug.Log("Player");
                    PlayerController player = collision.transform.root.GetChild(0).GetComponent<PlayerController>();
                    player.rig.AddForce(new Vector2(impulsePower / 15, 0), ForceMode2D.Impulse);
                    boosted = true;
                    gameObject.SetActive(false);

                }


                boosted = true;
                gameObject.SetActive(false);


            }
            else
            if (collision.transform.root.GetComponent<PlayerController>())
            {

                Debug.Log("Player");
                PlayerController player = collision.transform.root.GetComponent<PlayerController>();
                player.rig.AddForce(new Vector2(impulsePower, 0), ForceMode2D.Impulse);
                boosted = true;
                gameObject.SetActive(false);

            }
        }
    }
}


    



