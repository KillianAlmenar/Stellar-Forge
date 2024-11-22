using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private GameObject head;

    void Update()
    {
        CheckSpaceship();
    }

    private void CheckSpaceship()
    {
        RaycastHit[] raycasts = Physics.RaycastAll(head.transform.position, head.transform.forward, 10);

        foreach (RaycastHit raycast in raycasts)
        {
            if(raycast.transform.tag == "Spaceship")
            {

            }
        }

    }

}
