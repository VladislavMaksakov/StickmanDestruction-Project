using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour {

    public Rigidbody2D body;

    public float damageMultiplier =1;

    SpriteRenderer bodyPart;

    public GameObject bone;

    public bool canTakeDamage=true;

    public  float breakPower;

    public PlayerController rootPlayer;
    Color32 startColor;
    Rigidbody2D rig;
    HingeJoint2D connection;
	// Use this for initialization
	void Start () {
        bodyPart = GetComponent<SpriteRenderer>();
        bone = transform.GetChild(0).gameObject;
        bone.SetActive(false);
        // player = transform.root.GetComponent<PlayerController>();
        rig = GetComponent<Rigidbody2D>();
        if (GetComponent<HingeJoint2D>() != null)
        {
            connection = GetComponent<HingeJoint2D>();
        }
        startColor = bodyPart.color;
	}
	
	// Update is called once per frame
	void Update () {
      
	}

 

    void OnCollisionEnter2D(Collision2D col)
    {
        if (canTakeDamage)
        {
            if(rootPlayer.canTakeDamage)
            CheckDamage(col);
        }
    }

    void CheckDamage(Collision2D collision)
    {
        if (collision.gameObject.tag == rootPlayer.enemyTag || collision.gameObject.tag == "Car")
        {
           

                    if (body.velocity.magnitude >= breakPower)
                    {

                        bodyPart.color = new Color32(255, 0, 0, 100);
                // Debug.Log(body.angularVelocity);
                /*
                if (body.angularVelocity <= -1000f|| body.angularVelocity>=1000)
                {
                    if (connection)
                    {
                        connection.enabled = false;
                    }
                }
               */
                    int transportHitDamageMultiplier = 1;
                if (collision.gameObject.tag == "Car")
                {
                    transportHitDamageMultiplier = 4;
                }
                    TakeDamage((int)((body.velocity.magnitude * body.velocity.magnitude * body.velocity.magnitude) / 1800 * damageMultiplier* transportHitDamageMultiplier));
                      
                    }
                }
            
                //StartCoroutine(OnImpulse(collision.gameObject.GetComponent<Rigidbody2D>()));
         
    }




    IEnumerator OnImpulse(Rigidbody2D enemyRigit)
    {
        float addDamage=0;

        if (enemyRigit.velocity.magnitude > rig.velocity.magnitude) {
            addDamage = enemyRigit.velocity.magnitude - rig.velocity.magnitude;
        }
        Vector3 initialVelocity, newVelocity;
        //get velocity
        initialVelocity =rig.velocity;

        yield return null;
        yield return null;
        yield return null;

        //get new velocity
        newVelocity = rig.velocity;

        //impulse = magnitude of change
        Vector3 result = initialVelocity - newVelocity;

      
        if (result.magnitude > breakPower)
        {
            if (canTakeDamage)
            {
               // canTakeDamage = false;
                bodyPart.color = new Color32(255, 0, 0, 100);
                
                TakeDamage((int)((result.magnitude+addDamage)*damageMultiplier));
                Invoke("DamageCooldown",  3f);
            }
        }
        
    }

    void DamageCooldown()
    {
        rootPlayer.canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 5)
        {
            bone.SetActive(true);
        }
        rootPlayer.TakeDamage(damage,this);
    
        Invoke("ResetColor", 1f);
        PlaySound();
        if (Time.timeScale >= 1)
        {
         //   SlowMotion();
          
        }
     
    }

    void PlaySound()
    {
        if (name == "Head")
        {
            AudioManager.instance.DamageHead("Player");
        }
        else
        {
            AudioManager.instance.DamageBody("Player");
        }
    }

    void SlowMotion()
    {
      
        Time.timeScale = rootPlayer.slowTimeScale;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        StartCoroutine(ResetTimeScale());
    }

   void StartResetingTimeScale()
    {
        StartCoroutine(ResetTimeScale());
    }

    void ResetColor()
    {
        bodyPart.color = startColor;
        bone.SetActive(false);
    }

    IEnumerator ResetTimeScale()
    {
        if (Time.timeScale != 0)
        {
            while (Time.timeScale < 1)
            {
                
                    Time.timeScale += 0.1f;
                    Time.fixedDeltaTime = 0.02F * Time.timeScale;
                    yield return new WaitForSeconds(0.025f);
                
            }
            Time.timeScale = 1;
        }
       

    }


}
