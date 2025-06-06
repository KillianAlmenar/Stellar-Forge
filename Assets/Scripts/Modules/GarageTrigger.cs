using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        SpaceshipPhysics spaceshipPhysics = other.GetComponent<SpaceshipPhysics>();

        if (spaceshipPhysics != null)
        {
            spaceshipPhysics.onStation = false;
            spaceshipPhysics.onPlanet = false;
        }
    }
}
