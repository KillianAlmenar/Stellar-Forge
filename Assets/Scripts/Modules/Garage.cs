using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Garage : MainModule
{
    [SerializeField] GameObject spaceShip;
    [SerializeField] Transform shipSpawnTransform;

    public override void onSpawn()
    {
        if (GameManager.instance.UniversalObject.Contains(spaceShip))
        {
            foreach (GameObject obj in GameManager.instance.UniversalObject)
            {
                if (obj == spaceShip)
                {
                    obj.transform.position = shipSpawnTransform.position;
                    obj.transform.rotation = shipSpawnTransform.rotation;
                }
            }
        }
        else
        {
            GameObject spaceship = Instantiate(spaceShip, shipSpawnTransform.position, shipSpawnTransform.rotation);
            CameraManager.instance.spaceShipCam = spaceship.GetComponentInChildren<CinemachineVirtualCamera>();
            spaceship.GetComponent<SpaceshipPhysics>().onStation = true;
            GameManager.instance.UniversalObject.Add(spaceship);
            GameManager.instance.onUniversalObjectChange?.Invoke();
        }
    }

}