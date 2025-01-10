using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Create New Weapon")]
public class Weapon : ItemInventory
{
    public GameObject weaponVisual;

    private new void OnValidate()
    {
        base.OnValidate();
        type = TYPE.WEAPON;
    }

    public float weight;


}