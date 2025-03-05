using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public GameObject InteractObj;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SwitchInteractObject(bool isDetected)
    {
        if(!InteractObj.activeSelf && isDetected) 
        {
            InteractObj.SetActive(true);
        }
        else if(InteractObj.activeSelf && !isDetected) 
        {
            InteractObj.SetActive(false);
        }
    }
}
