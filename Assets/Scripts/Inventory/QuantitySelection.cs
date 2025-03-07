using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuantitySelection : MonoBehaviour
{

    public enum QUANTITYBUTTON
    {
        USE,
        TRANSFERT,
        DESTROY
    }

    [SerializeField] private TextMeshProUGUI quantityText;
    private int quantity;
    ItemInventory item;
    public QUANTITYBUTTON buttonPressed;
    [SerializeField] public InventoryUI inventoryUI;
    Inventory currentInventory;
    [SerializeField] InteractionInvItem interactionInvItem;

    private void Update()
    {
        if (interactionInvItem != null && currentInventory == null)
        {
            currentInventory = interactionInvItem.currentInv;
        }

        if (item != currentInventory.selectedItem)
        {
            item = currentInventory.selectedItem;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        quantity = 1;
        updateText();
    }

    public void UpPressed()
    {
        quantity++;
        updateText();
    }

    public void DownPressed()
    {
        quantity--;
        updateText();
    }

    private void updateText()
    {
        currentInventory = interactionInvItem.currentInv;
        item = currentInventory.selectedItem;

        if (quantity > item.stackSize || quantity > currentInventory.GetNumberOfItem(item))
        {
            quantity = 1;
        }
        else if (quantity < 1)
        {
            if (item.stackSize > currentInventory.GetNumberOfItem(item))
            {
                quantity = currentInventory.GetNumberOfItem(item);
            }
            else
            {
                quantity = item.stackSize;
            }
        }

        quantityText.text = quantity.ToString();
    }

    public void OkPressed()
    {
        item = currentInventory.selectedItem;
        List<ItemInventory> itemToDestroy = new List<ItemInventory>();

        foreach (ItemInventory testedItem in currentInventory.items)
        {
            if (quantity > 0)
            {
                if (testedItem == item && testedItem is Consommable consommable)
                {

                    switch (buttonPressed)
                    {
                        case QUANTITYBUTTON.USE:
                            consommable.Use();
                            break;
                        case QUANTITYBUTTON.TRANSFERT:
                            interactionInvItem.targetInv.AddItem(item, 1);
                            break;
                    }
                    itemToDestroy.Add(item);
                    quantity -= 1;
                }

                if (testedItem == item && testedItem is Ressources ressources)
                {
                    if (buttonPressed == QUANTITYBUTTON.TRANSFERT)
                    {
                        interactionInvItem.targetInv.AddItem(item, 1);
                    }

                    itemToDestroy.Add(item);
                    quantity -= 1;
                }
            }
            else
            {
                break;
            }
        }

        foreach (ItemInventory testedItem in itemToDestroy)
        {
            currentInventory.items.Remove(testedItem);
        }

        GameManager.instance.playerInventoryUI.updateUI();
        if (GameManager.instance.playerInventoryUI.inChest)
        {
            GameManager.instance.otherInventoryUI.updateUI();
        }

        quantity = 1;
        updateText();


        if (!currentInventory.items.Contains(item))
        {
            currentInventory.selectedItem = null;
        }

        gameObject.SetActive(false);
        interactionInvItem.gameObject.SetActive(false);
    }

}