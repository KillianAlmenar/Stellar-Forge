using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private DoorStation EnterDoor;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (!EnterDoor.DoorOpen && !GameManager.instance.Player.GetComponent<PlayerPhysics>().onStation)
            {
                GameManager.instance.Player.GetComponent<PlayerPhysics>().onStation = true;
            }
            else if (EnterDoor.DoorOpen)
            {                
                PlayerPhysics PlayerPhysics = GameManager.instance.Player.GetComponent<PlayerPhysics>();
                PlayerPhysics.onStation = false;
                PlayerPhysics.onPlanet = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (EnterDoor.DoorOpen)
            {
                GameManager.instance.Player.GetComponent<PlayerPhysics>().onStation = false;
            }
        }
    }
}
