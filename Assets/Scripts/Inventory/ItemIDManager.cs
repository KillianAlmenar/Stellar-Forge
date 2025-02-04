using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIDManager : MonoBehaviour
{
    public static ItemIDManager Instance;
    public int currentID = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int getNextID()
    {
        currentID += 1;
        return currentID;
    }
   
}
