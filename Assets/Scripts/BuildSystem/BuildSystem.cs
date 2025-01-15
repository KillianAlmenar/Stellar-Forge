using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private float buildDistance = 5;
    [SerializeField] private Material hologramMat;
    [SerializeField] public bool isBuilding = false;
    [SerializeField] public bool asInitBuildable = false;
    public GameObject selectedBuildable;
    public GameObject buildHolo;
    private Color HoloColor = Color.red;
    private ModuleSlot slot;

    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Build.performed += OnBuildPressed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Player.Build.performed -= OnBuildPressed;
    }

    private void Update()
    {
        if (isBuilding)
        {
            if (!asInitBuildable)
            {
                buildHolo = Instantiate(selectedBuildable);
                foreach (Renderer rend in buildHolo.GetComponentsInChildren<Renderer>())
                {
                    rend.material = hologramMat;
                    rend.material.SetColor("_FresnelColor", Color.red);
                }

                foreach (Collider collide in buildHolo.GetComponentsInChildren<Collider>())
                {
                    collide.enabled = false;
                }
                asInitBuildable = true;
            }

            buildHolo.transform.position = getDistanceNearObject();
            DetectModuleSlot();
        }
        else
        {

        }

    }

    private void DetectModuleSlot()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit);

        if (hit.transform != null && hit.transform.CompareTag("Module Slot"))
        {
            if(slot != hit.transform.GetComponent<ModuleSlot>())
            {
                slot = hit.transform.GetComponent<ModuleSlot>();
            }
        }

    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, buildDistance);
        if (hit.Length > 0)
        {
            foreach (RaycastHit hit2 in hit)
            {
                if (hit2.transform.CompareTag("Module Slot"))
                {
                    SwapHoloColor(Color.blue);

                    return hit2.transform.position;
                }
            }

            SwapHoloColor(Color.red);
            return hit[0].point;
        }
        else
        {
            SwapHoloColor(Color.red);

            return cameraHead.transform.position + cameraHead.transform.forward * buildDistance;
        }

    }

    private void SwapHoloColor(Color newColor)
    {
        if (HoloColor != newColor)
        {
            foreach (Renderer rend in buildHolo.GetComponentsInChildren<Renderer>())
            {
                rend.material = hologramMat;
                rend.material.SetColor("_FresnelColor", newColor);
            }
            HoloColor = newColor;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, getDistanceNearObject());
    //}

    private void OnBuildPressed(InputAction.CallbackContext ctx)
    {
        if (isBuilding)
        {
            slot.PlaceBuildable(selectedBuildable);
        }
        Debug.Log("aaa");
    }

}
