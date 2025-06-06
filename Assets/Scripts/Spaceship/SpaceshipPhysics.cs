using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPhysics : SFPhysics
{
    public bool inSpace = false;
    private SpaceshipMovement move;
    public bool startOnStation = false;
    public float distance = 1;

    private void Start()
    {
        move = GetComponent<SpaceshipMovement>();
        if (startOnStation)
        {
            StationSettings();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CelestialBody>() != null)
        {
            if (!onPlanet && collision.transform.tag != "Station")
            {
                PlanetReference = collision.gameObject;
                PlanetSettings();

                onPlanet = true;
            }

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CelestialBody>() != null)
        {
            if (onPlanet && !onStation)
            {
                SpaceSettings();
                onPlanet = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (onStation && !rb.useGravity)
        {
            StationSettings();
        }
        else if (!onPlanet && !onStation && !inSpace)
        {
            SpaceSettings();
        }

        PlanetNormal();
    }

    private new void Update()
    {
        base.Update();
    }

    public void StationSettings()
    {
        inSpace = false;
        onPlanet = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        forceMultiplier = 10f;
    }

    public void PlanetSettings()
    {
        rb.interpolation = RigidbodyInterpolation.None;
        inSpace = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        alignSpeed = 25;
        forceMultiplier = 10f;
    }

    public void SpaceSettings()
    {
        rb.interpolation = RigidbodyInterpolation.Interpolate;
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
