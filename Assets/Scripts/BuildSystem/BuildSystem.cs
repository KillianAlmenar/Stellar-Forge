using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private GameObject obj;
    [SerializeField] private float distance;
    [SerializeField] private float buildDistance = 5;
    [SerializeField] private Material hologramMat;
    [SerializeField] private bool isBuilding = false;


    private void Awake()
    {
        if (obj != null)
        {
            foreach (Renderer rend in obj.GetComponentsInChildren<Renderer>())
            {
                rend.material = hologramMat;
            }

            foreach (Collider collide in obj.GetComponentsInChildren<Collider>())
            {
                collide.enabled = false;
            }
        }
    }

    private void Update()
    {
        DetectModuleSlot();
    }

    private void DetectModuleSlot()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit);

        Debug.DrawRay(transform.position, transform.forward, Color.yellow);


        if (hit.transform != null && hit.transform.tag == "Module Slot")
        {
            Debug.Log(hit.transform.name);
        }

    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, distance);
        if (hit.Length > 0)
        {
            if (hit[0].transform.tag == "Module Slot")
            {

            }
            return hit[0].point;
        }
        else
        {
            return cameraHead.transform.position + cameraHead.transform.forward * distance;
        }

    }

}
