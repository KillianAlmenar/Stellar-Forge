using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private GameObject obj;
    [SerializeField] private float distance;
    [SerializeField] private Material hologramMat;

    private void Awake()
    {
        foreach(Renderer rend in obj.GetComponentsInChildren<Renderer>())
        {
            rend.material = hologramMat;
        }

        foreach (Collider collide in obj.GetComponentsInChildren<Collider>())
        {
            collide.enabled = false;
        }

    }

    private void Update()
    {
        Vector3 cubePos = getDistanceNearObject();
        obj.transform.position = cubePos;
    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, distance);
        if(hit.Length > 0)
        {
            if (hit[0].transform.tag == "Module Slot")
            {
                Debug.Log("aa");
            }
            return hit[0].point;
        }
        else
        {
            return cameraHead.transform.position + cameraHead.transform.forward * distance;
        }

    }

}
