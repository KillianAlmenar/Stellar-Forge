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
    [SerializeField] private ModuleSlot slot;
    private float currentHoloRotation = 0;
    private Vector3 placePos = Vector3.zero;

    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Build.performed += OnBuildPressed;
        GameManager.instance.gameInput.Player.RotateBuild.performed += RotateBuildPressed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Player.Build.performed -= OnBuildPressed;
        GameManager.instance.gameInput.Player.RotateBuild.performed -= RotateBuildPressed;
    }

    private void Update()
    {
        if (isBuilding)
        {
            if (!asInitBuildable)
            {
                buildHolo = Instantiate(selectedBuildable);
                buildHolo.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
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
    }

    private void DetectModuleSlot()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit);

        if (hit.transform != null && hit.transform.CompareTag("Module Slot"))
        {
            if (slot != hit.transform.GetComponent<ModuleSlot>())
            {
                slot = hit.transform.GetComponent<ModuleSlot>();
            }
        }
        else if (slot != null)
        {
            slot = null;
        }

    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, buildDistance);

        if (slot != null)
        {
            SwapHoloColor(Color.blue);
            Vector3 slotNearPoint = slot.GetNearestPoint(buildHolo.transform.position);

            Vector3 buildNearPoint = buildHolo.GetComponent<ModuleSlot>().GetNearestPoint(slotNearPoint);

            return slotNearPoint + (buildHolo.transform.position - buildNearPoint);
        }

        if (hit.Length > 0)
        {
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

    private void OnBuildPressed(InputAction.CallbackContext ctx)
    {
        if (isBuilding && slot != null)
        {
            slot.PlaceBuildable(selectedBuildable, currentHoloRotation, buildHolo.transform.position);
            isBuilding = false;
            Destroy(buildHolo);
        }
    }

    private void RotateBuildPressed(InputAction.CallbackContext ctx)
    {
        if (isBuilding)
        {
            currentHoloRotation += 90 * (ctx.ReadValue<float>() / 120);
            buildHolo.transform.localEulerAngles = new Vector3(0, currentHoloRotation % 360, 0);
        }

    }

}
