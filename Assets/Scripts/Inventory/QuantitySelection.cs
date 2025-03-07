using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuantitySelection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    private int quantity;
    ItemInventory item;
    public bool useButton = false;
    [SerializeField] public InventoryUI inventoryUI;

    private void Update()
    {
        if (item != GameManager.instance.Player.GetComponent<Inventory>().selectedItem)
        {
            item = GameManager.instance.Player.GetComponent<Inventory>().selectedItem;
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
        item = GameManager.instance.Player.GetComponent<Inventory>().selectedItem;

        if (quantity > item.stackSize || quantity > GameManager.instance.Player.GetComponent<Inventory>().GetNumberOfItem(item))
        {
            quantity = 1;
        }
        else if (quantity < 1)
        {
            if (item.stackSize > GameManager.instance.Player.GetComponent<Inventory>().GetNumberOfItem(item))
            {
                quantity = GameManager.instance.Player.GetComponent<Inventory>().GetNumberOfItem(item);
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
        item = GameManager.instance.Player.GetComponent<Inventory>().selectedItem;

        List<ItemInventory> itemToDestroy = new List<ItemInventory>();

        foreach (ItemInventory testedItem in GameManager.instance.Player.GetComponent<Inventory>().items)
        {
            if (quantity > 0)
            {
                if (testedItem == item && testedItem is Consommable consommable)
                {
                    if (useButton)
                    {
                        consommable.Use();
                    }
                    itemToDestroy.Add(item);
                    quantity -= 1;
                }

                if (!useButton && testedItem == item && testedItem is Ressources ressources)
                {
                    itemToDestroy.Add(item);
                    quantity -= 1;
                }
            }
            else
            {
                break;
            }
        }

        foreach (ItemInventory testtedItem in itemToDestroy)
        {
            GameManager.instance.Player.GetComponent<Inventory>().items.Remove(testtedItem);
        }

        inventoryUI.updateUI();

        quantity = 1;
        updateText();


        if (!GameManager.instance.Player.GetComponent<Inventory>().items.Contains(item))
        {
            GameManager.instance.Player.GetComponent<Inventory>().selectedItem = null;
        }

        gameObject.SetActive(false);

    }

}
