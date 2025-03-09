using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Garage : MonoBehaviour
{
    [SerializeField] GameObject spaceShip;
    [SerializeField] Transform shipSpawnTransform;

    public void SpawnShip()
    {
        if(GameManager.instance.UniversalObject.Contains(spaceShip))
        {
            foreach(GameObject obj in GameManager.instance.UniversalObject)
            {
                if(obj == spaceShip)
                {
                    obj.transform.position = shipSpawnTransform.position;
                    obj.transform.rotation = shipSpawnTransform.rotation;
                }
            }
        }
        else
        {
            Instantiate(spaceShip, shipSpawnTransform.position, shipSpawnTransform.rotation);
        }
    }
}
