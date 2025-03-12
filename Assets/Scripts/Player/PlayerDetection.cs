using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private GameObject head;
    private bool interactSwitch = false;
    IInteractable interactable;
    [SerializeField] private float stationRadius = 1;
    PlayerPhysics physicsScript;
    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Interact.performed += InteractPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Player.Interact.performed -= InteractPerformed;
    }

    private void Start()
    {
        physicsScript = GetComponent<PlayerPhysics>();
    }

    void Update()
    {
        CheckObject();
        DetectStation();
    }

    private void CheckObject()
    {
        RaycastHit raycasts;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out raycasts, 10) && raycasts.transform.GetComponent<IInteractable>() != null)
        {
            if (!interactSwitch)
            {
                Debug.Log("Chest detect");
                interactable = raycasts.transform.GetComponent<IInteractable>();
                interactSwitch = true;
                HUDManager.Instance.SwitchInteractObject(true);
            }
        }
        else if (interactSwitch)
        {
            interactable = null;
            interactSwitch = false;
            HUDManager.Instance.SwitchInteractObject(false);
        }
    }

    private void DetectStation()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, stationRadius, Vector3.forward);

        foreach(RaycastHit hit in hits) 
        {
            if (hit.transform.root.CompareTag("Station"))
            {
                physicsScript.PlanetReference = hit.transform.root.gameObject;
                physicsScript.stationNear = true;
                break;
            }
        }

    }
    
    private void InteractPerformed(InputAction.CallbackContext ctx)
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stationRadius);
    }

}