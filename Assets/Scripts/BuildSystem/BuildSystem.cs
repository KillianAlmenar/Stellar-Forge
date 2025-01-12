using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private float distance;
    [SerializeField] private float buildDistance = 5;
    [SerializeField] private Material hologramMat;
    [SerializeField] public bool isBuilding = false;
    [SerializeField] public bool asInitBuildable = false;
    public GameObject selectedBuildable;
    public GameObject buildHolo;


    private void Update()
    {
        if(isBuilding)
        {
            if (!asInitBuildable)
            {
                buildHolo = Instantiate(selectedBuildable);
                foreach (Renderer rend in buildHolo.GetComponentsInChildren<Renderer>())
                {
                    rend.material = hologramMat;
                }

                foreach (Collider collide in buildHolo.GetComponentsInChildren<Collider>())
                {
                    collide.enabled = false;
                }
                asInitBuildable = true;
            }
            buildHolo.transform.position = getDistanceNearObject();
        }
        else
        {

        }

       // DetectModuleSlot();
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
