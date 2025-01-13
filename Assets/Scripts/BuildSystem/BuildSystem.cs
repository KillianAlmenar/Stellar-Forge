using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
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

        Debug.DrawRay(transform.position, transform.forward, Color.yellow);


        if (hit.transform != null && hit.transform.CompareTag("Module Slot"))
        {
            Debug.Log(hit.transform.name);
        }

    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, buildDistance);
        if (hit.Length > 0)
        {
            foreach(RaycastHit hit2 in hit)
            {
                if(hit2.transform.CompareTag("Module Slot"))
                {
                    return hit2.transform.position;
                }
            }

            return hit[0].point;
        }
        else
        {
            return cameraHead.transform.position + cameraHead.transform.forward * buildDistance;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, getDistanceNearObject());
    }

}
