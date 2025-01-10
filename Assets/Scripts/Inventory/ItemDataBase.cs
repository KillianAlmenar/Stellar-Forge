using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemInventory> items = new List<ItemInventory>();

    public void AddItem(ItemInventory item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log($"Ajouté à la base de données : {item.name}");
        }
    }
}
