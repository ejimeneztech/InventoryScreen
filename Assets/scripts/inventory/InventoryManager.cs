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
            if (slotUI != null)
            {
                slotUI.slotIndex = i;
                //slotUI.subMenu = subMenuPanel; 
            }



            //Add click listener to button
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
            {

                int capturedIndex = i; // Capture the current index for the lambda to avoid closing issue

                slotButton.onClick.AddListener(() => OnSlotClicked(capturedIndex));
            }




        }
        
            //Add Click Listener to sub menu use button
            Button useButton = subMenuPanel.transform.Find("Use").GetComponent<Button>();
            useButton.onClick.AddListener(() => UseItem(selectedSlotIndex));
            
            //Add Click Listener to Discard button
            Button discardButton = subMenuPanel.transform.Find("Close").GetComponent<Button>();
            discardButton.onClick.AddListener(() => DiscardItem(selectedSlotIndex));
            
            
            //Add Click Listener to sub menu close button
            Button closeButton = subMenuPanel.transform.Find("Close").GetComponent<Button>();
            closeButton.onClick.AddListener(() => Close());
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
    public void UseItem(int slotIndex)
    {
        // Basic safety check to prevent errors. Also, check if item can be used.
        if (slotIndex < 0 || slotIndex >= inventorySlots.Count)
        {
            Debug.LogWarning($"Invalid slot index: {slotIndex}");
            return;
        }

        Debug.Log($"Using item in slot {slotIndex}");
        inventorySlots[slotIndex].sprite = emptySlotSprite;
        subMenuPanel.SetActive(false);

    }

    public void DiscardItem(int slotIndex)
    {
        Debug.Log($"Discarding item in slot {slotIndex}");
        inventorySlots[slotIndex].sprite = emptySlotSprite; 
    }

    ///Close Sub Menu
    public void Close()
    {
        subMenuPanel.SetActive(false);
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

        subMenuPanel.SetActive(true);
        
        Debug.Log($"Slot {slotIndex} clicked.");
    }




}
