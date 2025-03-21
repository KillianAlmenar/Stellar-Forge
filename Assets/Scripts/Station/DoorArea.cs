using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorArea : MonoBehaviour
{
    public bool playerDetected = false;
    public DoorStation Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Door.openTimer = 0;

            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Door.openTimer = 0;

            playerDetected = false;
        }
    }
}
