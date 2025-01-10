using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    public ItemInventory item;

    private void Start()
    {
        playerInventory = GameManager.instance.Player.GetComponent<Inventory>();

    }

    public void buttonPressed()
    {
        playerInventory.selectedItem = null;
        InventoryUI.instance.UpdateInformationUI();
        playerInventory.selectedItem = item;
        InventoryUI.instance.buttonPressed = gameObject;
    }
 
}