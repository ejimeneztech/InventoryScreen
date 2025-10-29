using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public int maxStack = 1;
    
    public virtual void Use()
    {
        Debug.Log($"Using item: {itemName}");
    }
}
