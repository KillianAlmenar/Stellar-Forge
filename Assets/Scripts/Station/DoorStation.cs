using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorStation : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public DoorArea OpeningArea;
    public DoorArea ClosingArea;
    [HideInInspector] public float openTimer = 0;
    public bool DoorOpen = false;

    private void Update()
    {
        if (OpeningArea.playerDetected)
        {
            if (!DoorOpen)
            {
                DoorOpen = true;
            }
            OpenDoor();
        }
        else if (!ClosingArea.playerDetected)
        {
            if (DoorOpen)
            {
                DoorOpen = false;
            }
            CloseDoor();
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
