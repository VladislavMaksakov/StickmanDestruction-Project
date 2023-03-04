using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


    public Image hpBar;

    public GameObject stickman;

    
    string stickmanType;

    Enemy enemy;
    PlayerController player;
    

    public int health;
    int maxHealth;

    private Color startColor;

	// Use this for initialization
	void Start () {
        stickmanType = GetStickmanOrigin();
        startColor = hpBar.color;
        GetHealth();
        maxHealth = health;
 
    }

    // Update is called once per frame
    void Update()
    {
        GetHealth();
        UpdateHealthBar();

    }
   
    void UpdateHealthBar()
    {
        hpBar.fillAmount = (float)health / maxHealth;
     
    }
    public void TakeDamage()
    {
        StopCoroutine(TakeDamageAnim());
        StartCoroutine(TakeDamageAnim());
    }

   IEnumerator TakeDamageAnim()
    {
        for (int i = 0; i < 6; i++)
        {
           
            hpBar.color = Color.red;
            yield return new WaitForSeconds(0.015f);
            hpBar.color = startColor;
            yield return new WaitForSeconds(0.015f);
        }
    }


    void GetHealth()
    {
        if (stickmanType == "Player")
        {
            health = player.health;
        }
        else if (stickmanType == "Enemy")
        {
        
            health = enemy.health;
        }
      
    }


    string GetStickmanOrigin()
    {
        if (stickman.GetComponent<PlayerController>() != null)
        {
            player = stickman.GetComponent<PlayerController>();
            return "Player";
        }
        else if (stickman.GetComponent<Enemy>() != null)
        {
            enemy = stickman.GetComponent<Enemy>();
            return "Enemy";
        }
        return "Error";
    }
	
}
