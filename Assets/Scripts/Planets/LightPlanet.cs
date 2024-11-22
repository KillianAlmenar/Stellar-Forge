using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlanet : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.parent);
    }
}
