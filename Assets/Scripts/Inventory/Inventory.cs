using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Inventory : MonoBehaviour
{
    public List<ItemInventory> items;

    public ItemInventory selectedItem;

    public void DeleteItem(ItemInventory _item, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            items.Remove(_item);
        }
    }

    public void AddItem(ItemInventory _item, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            items.Add(_item);
        }
    }

    public int GetNumberOfItem(ItemInventory _item)
    {
        if(items.Contains(_item))
        {
            int quantity = 0;
            foreach (ItemInventory item in items)
            {
                if(item == _item)
                {
                    quantity++;
                }
            }

            return quantity;
        }
     
        return 0;  
    }

}