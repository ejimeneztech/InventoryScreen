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

    [Header("Sub Menu")]
    public GameObject subMenuPanel;


    void Start()
    {
        inventoryScreen.SetActive(isOpen);
        subMenuPanel.SetActive(false);

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
                //slotUI.subMenu = subMenuPanel; 
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

        //Get RectTransform of clicked slot
        RectTransform slotRect = inventorySlots[slotIndex].GetComponent<RectTransform>();

        //Get the RectTransform of the subMenuPanel
        RectTransform menuRect = subMenuPanel.GetComponent<RectTransform>();

        // Keep world space relationship
        //subMenuPanel.transform.SetParent(slotRect.parent, worldPositionStays: true);

        //Position subMenuPanel next to clicked slot
        //1. Get slot position in world space
        Vector3 slotWorldPos = slotRect.position;
        //2. Set the menu position to the right of the slot
        Vector3 menuPos = slotWorldPos + new Vector3(slotRect.rect.width / 2 + menuRect.rect.width / 2, 0, 0); 
        subMenuPanel.transform.position = menuPos;
        
        
        subMenuPanel.SetActive(true);
    }




}
