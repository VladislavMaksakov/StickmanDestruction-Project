using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int health;
    public HealthBar hpBar;

    public float speed=1;

    public Rigidbody2D rig;

    public Transform player;

   Vector3 directionVector;

    public GameObject damageTextPrefab;

    public Color32 takeDamageColor;

    int randomDirection=1;
    int previousDirection;

    [Range(0f, 1f)]
    public float slowTimeScale = 0.5f;

    public float slowResetRepeatRate = 0.1f;

    bool alive = true;

    public bool canTakeDamage=true;

    public enum Behaviour
    {
        Agressive,
        Randomly
    }



    public Behaviour enemyBehaviour = Behaviour.Agressive;

    public List<HingeJoint2D> bodyConnections;

    // Use this for initialization
    void Start () {
        SetAgressiveBehavior();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (alive)
        {
            Move(1);
        }
      
    }



    void CheckBehaviour()
    {
        if (enemyBehaviour != Behaviour.Randomly)
        {
            if (rig.velocity.x <= 1f && rig.velocity.y <= 1f && rig.velocity.x >= -1f && rig.velocity.y >= -1f)
            {
                CancelInvoke("CheckBehaviour");
                enemyBehaviour = Behaviour.Randomly;

                randomDirection = GetRandomDirection();
                Invoke("CheckVelocityInNewState", 0.2f);
                Invoke("SetAgressiveBehavior",1.5f);
            }
        }
    }


   

    int GetRandomDirection()
    {
        int random = Random.Range(1, 6);
        if (random == previousDirection)
        {
          return  GetRandomDirection();
        }
        else
        {
            previousDirection = random;
            return random;
        }

    }
    void SetAgressiveBehavior()
    {
        enemyBehaviour = Behaviour.Agressive;
        if (alive)
        {
            Move(7);
        }
        Invoke("CheckVelocityInNewState", 0.2f);
        InvokeRepeating("CheckBehaviour", 1f, 0.5f);
       
    }

    void CheckVelocityInNewState()
    {
        if (rig.velocity.x <= 0.2f && rig.velocity.y <= 0.2f && rig.velocity.x >= -0.2f && rig.velocity.y >= -0.2f)
        {
            CancelInvoke("CheckBehaviour");
            CancelInvoke("SetAgressiveBehavior");
            if (enemyBehaviour == Behaviour.Agressive)
            {
             
                enemyBehaviour = Behaviour.Randomly;

                randomDirection = GetRandomDirection();

                Invoke("SetAgressiveBehavior", 1.5f);
            }
            else
            {
              
                SetAgressiveBehavior();
            }
        }

    }

    void Move(int boost)
    {

        switch (enemyBehaviour)
        {
            case Behaviour.Agressive:
                directionVector = Vector3.Normalize(player.transform.position - transform.position);
                break;

            
            case Behaviour.Randomly:
                
                switch (randomDirection)
                {
                    case 1:
                        directionVector = new Vector2(-1, 0.5f);
                        break;
                    case 2:
                        directionVector = new Vector2(1, 0.5f);
                        break;
                    case 3:
                        directionVector = new Vector2(0, 1);
                        break;
                    case 4:
                        directionVector = new Vector2(1, 1);
                        break;
                    case 5:
                        directionVector = new Vector2(-1, 1);
                        break;
                  
                }

                break;
        }

        rig.AddForce(directionVector * speed*boost, ForceMode2D.Force);

    }


    public void TakeDamage(int damage, EnemyDamageTaker bodyPart)
    {
        health -= damage;
        ShowDamage(damage, bodyPart.transform.position);
        hpBar.TakeDamage();
        CheckHealth();
    }


    public void ShowDamage(int damage, Vector3 position)
    {
        DamageText dmgText = DamageText.Create(damage, damageTextPrefab, takeDamageColor);
        dmgText.transform.position = position;

    }

    public void PushPlayer(float impulsePower)
    {
        Vector3 direction = (player.position - transform.position).normalized * -1;
        rig.AddForce((Vector2)direction * impulsePower, ForceMode2D.Impulse);
    }


    void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (alive)
        {
            alive = false;
            foreach (HingeJoint2D connection in bodyConnections)
            {
                connection.enabled = false;

            }


            GameUI.instance.DecreaseEnemiesLeft();
            Invoke("Deactivate", 3f);
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
