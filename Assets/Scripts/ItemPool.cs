using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    #region Singleton
    public static ItemPool sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of ItemPool has been detected");
        }
    }
    #endregion

    private void Start() {
        InitializeItemPool();
    }

    [Header("OBJECT POOL")]
    public List<GameObject> pooled_items = new List<GameObject>();

    [Header("OBJECTS TO SPAWN")]
    public ItemType[] itemTypes;
    public GameObject spawnPoint;
    public int num_of_instances = 100;
    [Header("SELECTED OBJECTS")]
    public ItemType selectedItemType;
    public ItemType selectedCerealType, selectedMilkType;
    
    public ItemType nullItemType;
    //Initializes the Item Pool. Executes at the very start of the application
    public void InitializeItemPool()
    {

        for(int i = 0; i < itemTypes.Length; i++)
        {
            //Initialize the objects
            for(int j = i * num_of_instances; j < (i + 1) * num_of_instances; j++)
            {
                int rand_index = Random.Range(0, itemTypes[i].itemPrefabs.Length - 1);
                GameObject new_item = Instantiate(itemTypes[i].itemPrefabs[rand_index]);
                new_item.name = itemTypes[i].item_name + j.ToString();
                pooled_items.Add(new_item);
                new_item.SetActive(false);
            }
        }
    }

    //Selects the item in the thing
    public void spawnItem(int itemIndex)
    {
        string itemTypeName = itemTypes[itemIndex].item_name;
        //Loop through the current Object Pool for an object with the same name AND is not currently active
        for (int i = 0; i < itemTypes.Length; i++)
        {
            if (itemTypes[i].item_name == itemTypeName)
            {
                //Select Projectile
                selectedItemType = itemTypes[i];

                if(selectedItemType.isCereal)
                    selectedCerealType = selectedItemType;
                else
                    selectedMilkType = selectedItemType;

                //Enable the thing
                for(int j = i * num_of_instances; j < (i + 1) * num_of_instances; j++)
                {
                    //Found the right object, spawn it into the world
                        Debug.Log("Spawninggg");
                        pooled_items[j].transform.position = spawnPoint.transform.position;
                        pooled_items[j].SetActive(true);
                }

                break;
            }
        }

        if(itemTypes[itemIndex].isCereal)
            SoundController.sharedInstance.playVFX(SoundController.sharedInstance.fallingCerealSound, false);
        else
            SoundController.sharedInstance.playVFX(SoundController.sharedInstance.fallingMilkSound, false);
    }

    public void clearAllItems()
    {
        //Disable everything lmaoooo
        for (int i = 0; i < itemTypes.Length; i++)
        {
            //Select Item
            selectedItemType = nullItemType;
            selectedCerealType = nullItemType;
            selectedMilkType = nullItemType;


            for(int j = i * num_of_instances; j < (i + 1) * num_of_instances; j++)
                pooled_items[j].SetActive(false); 
        }
    }

    public int getItemTypeIndex(string item_name)
    {
        for(int i = 0; i < itemTypes.Length; i++)
        {
            if(item_name == itemTypes[i].item_name)
                return i;
        }

        return -1;
    }

    public void ExplosionEffect()
    {
        foreach(GameObject item in pooled_items)
        {
            if(item.activeSelf)
            {
                Rigidbody _rb = item.GetComponent<Rigidbody>();

                //Add explosion force
                _rb.AddExplosionForce(100f, item.transform.position, 10f, 10f, ForceMode.Impulse);
            }
        }
    }
}

[System.Serializable]
public struct ItemType
{
    public string item_name;
    public string item_description;
    public GameObject[] itemPrefabs;
    public bool isCereal;
}
