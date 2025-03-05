using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private GameObject head;
    private bool interactSwitch = false;

    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Interact.performed += InteractPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Player.Interact.performed -= InteractPerformed;
    }

    void Update()
    {
        CheckObject();
    }

    private void CheckObject()
    {
        RaycastHit raycasts;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out raycasts, 10) && raycasts.transform.GetComponent<IInteractable>() != null)
        {
            if (!interactSwitch)
            {
                interactSwitch = true;
                HUDManager.Instance.SwitchInteractObject(true);
            }
        }
        else if (interactSwitch)
        {
            interactSwitch = false;
            HUDManager.Instance.SwitchInteractObject(false);
        }
    }
    
    private void InteractPerformed(InputAction.CallbackContext ctx)
    {

    }

}