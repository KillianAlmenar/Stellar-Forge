using UnityEngine;

public class PlayerPhysics : SFPhysics
{
    public bool isJumping = false;
    public bool inSpace = false;
    private PlayerMovement move;
    public bool startOnStation = false;
    public float distance = 1;

    private void Start()
    {
        move = GetComponent<PlayerMovement>();
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

        if (onStation)
        {
            if (!Physics.Raycast(transform.position, -transform.up, distance))
            {
                AddGravity(-transform.up);
            }

        }

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
        if (!isJumping)
        {
            inSpace = true;

            if (stationNear && PlanetReference != null)
            {
                rb.velocity = PlanetReference.GetComponent<Rigidbody>().velocity;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            rb.constraints = RigidbodyConstraints.FreezeRotation;
            alignSpeed = 0.5f;
            move.stationVec = Vector3.zero;
        }
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

    //Spawn the player when leaving Spaceship

    public void SpawnPlayer(bool _onStation, Vector3 spawnPos)
    {
        gameObject.SetActive(true);
        transform.position = spawnPos;
        onStation = _onStation;
        onPlanet = _onStation;
    }

}