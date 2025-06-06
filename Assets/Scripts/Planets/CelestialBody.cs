using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        foreach(GameObject obj in objAttracked)
        {
            if (!GameManager.instance.UniversalObject.Contains(obj))
            {
                obj.GetComponent<MovableObj>().PlanetReference = this.gameObject;
            }
        }

        GameManager.instance.onUniversalObjectChange += UpdateAttrackedObj;
    }

    private void Start()
    {
        mass = GetComponent<Rigidbody>().mass;

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

            if (GameManager.instance.UniversalObject.Contains(obj))
            {

                if (obj.GetComponent<SFPhysics>().onPlanet)
                {
                    distance = obj.GetComponent<SFPhysics>().planetCollisionPoint - obj.transform.position;
                }

                Vector3 gravityPlayer = obj.GetComponent<SFPhysics>().forceMultiplier * distance.normalized * acceleration;

                MovableObj objMovable = obj.GetComponent<MovableObj>();

                if(objMovable is SFPhysics physics && physics.stationNear)
                {

                }
                else
                {
                    objMovable.Rigidbody.AddForce(gravityPlayer, ForceMode.Acceleration);
                }

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
            if (distance < alignRadius && obj == GameManager.instance.UniversalObject.Contains(obj))
            {
                obj.GetComponent<SFPhysics>().AlignTarget(transform);
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

    private void UpdateAttrackedObj()
    {
        foreach (GameObject obj in GameManager.instance.UniversalObject)
        {
            if(!objAttracked.Contains(obj))
            {
                objAttracked.Add(obj);
            }
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
