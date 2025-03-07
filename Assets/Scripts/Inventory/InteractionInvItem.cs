using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionInvItem : MonoBehaviour
{
    [SerializeField] private GameObject quantity;
    private bool isConsommable = false;
    [SerializeField] InventoryUI inventoryUI;
    private void Update()
    {
        if (GameManager.instance.Player.GetComponent<Inventory>().selectedItem is Consommable && !isConsommable)
        {
            isConsommable = true;
        }
        else if (isConsommable)
        {
            isConsommable = false;
        }
    }

    public void UsePressed()
    {
        if (quantity.activeSelf)
        {
            quantity.SetActive(false);
        }

        if (GameManager.instance.Player.GetComponent<Inventory>().selectedItem is Consommable consommable)
        {
            consommable.Use();
            GameManager.instance.Player.GetComponent<Inventory>().items.Remove(GameManager.instance.Player.GetComponent<Inventory>().selectedItem);
            GameManager.instance.Player.GetComponent<Inventory>().selectedItem = null;
            inventoryUI.updateUI();
        }
    }

    public void UseQuantityPressed()
    {
        ItemInventory item = GameManager.instance.Player.GetComponent<Inventory>().selectedItem;

        if(item as Consommable && item.stackSize > 1 && GameManager.instance.Player.GetComponent<Inventory>().GetNumberOfItem(item) > 1)
        {
            if (!quantity.activeSelf)
            {
                quantity.SetActive(true);
                quantity.GetComponent<QuantitySelection>().useButton = true;
            }
            quantity.transform.position = transform.position + new Vector3(100, 0, 0);
        }
    }

    public void DestroyPressed()
    {
        ItemInventory item = GameManager.instance.Player.GetComponent<Inventory>().selectedItem;

        if (item.stackSize == 1 || GameManager.instance.Player.GetComponent<Inventory>().GetNumberOfItem(item) <= 1)
        {
            GameManager.instance.Player.GetComponent<Inventory>().items.Remove(item);
            inventoryUI.updateUI();

            GameManager.instance.Player.GetComponent<Inventory>().selectedItem = null;

        }
        else
        {
            if (!quantity.activeSelf)
            {
                quantity.SetActive(true);
                quantity.GetComponent<QuantitySelection>().useButton = false;
            }
            quantity.transform.position = transform.position + new Vector3(100, 0, 0);

        }

    }

    public void BackPressed()
    {
        if (quantity.activeSelf)
        {
            quantity.SetActive(false);
        }

        GameManager.instance.Player.GetComponent<Inventory>().selectedItem = null;
    }

}
