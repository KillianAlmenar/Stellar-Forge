using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public Item selectedItem;

    public void DeleteItem(Item _item, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            items.Remove(_item);
        }
    }

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            items.Add(_item);
        }
    }

    public int GetNumberOfItem(Item _item)
    {
        if(items.Contains(_item))
        {
            int quantity = 0;
            foreach (Item item in items)
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