using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum TYPE
    {
        KEYITEM,
        CONSOMMABLE,
        WEAPON,
        ARMOR
    }
    public TYPE type;

    public enum RARITY
    {
        LEGENDARY,
        RARE,
        COMMUN
    }
    public RARITY rarity;

    public string itemName;
    public string description;
    public Sprite icon;
    public int stackSize;
    public int id;

}
