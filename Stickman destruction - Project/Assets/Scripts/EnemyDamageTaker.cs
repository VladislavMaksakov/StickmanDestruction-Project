using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaker : MonoBehaviour {


    public Rigidbody2D body;

    SpriteRenderer bodyPart;

    public float damageMultiplier=1;


    public float breakPower;
    bool canTakeDamage = true;

 
   

    Enemy enemy;
    Color32 startColor;

    Rigidbody2D rig;

	// Use this for initialization
	void Start () {
        bodyPart = GetComponent<SpriteRenderer>();
        enemy = transform.root.GetComponent<Enemy>();
        rig = GetComponent<Rigidbody2D>();
        startColor = bodyPart.color;
        Invoke("HeadDefRemove",7f);
       
	}
	
	// Update is called once per frame
	void Update () {
       
    }


    void CalculateVelocity()
    {

    }


    void HeadDefRemove()
    {
        if (gameObject.name == "Head")
        {
           
           damageMultiplier = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        CheckDamage(col);
    }

   

    void CheckDamage(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<DamageTaker>().body.velocity.magnitude>= breakPower)
            {
                if (enemy.canTakeDamage)
                {
                    enemy.canTakeDamage = false;
                    bodyPart.color = new Color32(255, 0, 0, 100);

                    TakeDamage((int)(collision.gameObject.GetComponent<DamageTaker>().body.velocity.magnitude * damageMultiplier * GameUI.instance.attackBoostMulriplier));
                    Invoke("DamageCooldown", 1f);
                }
            }
            //StartCoroutine(OnImpulse(collision.gameObject.GetComponent<Rigidbody2D>()));
        }
    }

    IEnumerator OnImpulse(Rigidbody2D playerRig)
    {

        float addDamage = 0;

        if (playerRig.velocity.magnitude > rig.velocity.magnitude)
        {
            addDamage = playerRig.velocity.magnitude - rig.velocity.magnitude;
        }

        Vector3 initialVelocity, newVelocity;
        //get velocity
        initialVelocity = playerRig.velocity;


        yield return null;
        yield return null;
        yield return null;



        //get new velocity
        newVelocity = playerRig.velocity;

        //impulse = magnitude of change
        Vector3 result = initialVelocity - newVelocity;

        
        if (result.magnitude > breakPower)
        {
            if (enemy.canTakeDamage)
            {
                canTakeDamage = false;
                bodyPart.color = new Color32(255, 0, 0, 100);
                TakeDamage((int)(result.magnitude*damageMultiplier));
                Invoke("DamageCooldown", 1f);
            }
        }
      
   

    }

    void DamageCooldown()
    {
        enemy.canTakeDamage = true;
    }

    void TakeDamage(int damage)
    {
        enemy.TakeDamage(damage, this);
        PlaySound();
        Invoke("ResetColor", 1f);

    }


    void PlaySound()
    {
        if (name == "Head")
        {
            AudioManager.instance.DamageHead("Enemy");
        }
        else
        {
            AudioManager.instance.DamageBody("Enemy");
        }
    }
    void ResetColor()
    {
        bodyPart.color = startColor;
    }


}
