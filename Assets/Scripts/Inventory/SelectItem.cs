using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    public ItemInventory item;
    public InventoryUI inventoryUI;
    private void Start()
    {
        playerInventory = GameManager.instance.Player.GetComponent<Inventory>();

    }

    public void buttonPressed()
    {
        playerInventory.selectedItem = null;
        inventoryUI.UpdateInformationUI();
        playerInventory.selectedItem = item;
        inventoryUI.buttonPressed = gameObject;
    }
 
}