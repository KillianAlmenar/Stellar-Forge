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

    private void Update()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, radius, transform.up, radius);

        foreach(RaycastHit hit2 in hit)
        {
            if(hit2.transform.tag == "Player")
            {
                OpenDoor();
            }
        }

    }

    private void OpenDoor()
    {
        openTimer += Time.deltaTime;
        LeftDoor.transform.localPosition = Vector3.Lerp(LeftDoor.transform.localPosition, new Vector3(2.5f, 0, 0), openTimer);
        RightDoor.transform.localPosition = Vector3.Lerp(RightDoor.transform.localPosition, new Vector3(-2.75f, 0, 0), openTimer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
