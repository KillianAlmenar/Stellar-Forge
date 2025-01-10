using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiInput : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed += OnCloseInventoryPerformed;
        GameManager.instance.gameInput.UI.Back.performed += OnBackPerformed;
        GameManager.instance.gameInput.Player.Inventory.performed += OnInventoryPerformed;
        GameManager.instance.gameInput.Player.BuildMenu.performed += OnBuildMenuPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed -= OnCloseInventoryPerformed;
        GameManager.instance.gameInput.UI.Back.performed -= OnBackPerformed;
        GameManager.instance.gameInput.Player.Inventory.performed -= OnInventoryPerformed;
        GameManager.instance.gameInput.Player.BuildMenu.performed -= OnBuildMenuPerformed;
    }

    private void OnInventoryPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.Player.Disable();
        GameManager.instance.gameInput.UI.Enable();
        InventoryUI.instance.isDisplay = true;
    }

    private void OnBuildMenuPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.Player.Disable();
        GameManager.instance.gameInput.UI.Enable();
        BuildUI.instance.isDisplay = true;
    }

    private void OnCloseInventoryPerformed(InputAction.CallbackContext ctx)
    {
        if (InventoryUI.instance.isDisplay)
        {
            InventoryUI.instance.isDisplay = false;
        }
        else if(BuildUI.instance.isDisplay)
        {
            BuildUI.instance.isDisplay = false;
        }
        GameManager.instance.gameInput.UI.Disable();
        GameManager.instance.gameInput.Player.Enable();
    }

    private void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        //GameManager.instance.gameInput.UI.Disable();
        //GameManager.instance.gameInput.Player.Enable();
        //InventoryUI.instance.isDisplay = false;
    }
}
