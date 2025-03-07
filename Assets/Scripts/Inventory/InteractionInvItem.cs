using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionInvItem : MonoBehaviour
{
    [SerializeField] private GameObject quantity;
    private bool isConsommable = false;
    [SerializeField] InventoryUI inventoryUI;
    public Inventory currentInv;
    public Inventory targetInv;

    private void Update()
    {
        if (currentInv.selectedItem is Consommable && !isConsommable)
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

        if (currentInv.selectedItem is Consommable consommable)
        {
            consommable.Use();
            currentInv.items.Remove(currentInv.selectedItem);
            currentInv.selectedItem = null;
            inventoryUI.updateUI();
            gameObject.SetActive(false);
        }
    }

    public void UseQuantityPressed()
    {
        ItemInventory item = currentInv.selectedItem;

        if(item as Consommable && item.stackSize > 1 && currentInv.GetNumberOfItem(item) > 1)
        {
            if (!quantity.activeSelf)
            {
                quantity.SetActive(true);
                quantity.GetComponent<QuantitySelection>().buttonPressed = QuantitySelection.QUANTITYBUTTON.USE;
            }
            quantity.transform.position = transform.position + new Vector3(100, 0, 0);
        }
    }

    public void TransfertPressed()
    {
        ItemInventory item = currentInv.GetComponent<Inventory>().selectedItem;

        if (item.stackSize == 1 || currentInv.GetComponent<Inventory>().GetNumberOfItem(item) <= 1)
        {
            currentInv.items.Remove(item);
            targetInv.AddItem(item, 1);

            currentInv.GetComponent<Inventory>().selectedItem = null;
            gameObject.SetActive(false);

        }
        else
        {
            if (!quantity.activeSelf)
            {
                quantity.SetActive(true);
                quantity.GetComponent<QuantitySelection>().buttonPressed = QuantitySelection.QUANTITYBUTTON.TRANSFERT;
            }
            quantity.transform.position = transform.position + new Vector3(100, 0, 0);

        }

        GameManager.instance.playerInventoryUI.updateUI();
        if (GameManager.instance.playerInventoryUI.inChest)
        {
            GameManager.instance.otherInventoryUI.updateUI();
        }
    }

    public void DestroyPressed()
    {
        ItemInventory item = currentInv.GetComponent<Inventory>().selectedItem;

        if (item.stackSize == 1 || currentInv.GetComponent<Inventory>().GetNumberOfItem(item) <= 1)
        {
            currentInv.GetComponent<Inventory>().items.Remove(item);
            inventoryUI.updateUI();

            currentInv.GetComponent<Inventory>().selectedItem = null;
            gameObject.SetActive(false);
        }
        else
        {
            if (!quantity.activeSelf)
            {
                quantity.SetActive(true);
                quantity.GetComponent<QuantitySelection>().buttonPressed = QuantitySelection.QUANTITYBUTTON.DESTROY;
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

        currentInv.GetComponent<Inventory>().selectedItem = null;
        gameObject.SetActive(false);

    }

}
