using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TnT : MonoBehaviour {

    public float radius = 5.0f;
    public float power = 10.0f;
    public float breakPower=0;
    public GameObject explosionAnim;

    Rigidbody2D rig;
    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        if (!GameUI.instance.ended)
        {
            CheckPunchPower(col);
        }
    }


    void CheckPunchPower(Collision2D coll)
    {
       
        if (coll.gameObject.tag == "Car" || coll.gameObject.tag == "Player")
        {
            Explode();
            CancelInvoke("CheckPunchPower");
            return;
        }
        else if ((coll.relativeVelocity.magnitude > breakPower || coll.relativeVelocity.magnitude < -breakPower))
        {



            Explode();
            CancelInvoke("CheckPunchPower");
        }
    }


    void Explode()
    {

        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<DamageTaker>())
            {
                hit.GetComponent<DamageTaker>().TakeDamage(Random.Range(5, 20));
            }
            if (hit.GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                Debug.Log(rb.gameObject.name);

                 AddExplosionForce2D(rb, power, explosionPos, radius);
            }

        }
        //yield return new WaitForSeconds(1f);
        explosionAnim.transform.position = transform.position;
        explosionAnim.SetActive(true);
        AudioManager.instance.PlayExplosionSound();
        Destroy(gameObject);
    }


    
    void AddExplosionForce2D(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
    {
     
        Vector3 dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0)
        {
            calc *= -1;
        }


        if (body.velocity.magnitude < 40)
        {
            body.AddForce(dir.normalized * expForce * calc);
        }
    }
    

}



