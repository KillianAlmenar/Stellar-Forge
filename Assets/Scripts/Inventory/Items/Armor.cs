using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Create New Armor")]
public class Armor : Item
{
    private void OnValidate()
    {
        type = TYPE.ARMOR;
    }

    public float weight;
}
