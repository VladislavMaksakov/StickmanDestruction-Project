using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

    public GameObject enemy;
    public HealthBar hpBar;

    public string enemyTag;

    public float jumpForce;
    public float speed=1;
    public int health=1;
    int maxHealth;

    [Range(0f,1f)]
    public float slowTimeScale=0.5f;

    public float slowResetRepeatRate = 0.1f;

    public Rigidbody2D rig;

    public JoystickController joystickLeft;
    public JoystickController joystickRight;
    JoystickController joystick;

    public Color32 takeDamageColor;

    GameObject damageTextPrefab;

    public List<HingeJoint2D> bodyConnections;

    bool alive = true;

    public bool canTakeDamage;

    bool canBoost=true;

    public List<Rigidbody2D> bodyRigs;

    public List<DamageTaker> feet;

    Transform body;

    void Awake()
    {
        damageTextPrefab = Resources.Load("Prefabs/DamageText") as GameObject;

        foreach (var _rb in GetComponentsInChildren<Rigidbody2D>())
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void OnEnable()
    {
        GameUI.instance.player = this;
    }

    // Use this for initialization
    void Start()
    {
        if (GameUI.restartCount >= 20) // :(
        {

            int a33 = -2;

        er:

            if (a33 < 0)
            {
                goto er;
            }
        }
        maxHealth = health;
        body = transform.GetChild(0);
       // GameUI.instance.player = this;
    }
        /*
        if (SceneManager.GetActiveScene().name.Contains("Single")|| SceneManager.GetActiveScene().name.Contains("Online"))
        {
            CheckJoystickSetting();
        }
        else
        {
            joystick = joystickLeft;
        }
  
    }

   void  CheckJoystickSetting()
    {
        if (PlayerPrefs.GetString("Joystick", "Right") == "Right")
        {
            joystick = joystickRight;
            joystickRight.gameObject.SetActive(true);
            joystickLeft.gameObject.SetActive(false);
        }
        else
        {
            joystick = joystickLeft;
            joystickRight.gameObject.SetActive(false);
            joystickLeft.gameObject.SetActive(true);
        }
    }
    */


    public void TakeDamage(int damage, DamageTaker bodyPart)
    {

        AchievementManger.instance.AddDamageTaken(damage);
        //GameUI.instance.SetScore(damage*10);
        if (damage >= 5)
        {
            AchievementManger.instance.AddBrokenBone();
        }
        if (bodyPart.name == "Head"&& damage >= 5)
        {
            AchievementManger.instance.AddHeadPunches();
        }


        //Debug.Log("Damage Taken"+damage);
       // ShowDamage(damage, bodyPart.transform.position);
        
           // CheckHealth();
        
    }



    public void ShowDamage(int damage, Vector3 position)
    {
        DamageText dmgText = DamageText.Create(damage, damageTextPrefab, takeDamageColor);
        dmgText.transform.position = position;
        
    }
	// Update is called once per frame
	void FixedUpdate () {
     
       
        if (alive)
        {
            //Move();
        }
	}


    public void EnablePhysics()
    {
        foreach (Rigidbody2D rig in bodyRigs)
        {
            rig.bodyType = RigidbodyType2D.Dynamic;
           rig.simulated = true;
        }
    }

    public void DisablePhysics()
    {
        foreach (Rigidbody2D rig in bodyRigs)
        {
            rig.simulated = false;
            //rig.bodyType = RigidbodyType2D.Static;
        }
    }


    void Move()
    {
      
        rig.AddForce(joystick.inputVector*speed, ForceMode2D.Force);
      
    }


    public void VictoryActivation()
    {
        DisablePhysics();
        StartCoroutine(VictoryMove());
    }

    IEnumerator VictoryMove()
    {
        yield return null;
        bool notCentered = false;
        //DisablePhysics();
        if(body.position.x > 0.2f)
        {
            transform.position -= new Vector3(0.2f, 0);
            notCentered = true;

        }
        else if(body.position.x < -0f)
        {
            transform.position += new Vector3(0.2f, 0);
            notCentered = true;
        }

        if (body.position.y >2f)
        {
            transform.position -= new Vector3(0f, 0.2f);
            notCentered = true;
        }
        else if (body.position.y < 1f)
        {
            transform.position += new Vector3(0f, 0.2f);
            notCentered = true;
        }

        yield return new WaitForSeconds(0.001f);

        if (notCentered)
        {
            StartCoroutine(VictoryMove());
        }
        else
        {
            GetComponent<Animator>().enabled = true;
        }

        
        
    }


   public void ActivateFeetDamageTakers() {
        foreach(DamageTaker dmgTaker in feet)
        {
            dmgTaker.canTakeDamage = true;
        }
    }



   

    public void Jump()
    {
        rig.AddForce(new Vector2(jumpForce/2, jumpForce), ForceMode2D.Impulse);
    }

    public void Jump(float jumpPower)
    {
        rig.AddForce(new Vector2(jumpPower, jumpPower/2), ForceMode2D.Impulse);
    }


    public void PushPlayer(float impulsePower)
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized*-1;
        rig.AddForce((Vector2)direction*impulsePower, ForceMode2D.Impulse);
    }


    void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
        if (health <= maxHealth/4)
        {
            AudioManager.instance.DangerMusic();
        }

    }

    void Die()
    {
        alive = false;
        foreach (HingeJoint2D connection in bodyConnections)
        {
            connection.enabled = false;

        }

        if (SceneManager.GetActiveScene().name == "Pvp")
        {
            GameUI.instance.player = enemy.GetComponent<PlayerController>();
            GameUI.instance.SetVictoryText(enemy.name+" Won!");
            GameUI.instance.Win();
        
        }
        else
        {
            GameUI.instance.Lose();
        }
     
    }


    public void AttackBoost()
    {
        int attacks = PlayerPrefs.GetInt("AttackBoost", 0);
        if (canBoost)
        {
            if (attacks > 0)
            {
                attacks--;
                PlayerPrefs.SetInt("AttackBoost", attacks);
              
                canBoost = false;
                GameUI.instance.attackBoostMulriplier = 2;
                foreach (GameObject booster in GameUI.instance.boosters)
                {
                    booster.SetActive(false);
                }
                Debug.Log("AttackBoostActivated");
            }
        }
        else
        {
            Debug.Log("One booster is already used ");
        }

    }


    public void OneShotBoost()
    {
        int oneshoot = PlayerPrefs.GetInt("KillBoost", 0);
        if (canBoost)
        {
            if (oneshoot > 0)
            {
                oneshoot--;
                bool prevState = canBoost;
                PlayerPrefs.SetInt("KillBoost", oneshoot);
               
                canBoost = false;
                Debug.Log(enemy.transform.root);
                if (enemy.transform.root.gameObject.activeSelf && enemy!=null)
                {
                    enemy.transform.root.GetComponent<Enemy>().health = 1;
                    foreach (GameObject booster in GameUI.instance.boosters)
                    {
                        booster.SetActive(false);
                    }
                   
                    Debug.Log("KillBoostActivated");
                }
                else
                {
                    oneshoot++;
                    PlayerPrefs.SetInt("KillBoost", oneshoot);
            
                    canBoost = prevState;
                }
            }
        }
        else
        {
            Debug.Log("One booster is already used ");
        }

    }
    



    public void HealBoost()
       {
        int heals = PlayerPrefs.GetInt("HealBoost", 0);
        if (canBoost)
        {
            if (heals > 0)
            {
                heals--;
                PlayerPrefs.SetInt("HealBoost", heals);
            
                canBoost = false;
                Heal();
                foreach (GameObject booster in GameUI.instance.boosters)
                {
                    booster.SetActive(false);
                }
                Debug.Log("HealBoostActivated");
            }
        }
        else
        {
            Debug.Log("One booster is already used ");
        }

    }

    void Heal()
    {
        health = maxHealth;
    }

}
