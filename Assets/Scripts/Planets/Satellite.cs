using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    Vector3 sunPos;
    Vector3 parentPos;

    [SerializeField] private float offSet = 0;

    private void Awake()
    {
        sunPos = GameManager.instance.Sun.transform.position;
        parentPos = transform.parent.position; 
        Vector3 sunParentVector = parentPos - sunPos;

        transform.position = parentPos + (sunParentVector.normalized * transform.parent.localScale.x / 2) + (sunParentVector.normalized * offSet); 

    }

}
