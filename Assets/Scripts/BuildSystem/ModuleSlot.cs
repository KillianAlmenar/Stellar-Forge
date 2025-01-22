using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField] List<Transform> transforms = new List<Transform>();
    [SerializeField] List<Transform> transformsTaken;
    [SerializeField] GameObject station;

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
        GameObject buildableObj = Instantiate(obj, _pos, Quaternion.Euler(0, _rotation, 0), station.transform);
        buildableObj.GetComponent<ModuleSlot>().transforms.Remove(buildableObj.GetComponent<ModuleSlot>().GetNearestPoint(transform.position));

        switch (type)
        {
            case TYPE.CORRIDOR:
                transforms.Remove(GetNearestPoint(_pos));
               // AddTransformTaken(GetNearestPoint(_pos));
                break;
            case TYPE.DESTROYABLE:
                Destroy(gameObject);
                break;
        }
    }

    public Transform GetNearestPoint(Vector3 point)
    {
        Transform nearestPoint = null;
        float minDistance = float.MaxValue;

        switch (type)
        {
            case TYPE.CORRIDOR:

                foreach (Transform t in transforms)
                {
                    if (transformsTaken.Contains(t))
                    {
                        continue;
                    }
                    float distance = (t.position - point).sqrMagnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPoint = t;
                    }
                }
                return nearestPoint;
            case TYPE.DESTROYABLE:

                return transforms[0];
            default:
                return transforms[0];
        }
    }

    public void AddTransformTaken(Transform _pos)
    {
        transformsTaken.Add(_pos);
    }

}
