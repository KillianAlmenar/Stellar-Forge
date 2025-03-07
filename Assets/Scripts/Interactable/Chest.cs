using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (!GameManager.instance.otherInventoryUI.isDisplay)
        {
            Inventory currentInv = GetComponent<Inventory>();
            InteractionInvItem currentInteract = GameManager.instance.otherInventoryUI.interaction.GetComponent<InteractionInvItem>();
            GameManager.instance.otherInventoryUI.Inventory = currentInv;
            GameManager.instance.otherInventoryUI.isDisplay = true;
            currentInteract.currentInv = currentInv;
            currentInteract.targetInv = GameManager.instance.Player.GetComponent<Inventory>();

            GameManager.instance.playerInventoryUI.isDisplay = true;
            GameManager.instance.playerInventoryUI.inChest = true;
            GameManager.instance.playerInventoryUI.interaction.GetComponent<InteractionInvItem>().targetInv = currentInv;

            GameManager.instance.gameInput.Player.Disable();
            GameManager.instance.gameInput.UI.Enable();
        }
    }
}
