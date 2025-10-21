using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("Slot Settings")]
    public GameObject slotPrefab;
    public Transform slotParent; // Parent object to hold slots
    public int addSlots = 3; // Total slots in inventory

    [Header("UI Slots")]
    public List<Image> inventorySlots; // Drag your slot images here; need to get rid of this and just ref the imag of slotPrefab
    public Sprite emptySlotSprite; // Placeholder for empty

    public int selectedSlotIndex = -1;

    [Header("Inventory Screen")]
    public GameObject inventoryScreen;
    private bool isOpen = false; // Inventory Grid State

    void Start()
    {
        inventoryScreen.SetActive(isOpen);

        //Generate slots automatically
        for (int i = 0; i <= addSlots; i++)
        {
            // Add the Image component of the new slot to the list
            GameObject newSlot = Instantiate(slotPrefab, slotParent);

            Image slotImage = newSlot.GetComponent<Image>();
            if (slotImage == null) slotImage = newSlot.AddComponent<Image>();

            slotImage.sprite = emptySlotSprite;
            slotImage.color = Color.white;
            inventorySlots.Add(newSlot.GetComponent<Image>());

            //add index to slot UI script
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
            if(slotUI != null)
            {
                slotUI.slotIndex = i; 

                slotUI.inventoryManager = this;
            }

            

            //Add click listener to button
            Button slotButton = newSlot.GetComponent<Button>();
            if(slotButton != null)
            {

                int capturedIndex = i; // Capture the current index for the lambda to avoid closing issue

                slotButton.onClick.AddListener(() => OnSlotClicked(capturedIndex));
            }
            
        }
    }


    void Update()
    {
        // Toggle inventory screen
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            inventoryScreen.SetActive(isOpen);
        }
    }

    public void AddItem(Item item)
    {

        //Add item icon to first empty slot
        foreach (Image slot in inventorySlots)
        {
            Debug.Log("Checking slot: " + slot.name + ", current sprite: " + slot.sprite);
            if (slot.sprite == emptySlotSprite || slot.sprite == null) // Slot is empty
            {
                slot.sprite = item.icon;
                slot.color = Color.white;
                Debug.Log($"Placed {item.name}in slot: {slot.name}!");
                return; // Exit after adding the item
            }
        }
        Debug.Log("Inventory Full! Cannot add item: " + item.name);
    }

  

    //Use Item
    public void UseItem()
    {
        if(selectedSlotIndex >= 0 && selectedSlotIndex < inventorySlots.Count)
        {
            Debug.Log($"Using item in slot {selectedSlotIndex}");
        }
    }

    //Move Item
    public void MoveItem()
    {
        Debug.Log("Move item - not implemented yet");
    }

    //Discard Item
    public void DiscardItem()
    {
        Debug.Log($"Discarding item in slot {selectedSlotIndex}");
        inventorySlots[selectedSlotIndex].sprite = emptySlotSprite;

    }
    
    public void OnSlotClicked(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        Debug.Log($"Slot {slotIndex} clicked");
    }
}
