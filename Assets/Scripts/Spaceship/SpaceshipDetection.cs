using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipDetection : MonoBehaviour
{
    [SerializeField] private float stationRadius = 1;
    SpaceshipPhysics physicsScript;

    private void Start()
    {
        physicsScript = GetComponent<SpaceshipPhysics>();

        DetectStation();
    }

    void Update()
    {
        if (!physicsScript.onStation && !physicsScript.onPlanet)
        {
            DetectStation();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stationRadius);
    }

}