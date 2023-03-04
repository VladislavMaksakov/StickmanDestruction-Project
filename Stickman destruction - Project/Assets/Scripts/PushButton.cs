using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PushButton : Button, IPointerDownHandler, IPointerUpHandler
{


    public Image loader;
    bool holdTheButton;
    public GameObject pressHoldText;

    public float power;
    bool started;

    new void Start()
    {
        base.Start();
        power = 1;
    }

    float direction = 1;




    override public void OnPointerDown(PointerEventData eventData)
    {
        //FindObjectOfType<GameUI>().Pause();
        holdTheButton = true;
    }

    override public void OnPointerUp(PointerEventData eventData)
    {
        if (!started)
        {


            if (pressHoldText)
                pressHoldText.SetActive(false);
            GameUI.instance.StartSingleCharacter(power);
            started = true;
            interactable = false;

        }
    }

}
