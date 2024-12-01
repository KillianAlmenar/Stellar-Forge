using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCategory : MonoBehaviour
{
    public void AllPressed()
    {
        InventoryUI.instance.sortingType = InventoryUI.SORTINGTYPE.ALL;
        InventoryUI.instance.updateUI();
    }

    public void WeaponPressed()
    {
        InventoryUI.instance.sortingType = InventoryUI.SORTINGTYPE.WEAPON;
        InventoryUI.instance.updateUI();
    }

    public void KeyItemPressed()
    {
        InventoryUI.instance.sortingType = InventoryUI.SORTINGTYPE.KEYITEM;
        InventoryUI.instance.updateUI();

    }

    public void ConsommablePressed()
    {
        InventoryUI.instance.sortingType = InventoryUI.SORTINGTYPE.CONSOMMABLE;
        InventoryUI.instance.updateUI();

    }

    public void ArmorPressed()
    {
        InventoryUI.instance.sortingType = InventoryUI.SORTINGTYPE.ARMOR;
        InventoryUI.instance.updateUI();

    }


}
