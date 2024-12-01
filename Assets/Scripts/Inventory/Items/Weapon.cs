using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Create New Weapon")]
public class Weapon : Item
{
    public GameObject weaponVisual;

    private void OnValidate()
    {
        type = TYPE.WEAPON;
    }

    public float weight;


}