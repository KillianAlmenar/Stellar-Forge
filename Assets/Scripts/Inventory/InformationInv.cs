using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InformationInv : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI rarity;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private Image icon;

    public void SetInformations(ItemInventory _selectedItem)
    {
        itemName.text = _selectedItem.name;
        description.text = _selectedItem.description;
        icon.sprite = _selectedItem.icon;

        switch(_selectedItem.rarity)
        {
            case ItemInventory.RARITY.COMMUN:
                rarity.text = "Commun item";
                break;
            case ItemInventory.RARITY.RARE:
                rarity.text = "Rare item";
                break;
            case ItemInventory.RARITY.LEGENDARY:
                rarity.text = "Legendary item";
                break;
        }


        if(_selectedItem is Armor armor )
        {
            weight.text = armor.weight.ToString() + " lbs";
        }
        else if(_selectedItem is Weapon weapon)
        {
            weight.text = weapon.weight.ToString() + " lbs";
        }
        else
        {
            weight.text = " ";
        }
    }

}