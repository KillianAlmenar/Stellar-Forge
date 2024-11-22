using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image oxygenBar;
    [SerializeField] private Image fuelBar;   
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        oxygenBar.fillAmount = stats.oxygen / stats.oxygenMax;
        fuelBar.fillAmount = stats.fuel / stats.fuelMax;
    }

}
