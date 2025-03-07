using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public ItemInventory item;
    public InventoryUI inventoryUI;
    private void Start()
    {
        inventory = inventoryUI.Inventory;

    }

    public void buttonPressed()
    {
        inventory.selectedItem = item;
        inventoryUI.buttonPressed = gameObject;
        inventoryUI.UpdateInformationUI();
        inventoryUI.ActivateInteraction();
    }

}