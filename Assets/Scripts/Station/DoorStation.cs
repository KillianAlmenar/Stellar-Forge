using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorStation : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    [SerializeField] private float radius = 1;
    float openTimer = 0;
    public bool DoorOpen = false;

    private void Update()
    {
        if(DoorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            openTimer = 0;

            DoorOpen = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            openTimer = 0;
            DoorOpen = false;
        }
    }

    private void OpenDoor()
    {
        if (LeftDoor.transform.localPosition != new Vector3(2.5f, 0, 0))
        {
            openTimer += Time.deltaTime;
            LeftDoor.transform.localPosition = Vector3.Lerp(LeftDoor.transform.localPosition, new Vector3(2.5f, 0, 0), openTimer);
            RightDoor.transform.localPosition = Vector3.Lerp(RightDoor.transform.localPosition, new Vector3(-2.75f, 0, 0), openTimer);
        }
    }


    private void CloseDoor()
    {
        if (LeftDoor.transform.localPosition != new Vector3(1.4f, 0, 0))
        {
            openTimer += Time.deltaTime;
            LeftDoor.transform.localPosition = Vector3.Lerp(LeftDoor.transform.localPosition, new Vector3(1.4f, 0, 0), openTimer);
            RightDoor.transform.localPosition = Vector3.Lerp(RightDoor.transform.localPosition, new Vector3(-1.4f, 0, 0), openTimer);
        }
    }

}
