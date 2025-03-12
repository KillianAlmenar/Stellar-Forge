using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SFPhysics : MovableObj
{
    public bool onPlanet = false;
    [HideInInspector] public float timeWithoutAlign = 1;
    [HideInInspector] public Vector3 planetNormal;
    [SerializeField] protected float alignSpeed = 1;
    [HideInInspector] protected bool isAlign = false;
    public float forceMultiplier = 1;
    [SerializeField] private float speed = 0;
    [HideInInspector] public Vector3 planetCollisionPoint;
    public bool onStation = false;
    public bool stationNear = false;

    protected void Update()
    {
        timeWithoutAlign += Time.deltaTime;
        speed = rb.velocity.magnitude;
    }

    public void AlignTarget(Transform target)
    {
        timeWithoutAlign = 0;
        Vector3 alignmentVector;

        if (!onPlanet)
        {
            alignmentVector = target.position - transform.position;
        }
        else if (planetNormal != null)
        {
            alignmentVector = -planetNormal;

        }
        else
        {
            return;
        }

        Quaternion targetRotation = Quaternion.FromToRotation(-transform.up, alignmentVector.normalized) * transform.rotation;

        if (-transform.up != alignmentVector)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, alignSpeed * Time.deltaTime);
        }

        isAlign = Quaternion.Angle(transform.rotation, targetRotation) < 0.1f;
    }

}

