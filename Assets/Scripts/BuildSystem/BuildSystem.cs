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
    public bool asInitBuildable = false;
    private bool canBuild = false;
    public GameObject selectedBuildable;
    public GameObject buildHolo;
    private Color HoloColor = Color.red;
    [SerializeField] private ModuleSlot slot;
    private int currentHoloRotation = 0;
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
                buildHolo.transform.tag = "Module Holo";
                HoloColor = Color.red;
                foreach (Renderer rend in buildHolo.GetComponentsInChildren<Renderer>())
                {
                    rend.material = hologramMat;
                    rend.material.SetColor("_FresnelColor", Color.red);
                }

                foreach (Collider collide in buildHolo.GetComponentsInChildren<Collider>())
                {
                    if (!collide.isTrigger)
                    {
                        collide.enabled = false;
                    }
                }
                asInitBuildable = true;
            }

            buildHolo.transform.position = getDistanceNearObject();
            DetectModuleSlot();
            checkCanBuild();
        }
    }

    private void DetectModuleSlot()
    {
        RaycastHit[] hitAll = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, buildDistance);

        foreach (RaycastHit hit in hitAll)
        {
            if (hit.transform.CompareTag("Module Slot"))
            {
                if (slot != hit.transform.GetComponent<ModuleSlot>())
                {
                    slot = hit.transform.GetComponent<ModuleSlot>();
                }
                return;

            }
        }

        if (slot != null)
        {
            slot = null;
        }
    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, buildDistance);

        if (slot != null)
        {
            Transform slotAnchor = slot.GetNearestPoint(buildHolo.transform.position);

            if (slotAnchor != null)
            {
                Transform myAnchor = buildHolo.GetComponent<ModuleSlot>().GetNearestPoint(slotAnchor.position);

                SetRotationToAnchor(myAnchor, slotAnchor);

                return slotAnchor.position + (buildHolo.transform.position - myAnchor.position);
            }
        }

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.CompareTag("Module Holo"))
            {
                continue;
            }
            else
            {
                return hit[i].point;
            }
        }
        return cameraHead.transform.position + cameraHead.transform.forward * buildDistance;

    }

    private void SetRotationToAnchor(Transform _myTransform, Transform otherAnchor)
    {
        Vector3 myForward = _myTransform.forward;
        Vector3 targetDirection = -otherAnchor.forward;

        float targetAngle = Vector3.SignedAngle(myForward, targetDirection, Vector3.up);

        currentHoloRotation += Mathf.RoundToInt(targetAngle);

        buildHolo.transform.localEulerAngles = new Vector3(0, currentHoloRotation % 360, 0);
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

    private void checkCanBuild()
    {
        if (isBuilding && slot != null && slot.anchors.Count > 0 && !buildHolo.GetComponent<ModuleSlot>().playerInModule)
        {
            SwapHoloColor(Color.blue);
            canBuild = true;
        }
        else if(canBuild)
        {
            SwapHoloColor(Color.red);
            canBuild = false;
        }
    }

    private void OnBuildPressed(InputAction.CallbackContext ctx)
    {
        if (canBuild)
        {
            slot.PlaceBuildable(selectedBuildable, currentHoloRotation, buildHolo.transform.position);

            isBuilding = false;
            canBuild= false;
            Destroy(buildHolo);
        }
    }

    private void RotateBuildPressed(InputAction.CallbackContext ctx)
    {
        if (isBuilding)
        {
            currentHoloRotation += (int)(90 * (ctx.ReadValue<float>() / 120));
            buildHolo.transform.localEulerAngles = new Vector3(0, currentHoloRotation % 360, 0);
        }
    }

    public void ResetBuildable()
    {
        if (buildHolo)
        {
            Destroy(buildHolo);
        }
    }

}