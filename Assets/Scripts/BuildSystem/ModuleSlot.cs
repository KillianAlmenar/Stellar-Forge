using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField] List<Transform> transforms = new List<Transform>();
    [SerializeField] GameObject station;
    public enum TYPE
    {
        CORRIDOR,
        DESTROYABLE
    }
    public TYPE type;

    public void PlaceBuildable(GameObject obj)
    {
        switch (type)
        {
            case TYPE.CORRIDOR:
                break;
            case TYPE.DESTROYABLE:
                Instantiate(obj, transforms[0].position, Quaternion.identity, station.transform);
                Destroy(gameObject);
                break;
        }
    }

    public Vector3 GetNearestPoint(Vector3 point)
    {

        return Vector3.zero;
    }

}
