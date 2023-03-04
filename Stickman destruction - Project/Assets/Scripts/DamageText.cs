using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {


    public string SortingLayerName = "Default";
    public int SortingOrder = 0;

   public int score;

    Color32 textColor;


    void Awake()
    {
     
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
        textColor = Color.white;
    }


    //need to write animation and transparency change functions

    void Start()
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        textMesh.text = (score*10).ToString();
        textMesh.fontSize = (20+score) * 7;
        textMesh.color = textColor;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = score * 10;
        StartCoroutine(Disapear(textMesh));
    }


    
    private void Move(int direction)
    {
        switch (direction)
        {
            case 0:
                transform.position += new Vector3(0.1f, 0, 0);
                break;
             
            case 1:
                transform.position += new Vector3(0, 0.1f, 0);
                break;

            case 2:
                transform.position += new Vector3(0, -0.1f, 0);
                break;
            case 3:
                transform.position += new Vector3(-0.1f, 0, 0);
                break;
            case 4:
                transform.position += new Vector3(0.05f, 0.05f, 0);
                break;
            case 5:
                transform.position += new Vector3(0.05f, -0.05f, 0);
                break;
            case 6:
                transform.position += new Vector3(-0.05f, 0.05f, 0);
                break;
            case 7:
                transform.position += new Vector3(-0.05f, -0.05f, 0);
                break;

        }
       
    }

    IEnumerator Disapear(TextMesh text)
    {
        int direction = Random.Range(0, 8);
        while (text.color.a > 0)
        {
          
            text.color=new Color(text.color.r,text.color.g,text.color.b,text.color.a-0.01f);
            Move(direction);
            yield return new WaitForSeconds(0.01f);
        }

        StopCoroutine(Disapear(text));
        Destroy(gameObject);
    }

    public static DamageText Create(int damage, GameObject prefab, Color32 color)
    {
        
        GameObject newObject = Instantiate(prefab) as GameObject;
        DamageText newText = newObject.GetComponent<DamageText>();
        newText.score = damage;
        newText.textColor = color;
        return newText;
    }

    public static DamageText Create(int damage, GameObject prefab)
    {

        GameObject newObject = Instantiate(prefab) as GameObject;
        DamageText newText = newObject.GetComponent<DamageText>();
        newText.score = damage;
        return newText;
    }
}
