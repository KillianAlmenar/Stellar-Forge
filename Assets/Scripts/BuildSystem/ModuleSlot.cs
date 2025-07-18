using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField] public List<Transform> anchors = new List<Transform>();
    [SerializeField] List<Transform> transformsTaken;
    [SerializeField] GameObject station;
    [HideInInspector] public bool playerInModule = false;
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
        //Destroy other anchor attach to me
        GameObject buildableObj = Instantiate(obj, _pos, Quaternion.Euler(0, _rotation, 0), station.transform);
        Transform otherAnchorToDestroy = buildableObj.GetComponent<ModuleSlot>().GetNearestPoint(transform.position);
        buildableObj.GetComponent<ModuleSlot>().anchors.Remove(otherAnchorToDestroy);
        buildableObj.tag = "Module Slot";
        Destroy(otherAnchorToDestroy.gameObject);

        if (buildableObj.GetComponent<MainModule>() != null)
        {
            buildableObj.GetComponent<MainModule>().onSpawn();
        }


        switch (type)
        {
            case TYPE.CORRIDOR:
                //Disable my anchor
                Transform anchorToDestroy = GetNearestPoint(_pos);
                anchorToDestroy.gameObject.SetActive(false);
                anchors.Remove(anchorToDestroy);
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

                foreach (Transform t in anchors)
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

                return anchors[0];
            default:
                return anchors[0];
        }
    }

    public void AddTransformTaken(Transform _pos)
    {
        transformsTaken.Add(_pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerInModule)
            {
                //playerInModule = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInModule)
            {
                playerInModule = false;
            }
        }
    }

}
