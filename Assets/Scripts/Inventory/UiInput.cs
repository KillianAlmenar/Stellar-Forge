using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiInput : MonoBehaviour
{
    [SerializeField] InventoryUI playerInventoryUI;
    private void OnEnable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed += OnCloseInventoryPerformed;
        GameManager.instance.gameInput.UI.Back.performed += OnBackPerformed;
        GameManager.instance.gameInput.Player.Inventory.performed += OnInventoryPerformed;
        GameManager.instance.gameInput.Player.BuildMenu.performed += OnBuildMenuPerformed;
        GameManager.instance.gameInput.UI.CloseBuildMenu.performed += OnCloseBuildMenuPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.UI.CloseInventory.performed -= OnCloseInventoryPerformed;
        GameManager.instance.gameInput.UI.Back.performed -= OnBackPerformed;
        GameManager.instance.gameInput.Player.Inventory.performed -= OnInventoryPerformed;
        GameManager.instance.gameInput.Player.BuildMenu.performed -= OnBuildMenuPerformed;
        GameManager.instance.gameInput.UI.CloseBuildMenu.performed -= OnCloseBuildMenuPerformed;
    }

    private void OnInventoryPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.Player.Disable();
        GameManager.instance.gameInput.UI.Enable();
        playerInventoryUI.isDisplay = true;
    }

    private void OnBuildMenuPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.Player.Disable();
        GameManager.instance.gameInput.UI.Enable();
        BuildUI.instance.isDisplay = true;

    }
    private void OnCloseBuildMenuPerformed(InputAction.CallbackContext ctx)
    {
        if (BuildUI.instance.isDisplay)
        {
            BuildUI.instance.isDisplay = false;
            GameManager.instance.gameInput.UI.Disable();
            GameManager.instance.gameInput.Player.Enable();
        }
    }

    private void OnCloseInventoryPerformed(InputAction.CallbackContext ctx)
    {
        if (playerInventoryUI.isDisplay)
        {
            playerInventoryUI.isDisplay = false;
            GameManager.instance.gameInput.UI.Disable();
            GameManager.instance.gameInput.Player.Enable();
        }
    }

    private void OnBackPerformed(InputAction.CallbackContext ctx)
    {
        //Check wich menu is open and close it
        if (BuildUI.instance.isDisplay)
        {
            BuildUI.instance.isDisplay = false;
        }
        else if (playerInventoryUI.isDisplay)
        {
            playerInventoryUI.isDisplay = false;
        }
        GameManager.instance.gameInput.UI.Disable();
        GameManager.instance.gameInput.Player.Enable();
    }
}
