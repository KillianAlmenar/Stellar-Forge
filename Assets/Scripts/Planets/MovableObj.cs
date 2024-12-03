using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovableObj : MonoBehaviour
{
    protected Rigidbody rb;
    [SerializeField] private bool isOrbital = false;
    [SerializeField] private bool isEllipsal = false;
    [HideInInspector] public GameObject PlanetReference;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }

    private void Start()
    {
        if (isOrbital)
        {
            Vector3 directionToPlanet = PlanetReference.transform.position - transform.position;
            float distance = directionToPlanet.magnitude;

            if (PlanetReference.GetComponent<CelestialBody>().exponent == 1)
            {
                distance = 1;
            }

            float orbitalSpeed = Mathf.Sqrt(GameManager.instance.gravitationalConstant * PlanetReference.GetComponent<Rigidbody>().mass / distance);

            Vector3 tangentialDirection = Vector3.Cross(directionToPlanet.normalized, Vector3.up).normalized;

            Vector3 initialSpeed = tangentialDirection * orbitalSpeed;

            if(isEllipsal)
            {
                initialSpeed /= 1.15f;
            }

            rb.AddForce(initialSpeed, ForceMode.VelocityChange);
        }
    }

    private void Update()
    {
        if (lineRenderer != null && GameManager.instance.Line)
        {
            lineRenderer.widthMultiplier = 200;
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

        }
    }
    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }

    public void SetMove(Vector3 force)
    {
        if(force != Vector3.zero) 
        {
            rb.velocity = force;
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void AddMove(Vector3 force)
    {
        rb.velocity += force;
    }

    public void AddGravity(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ResetMove()
    {
        rb.velocity = Vector3.zero;
    }
}
