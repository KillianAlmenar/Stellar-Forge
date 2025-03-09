using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildable", menuName = "Buildable/Create New Buildable")]
public class Buildable : ScriptableObject
{
    public bool isUnlocked = false;
    public Sprite icon;
    public string itemName;
    public string description;
    public GameObject buildableObject;
    public List<ItemInventory> ressources;
}
