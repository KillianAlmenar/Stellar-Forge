using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiInput : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed += OnCloseInventoryPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed -= OnCloseInventoryPerformed;
    }

    private void OnCloseInventoryPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.UI.Disable();
        GameManager.instance.gameInput.Player.Enable();
        InventoryUI.instance.isDisplay = false;
    }
}
