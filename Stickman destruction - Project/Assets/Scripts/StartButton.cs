using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : Button, IPointerDownHandler, IPointerUpHandler
{


    public Image loader;
    bool holdTheButton;
    public GameObject pressHoldText;

    float power;
    bool started;

    new void Start()
    {
        base.Start();
        InvokeRepeating("Charge", 0.1f, 0.005f);
    }

    float direction = 1;
    float loadingScale = 0.95f;


    void Charge()
    {
        if (holdTheButton)
        {
            if (power < 1)
            {
                if (power > 0.6f)
                {
                    power += direction * 0.03f;
                }
                else
                {
                    power += direction * 0.01f;
                }

                if (power < 0)
                {
                    power = 0.02f;
                    direction = 1f;
                }
            }
            else
            {
                direction = -1f;
                power = 0.99f;
            }
            loader.fillAmount = power;
            loader.color = Color.Lerp(Color.green, Color.red, power);
            loader.transform.localScale = new Vector3(loadingScale + Mathf.Clamp(power, 0.01f, 0.3f), loadingScale + Mathf.Clamp(power, 0.01f, 0.3f), loadingScale);
        }
    }
    override public void OnPointerDown(PointerEventData eventData)
    {

        holdTheButton = true;
    }

    override public void OnPointerUp(PointerEventData eventData)
    {
        if (!started)
        {
            holdTheButton = false;
            CancelInvoke("Charge");
            if (pressHoldText)
                pressHoldText.SetActive(false);
            GameUI.instance.StartSingleCharacter(power);
            started = true;
            if (gameObject.name == "JumpButton")
            {
                transform.GetChild(0).GetComponent<Text>().text = Localisation.GetString("Re");

            }
        }
        else if (gameObject.name == "JumpButton")
        {

            //GameUI.instance.Restart();
            Application.LoadLevel("Menu");
        }
    }

}
