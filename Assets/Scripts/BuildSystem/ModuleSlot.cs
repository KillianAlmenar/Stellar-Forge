using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField] List<Transform> transforms = new List<Transform>();
    [SerializeField] GameObject station;
    Vector3 nearestPoint = Vector3.zero;

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

    public void PlaceBuildable(GameObject obj, float _rotation, Vector3 _position)
    {
        switch (type)
        {
            case TYPE.CORRIDOR:
                Instantiate(obj, nearestPoint, Quaternion.Euler(0, _rotation, 0), station.transform);
                break;
            case TYPE.DESTROYABLE:
                Instantiate(obj, transforms[0].position, Quaternion.Euler(0, _rotation,0), station.transform);
                Destroy(gameObject);
                break;
        }
    }

    public Vector3 GetNearestPoint(Vector3 point)
    {
        switch (type)
        {
            case TYPE.CORRIDOR:
                foreach (Transform t in transforms)
                {
                    if((t.position - point).magnitude < (nearestPoint - point).magnitude)
                    {
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
