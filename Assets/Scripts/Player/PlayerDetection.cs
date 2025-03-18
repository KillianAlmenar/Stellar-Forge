using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (physicsScript.startOnStation)
        {
            DetectStation();
        }
    }

    void Update()
    {
        CheckObject();
        if (!physicsScript.onStation)
        {
            DetectStation();
        }
    }

    private void CheckObject()
    {
        RaycastHit raycasts;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out raycasts, 10) && raycasts.collider != null && raycasts.collider.gameObject.GetComponent<IInteractable>() != null)
        {
            if (!interactSwitch)
            {
                interactable = raycasts.collider.gameObject.GetComponent<IInteractable>();
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

        bool stationHit = false;

        foreach(RaycastHit hit in hits) 
        {
            if (hit.transform.root.CompareTag("Station"))
            {
                physicsScript.PlanetReference = hit.transform.root.gameObject;
                physicsScript.stationNear = true;
                stationHit = true;
                break;
            }
        }

        if(!stationHit)
        {
            physicsScript.PlanetReference = null;
            physicsScript.stationNear = false;
        }

    }
    
    private void InteractPerformed(InputAction.CallbackContext ctx)
    {
        if (interactable != null)
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