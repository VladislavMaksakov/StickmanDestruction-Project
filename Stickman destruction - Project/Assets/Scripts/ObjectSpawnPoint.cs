using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSpawnPoint : MonoBehaviour {

    GameObject old;
    public int id;
    
	// Use this for initialization
	void Start () {

        
        DontDestroyOnLoad(gameObject);
        old=GameObject.Find(name);
        if (old == gameObject)
        {
            old = GameObject.Find(name);
        }
        if (old == gameObject)
        {
            old = null;
        }
        if (old != null)
        {
            if (old.transform.childCount > 0)
            {
                GameUI.instance.objectPoints[id] = old.transform;
                old.GetComponent<SpriteRenderer>().enabled = false;
                //Debug.Log(PrefabUtility.GetPrefabObject(old.transform.GetChild(0).gameObject));
                // PrefabUtility.RevertPrefabInstance(old.transform.GetChild(0).gameObject);
                

                string prefabName = old.transform.GetChild(0).name;
                prefabName = prefabName.Remove(prefabName.Length - 7);
              
                
                GameUI.instance.selectedObjectPoint = old.transform;
                GameUI.instance.SelectObject(Resources.Load("Objects/"+prefabName, typeof(GameObject))as GameObject);

                //  GameObject instance = Instantiate(Resources.Load(prefabName, typeof(GameObject))) as GameObject;

                // Debug.Log(old.transform.GetChild(0).gameObject.name);
                Destroy(gameObject);
            }
            else
            {
                Destroy(old);
            }
        }
       

	}



    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Debug.Log("Destroy in menu");
            Destroy(gameObject);
        }
    }

  

    // Update is called once per frame
    void Update () {
      
    }

    public void SetAsNew()
    {
        GameUI.instance.objectPoints[id] = transform;
    }

    /*
    void MouseHit()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {

        }
    }
    */
    void OnMouseDown()
    {
        if (!GameUI.instance.started)
        {
            if (!GameUI.instance.objectSelectView.activeSelf)
            {
             
                GameUI.instance.selectedObjectPoint = transform;
                GameUI.instance.objectSelectView.SetActive(true);
            }
        }
    }
}
