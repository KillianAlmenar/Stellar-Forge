using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public GameObject InteractObj;
    [SerializeField] private GameObject LeaveSpaceship;

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

    public void SwitchInteractObject(bool isDetected, string _textToDisplay = "")
    {
        if (!InteractObj.activeSelf && isDetected)
        {
            InteractObj.GetComponentInChildren<TextMeshProUGUI>().text = _textToDisplay;
            InteractObj.SetActive(true);
        }
        else if (InteractObj.activeSelf && !isDetected)
        {
            InteractObj.SetActive(false);
        }
    }

    public void SwitchLeaveSpaceship(bool isDetected)
    {
        if (!LeaveSpaceship.activeSelf && isDetected)
        {
            LeaveSpaceship.SetActive(true);
        }
        else if (LeaveSpaceship.activeSelf && !isDetected)
        {
            LeaveSpaceship.SetActive(false);
        }
    }

}
