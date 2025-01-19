using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField] List<Transform> transforms = new List<Transform>();
    [SerializeField] GameObject station;
    Vector3 nearestPoint = Vector3.positiveInfinity;

    public enum TYPE
    {
        CORRIDOR,
        DESTROYABLE
    }
    public TYPE type;

    private void Start()
    {
        station = GameObject.Find("Station");
    }

    public void PlaceBuildable(GameObject obj, float _rotation, Vector3 _pos)
    {
        switch (type)
        {
            case TYPE.CORRIDOR:
                Instantiate(obj, _pos, Quaternion.Euler(0, _rotation, 0), station.transform);
                break;
            case TYPE.DESTROYABLE:
                Instantiate(obj, _pos, Quaternion.Euler(0, _rotation,0), station.transform);
                Destroy(gameObject);
                break;
        }
    }

    public Vector3 GetNearestPoint(Vector3 point)
    {
        nearestPoint = Vector3.positiveInfinity;
        float minDistance = float.MaxValue;

        switch (type)
        {
            case TYPE.CORRIDOR:

                foreach (Transform t in transforms)
                {
                    float distance = (t.position - point).sqrMagnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPoint = t.position;
                    }
                }

                return nearestPoint;
            case TYPE.DESTROYABLE:
                return transforms[0].position;
            default:
                return Vector3.zero;

        }

    }

}
