using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CelestialBody : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float cutOffAcceleration = 0;
    [SerializeField] private float cutOffRadius = 0;
    [SerializeField] private float SurfAcceleration = 0;
    [SerializeField] private float lowSurfRadius = 0;
    [SerializeField] private float upperSurfRadius = 0;
    [SerializeField] public int exponent = 1;
    [SerializeField] private float alignRadius = 0;
    [HideInInspector] public float mass = 0;
    public float lightDistance = 1;

    [Header("References")]
    [SerializeField] public List<GameObject> objAttracked = new List<GameObject>();
    [SerializeField] private GameObject lightSource;

    private void Start()
    {
        mass = GetComponent<Rigidbody>().mass;
        objAttracked.Add(GameManager.instance.Player);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        SetSunLight();
    }

    private void ApplyGravity()
    {
        foreach (var obj in objAttracked)
        {
            float acceleration = CheckDistance(obj);
            Vector3 distance = transform.position - obj.transform.position;

            Vector3 gravity = distance.normalized * acceleration;

            if (obj == GameManager.instance.Player)
            {

                if (obj.GetComponent<PlayerPhysics>().onPlanet)
                {
                    distance = obj.GetComponent<PlayerPhysics>().planetCollisionPoint - obj.transform.position;
                }

                Vector3 gravityPlayer = obj.GetComponent<PlayerPhysics>().forceMultiplier * distance.normalized * acceleration;

                obj.GetComponent<MovableObj>().Rigidbody.AddForce(gravityPlayer, ForceMode.Acceleration);

            }
            else
            {
                obj.GetComponent<MovableObj>().Rigidbody.AddForce(gravity, ForceMode.Acceleration);
            }
        }

    }

    private void SetSunLight()
    {
        if(lightSource != null)
        {
            Vector3 sunDir = GameManager.instance.Sun.transform.position - transform.position;

            lightDistance = 200 * transform.localScale.x + 250;

            lightSource.transform.position = transform.position + sunDir.normalized * lightDistance;
        }
    }

    public float CheckDistance(GameObject obj)
    {
        float distance = (transform.position - obj.transform.position).magnitude;

        //Setup gravity for each radius
        if (distance < cutOffRadius)
        {
            return cutOffAcceleration;
        }
        else if (distance < lowSurfRadius)
        {
            //Interpolate gravity from less to higher depending on the distance to the planet
            return (distance - cutOffRadius) * SurfAcceleration / (lowSurfRadius - cutOffRadius);

        }
        else if (distance < upperSurfRadius)
        {
            return SurfAcceleration;
        }
        else
        {
            if (distance < alignRadius && obj == GameManager.instance.Player)
            {
                obj.GetComponent<PlayerPhysics>().AlignPlayer(transform);
            }

            //attenuation exponent of gravity
            for (int i = 0; i < exponent - 1; i++)
            {
                distance *= distance;
            }

            float acceleration = GameManager.instance.gravitationalConstant * mass / distance;


            //acceleration formule for gravity
            return acceleration;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, cutOffRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lowSurfRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, upperSurfRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alignRadius);

    }

}