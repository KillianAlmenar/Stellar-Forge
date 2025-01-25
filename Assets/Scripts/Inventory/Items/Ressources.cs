using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ressources", menuName = "Item/Create New Ressources")]
public class Ressources : ItemInventory
{
    private new void OnValidate()
    {
        base.OnValidate();
        type = TYPE.RESSOURCE;
    }

}
