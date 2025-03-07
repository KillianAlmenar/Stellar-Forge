using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour , IInteractable
{

    public void Interact()
    {
        if(!GameManager.instance.otherInventoryUI.isDisplay)
        {
            GameManager.instance.otherInventoryUI.isDisplay = true;
            GameManager.instance.playerInventoryUI.isDisplay = true;
            GameManager.instance.gameInput.Player.Disable();
            GameManager.instance.gameInput.UI.Enable();
        }
    }
}
