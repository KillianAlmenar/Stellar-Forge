using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCategory : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    public void AllPressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.ALL;
        inventoryUI.updateUI();
    }

    public void WeaponPressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.WEAPON;
        inventoryUI.updateUI();
    }

    public void KeyItemPressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.KEYITEM;
        inventoryUI.updateUI();

    }

    public void ConsommablePressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.CONSOMMABLE;
        inventoryUI.updateUI();

    }

    public void ArmorPressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.ARMOR;
        inventoryUI.updateUI();

    }

    public void RessourcePressed()
    {
        inventoryUI.sortingType = InventoryUI.SORTINGTYPE.RESSOURCE;
        inventoryUI.updateUI();

    }


}
