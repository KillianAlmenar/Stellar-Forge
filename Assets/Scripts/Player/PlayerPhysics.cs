using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerPhysics : MovableObj
{
    [SerializeField] public bool onPlanet = false;
    [HideInInspector] public bool jumpBtn = false;
    public bool isJumping = false;

    [HideInInspector] private bool isAlign = false;
    [HideInInspector] public float timeWithoutAlign = 0;
    public float forceMultiplier = 1;
    [SerializeField] private float alignSpeed = 1;

    [HideInInspector] public Vector3 planetNormal;
    [HideInInspector] public Vector3 planetCollisionPoint;
    private PlayerMovement move;
    [SerializeField] private float speed = 0;

    private void Start()
    {
        move = GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CelestialBody>() != null)
        {
            if (!onPlanet)
            {
                PlanetReference = collision.gameObject;
                PlanetSettings();
                onPlanet = true;
            }

            if (isJumping)
            {
                isJumping = false;
            }

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CelestialBody>() != null)
        {
            if (jumpBtn)
            {
                isJumping = true;
            }

            if (onPlanet)
            {
                SpaceSettings();
                onPlanet = false;
            }
        }
    }

    private void FixedUpdate()
    {
        PlanetNormal();
    }

    private void Update()
    {
        timeWithoutAlign += Time.deltaTime;
        speed = rb.velocity.magnitude;
    }

    public void AlignPlayer(Transform target)
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

    private void PlanetSettings()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        alignSpeed = 25;
        forceMultiplier = 10f;
    }

    private void SpaceSettings()
    {
        if (!isJumping)
        {
            rb.constraints = RigidbodyConstraints.None;
            alignSpeed = 0.5f;
        }
        forceMultiplier = 100;
    }

    private void PlanetNormal()
    {
        if (onPlanet)
        {
            if (Physics.RaycastAll(transform.position, -transform.up, 10).Length > 0)
            {
                planetNormal = Physics.RaycastAll(transform.position, -transform.up, 10)[0].normal;
                planetCollisionPoint = Physics.RaycastAll(transform.position, -transform.up, 10)[0].point;
            }

        }
    }


}