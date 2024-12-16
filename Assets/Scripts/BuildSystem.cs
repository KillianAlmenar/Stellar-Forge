using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject cameraHead;
    [SerializeField] private GameObject cube;
    [SerializeField] private float distance;
    [SerializeField] private Material hologramMat;


    private void Update()
    {
        
        Vector3 cubePos = getDistanceNearObject();
        //cube.transform.position = cubePos;
    }

    Vector3 getDistanceNearObject()
    {
        RaycastHit[] hit = Physics.RaycastAll(cameraHead.transform.position, cameraHead.transform.forward, 5);
        if(hit.Length > 0)
        {
            if (hit[0].transform.tag == "Module Slot")
            {
                Debug.Log("aaaa");
            }
            return hit[0].point;
        }
        else
        {
            return cameraHead.transform.position + cameraHead.transform.forward * distance;
        }

    }

}
