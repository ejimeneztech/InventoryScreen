using UnityEngine;
using UnityEngine.InputSystem;

public class TestInventory : MonoBehaviour
{
    public Item key; // Reference to the item scriptable object
    public InventoryManager inventoryManager; // Reference to the InventoryManager script


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     inventoryManager.AddItem(testItem);
    // }


    void Update()
    {

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            inventoryManager.AddItem(key);
        }
            

    }
}
