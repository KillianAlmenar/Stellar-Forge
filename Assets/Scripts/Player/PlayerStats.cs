using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float oxygen = 100;
    [SerializeField] public float oxygenMax = 100;
    [SerializeField] public float fuel = 100;
    [SerializeField] public float fuelMax = 100;
    [SerializeField] private bool treeDetection = false;
    [SerializeField] private bool fuelRefill = false;

    [SerializeField] public float oxygenLost = 0.27f;
    [SerializeField] public float fuelLost = 0.5f;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerPhysics physic;

    private void Update()
    {
        OxygenCheck();
        fuelCheck();

        if(fuelRefill)
        {
            refillFuel();
        }

    }

    private void OxygenCheck()
    {
        if (treeDetection)
        {
            oxygen += 30 * Time.deltaTime;
        }
        else
        {
            oxygen -= oxygenLost * Time.deltaTime;
        }

        if (oxygen > oxygenMax)
        {
            oxygen = oxygenMax;
        }
        else if (oxygen < 0)
        {
            oxygen = 0;
        }

    }

    public void fuelCheck()
    {
        if (movement.isMoving)
        {
            fuel -= fuelLost * Time.deltaTime;
        }

        if (fuel < 0)
        {
            fuel = 0;
        }
    }

    private void refillFuel()
    {
        fuel += 30 * Time.deltaTime;

        if (fuel > fuelMax)
        {
            fuel = fuelMax;
            fuelRefill = false;
        }
    }

}
