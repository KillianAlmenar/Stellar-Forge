using UnityEngine;

public class PlayerPhysics : SFPhysics
{
    public bool isJumping = false;
    private PlayerMovement move;

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
            if (move.jumpBtn)
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

    private new void Update()
    {
        base.Update();
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