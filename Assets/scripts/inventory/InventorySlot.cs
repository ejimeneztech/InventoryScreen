using UnityEngine;
using UnityEngine.UI;


public class InventorySlotUI : MonoBehaviour
{
    public int slotIndex;
    public InventoryManager inventoryManager;
    public GameObject contextMenuPanel; //panel with Use/Move/Discard buttons

    void Start()
    {
        if (contextMenuPanel != null)
        {
            contextMenuPanel.SetActive(false);
        }
    }

    public void OnSlotClicked()
    {
        contextMenuPanel.SetActive(true);
        contextMenuPanel.transform.position = transform.position;

        inventoryManager.selectedSlotIndex = slotIndex;

    }

    public void ClosePanel()
   {
       contextMenuPanel.SetActive(false);
   }
}