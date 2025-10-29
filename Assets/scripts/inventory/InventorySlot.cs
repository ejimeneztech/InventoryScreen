using UnityEngine;
using UnityEngine.UI;


public class InventorySlotUI : MonoBehaviour
{
    public int slotIndex;

    [Header("Sub Menu")]
    public GameObject subMenu;
    
    private RectTransform rectTransform;


    void Start()
    {
        //subMenu = GameObject.Find("Sub Menu").gameObject;
        rectTransform = GetComponent<RectTransform>();
        subMenu.SetActive(false); 
    }


    

    public void ClosePanel()
   {
       subMenu.SetActive(false);
   }
}