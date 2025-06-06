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
    [SerializeField] GameObject SpaceShip;
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
        if (!physicsScript.onStation && !physicsScript.onPlanet)
        {
            DetectStation();
        }
    }

    private void CheckObject()
    {
        RaycastHit raycasts;

        if (Physics.Raycast(head.transform.position, head.transform.forward, out raycasts, 10))
        {
            if (raycasts.collider != null && raycasts.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                if (!interactSwitch)
                {
                    interactable = raycasts.collider.gameObject.GetComponent<IInteractable>();
                    interactSwitch = true;
                    HUDManager.Instance.SwitchInteractObject(true, "Press left click to interact");
                }

            }
            else if (interactable != null)
            {
                interactable = null;
                interactSwitch = false;
                HUDManager.Instance.SwitchInteractObject(false);
            }

            if (raycasts.transform.CompareTag("Spaceship"))
            {
                SpaceShip = raycasts.transform.gameObject;
                interactSwitch = true;
                HUDManager.Instance.SwitchInteractObject(true, "Press left click to get into the Spaceship");
            }
            else if(SpaceShip != null)
            {
                interactSwitch = false;
                SpaceShip = null;
                HUDManager.Instance.SwitchInteractObject(false);
            }
        }
    }

    private void DetectStation()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, stationRadius, Vector3.forward);

        bool stationHit = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.root.CompareTag("Station"))
            {
                physicsScript.PlanetReference = hit.transform.root.gameObject;
                physicsScript.stationNear = true;
                stationHit = true;
                break;
            }
        }

        if (!stationHit)
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

        if(SpaceShip != null)
        {
            SpaceShip.GetComponent<SpaceshipMovement>().PlayerInteract();
        }

    }

}