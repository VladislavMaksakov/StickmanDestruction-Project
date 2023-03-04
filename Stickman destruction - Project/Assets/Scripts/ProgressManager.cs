using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressManager : MonoBehaviour
{


    public static ProgressManager instance;
    public GameObject transportsFolderObject;

    [HideInInspector]
    public List<ShopItem> transportItems;

    public GameObject characterFolderObject;

    [HideInInspector]
    public List<ShopItem> characterItems;

    public GameObject levelObjectsFolderObject;

    [HideInInspector]
    public List<ShopItem> levelObjectsItems;

    public GameObject levelsFolderObject;

    [HideInInspector]
    public List<ShopItem> levelsItems;

    [Space]
    [Header("Counters")]
    public AvailablesCount availableTransportCounter;
    public AvailablesCount availableCharactersCounter;
    public AvailablesCount availableLevelObjectsCounter;

    bool[] transportProgress;
    bool[] characterProgress;
    bool[] levelObjectsProgress;
    bool[] levelsProgress;


    // Use this for initialization
    void Start()
    {
        instance = this;
        GetItemLists();
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetItemLists()
    {
        int i = 0;
        if (!levelsFolderObject)
        {
            //game scene
            foreach (Transform transport in transportsFolderObject.transform)
            {
                transportItems.Add(transport.GetComponent<ShopItem>());
                transport.GetComponent<ShopItem>().id = i;
                i++;
            }

            i = 0;
            foreach (Transform character in characterFolderObject.transform)
            {
                characterItems.Add(character.GetComponent<ShopItem>());
                character.GetComponent<ShopItem>().id = i;
                i++;
            }

            i = 0;
            foreach (Transform levelObject in levelObjectsFolderObject.transform)
            {
                levelObjectsItems.Add(levelObject.GetComponent<ShopItem>());
                levelObject.GetComponent<ShopItem>().id = i;
                i++;
            }
        }
        else
        {
            foreach (Transform level in levelsFolderObject.transform)
            {
                levelsItems.Add(level.GetComponent<ShopItem>());
                level.GetComponent<ShopItem>().id = i;
                i++;
            }
            // menu scene + level select

        }
    }



    void LoadData()
    {
        if (!levelsFolderObject)
        {
            //game scene
            transportProgress = PlayerPrefsX.GetBoolArray("TransportProgress", false, transportItems.Count);
            characterProgress = PlayerPrefsX.GetBoolArray("CharacterProgress", false, characterItems.Count);
            levelObjectsProgress = PlayerPrefsX.GetBoolArray("ObjectsProgress", false, levelObjectsItems.Count);

            //update lists
            int i = 0;
            foreach (ShopItem transport in transportItems)
            {
                transport.opened = transportProgress[i];
                transport.CheckAvailability();

                i++;
            }

            i = 0;
            foreach (ShopItem character in characterItems)
            {
                character.opened = characterProgress[i];
                character.CheckAvailability();

                i++;
            }

            i = 0;
            foreach (ShopItem levelObj in levelObjectsItems)
            {
                levelObj.opened = levelObjectsProgress[i];
                levelObj.CheckAvailability();

                i++;
            }
        }
        else
        {
            //menu scene + level select
            levelsProgress = PlayerPrefsX.GetBoolArray("LevelsProgress", false, levelsItems.Count);

            //update list
            int i = 0;
            foreach (ShopItem level in levelsItems)
            {
                level.opened = levelsProgress[i];
                if (i == 0)
                {
                    level.opened = true;
                }
                level.CheckAvailability();
                i++;
            }
        }
        Debug.Log("DataLoaded");
    }

    public void ResetProgress()
    {

        // bad code

        int g33 = -2;

    lab:

        if (g33 < 0)
        {
            goto lab;
        }
    }

    public void SaveData(ShopItem.itemType typeToUpdate)
    {
        if (!levelsFolderObject)
        {
            int i = 0;
            //update bool arrays
            switch (typeToUpdate)
            {
                case ShopItem.itemType.Transport:
                    foreach (ShopItem transport in transportItems)
                    {
                        transportProgress[i] = transport.opened;
                        i++;
                    }

                    //save
                    PlayerPrefsX.SetBoolArray("TransportProgress", transportProgress);
                    availableTransportCounter.CheckAvailability();
                    //availableCharactersCounter.CheckAvailability();
                    availableLevelObjectsCounter.CheckAvailability();
                    break;

                case ShopItem.itemType.Character:
                    i = 0;
                    foreach (ShopItem character in characterItems)
                    {
                        characterProgress[i] = character.opened;
                        i++;
                    }

                    //save
                    PlayerPrefsX.SetBoolArray("CharacterProgress", characterProgress);
                    availableCharactersCounter.CheckAvailability();
                    availableTransportCounter.CheckAvailability();
                    availableLevelObjectsCounter.CheckAvailability();
                    break;

                case ShopItem.itemType.LevelObject:
                    i = 0;
                    foreach (ShopItem levelObject in levelObjectsItems)
                    {
                        levelObjectsProgress[i] = levelObject.opened;
                        i++;
                    }

                    //save
                    PlayerPrefsX.SetBoolArray("ObjectsProgress", levelObjectsProgress);
                    availableLevelObjectsCounter.CheckAvailability();
                    availableTransportCounter.CheckAvailability();
                    //availableCharactersCounter.CheckAvailability();
                    break;

            }
        }
        else
        {
            //update bool arrays
            int i = 0;
            foreach (ShopItem level in levelsItems)
            {
                levelsProgress[i] = level.opened;
                i++;
            }
            //save
            PlayerPrefsX.SetBoolArray("LevelsProgress", levelsProgress);
        }
        Debug.Log("DataSaved");
    }

}
