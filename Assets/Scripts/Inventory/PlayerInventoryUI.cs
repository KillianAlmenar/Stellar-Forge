using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryUI : InventoryUI
{
    public bool inChest = false;
    public GameObject TransfertButton;
    private new void Update()
    {
        base.Update();
        isInChest();
    }

    private void isInChest()
    {
        if (inChest && !TransfertButton.activeSelf)
        {
            TransfertButton.SetActive(true);
        }
        else if (!inChest && TransfertButton.activeSelf)
        {
            TransfertButton.SetActive(false);
        }
    }

}
