using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadIdle : MonoBehaviour {

    public float speed;
    public float scaleSpeed;
    public int direction;

    public Color lerpedColor;
    public Color startColor;
    public Color endColor;
    Image image;
    SpriteRenderer spriteRend;
    int randomColorSpeed;

    // Use this for initialization
    void Start()
    {
        direction = Random.Range(0, 4);
        Invoke("ChangeDirection", Random.Range(3f, 7f));
        if (GetComponent<Image>())
        {
            image = GetComponent<Image>();
            startColor = image.color;
        }
        else
        {
            spriteRend = GetComponent<SpriteRenderer>();
            startColor = spriteRend.color;
        }
        randomColorSpeed = Random.Range(5, 20);


    }

    // Update is called once per frame
    void Update () {
        Move();
        ChangeColor();

    }

   void FixedUpdate()
    {
      
    }

    void ChangeColor()
    {
        lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time/ randomColorSpeed, 1));
        if (image)
        {
            image.color = lerpedColor;
        }
        else
        {
            spriteRend.color = lerpedColor;
        }
    }

    void Move()
    {
        switch (direction)
        {

            case 0:
                transform.position += new Vector3(speed, 0);
                transform.localScale+= new Vector3(scaleSpeed, scaleSpeed);
                break;
            case 1:
                transform.position -= new Vector3(speed, 0);
                transform.localScale -= new Vector3(scaleSpeed, scaleSpeed);
                break;
            case 2:
                transform.position -= new Vector3(0, speed);
                transform.localScale += new Vector3(scaleSpeed, scaleSpeed);
                break;
            case 3:
                transform.position += new Vector3(0, speed);
                transform.localScale -= new Vector3(scaleSpeed, scaleSpeed);
                break;

            default:
                break;
        }
      
    }

    void ChangeDirection()
    {
        int prevDirection = direction;
        direction = Random.Range(0, 4);
        if (direction == prevDirection)
        {
            ChangeDirection();
            return;

        }
        Invoke("ChangeDirection", 5f);
    }
}
