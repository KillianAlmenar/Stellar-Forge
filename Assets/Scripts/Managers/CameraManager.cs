using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] public CinemachineVirtualCamera spaceShipCam;
    public static CameraManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SwitchSpaceshipCam()
    {
        playerCam.enabled = !GameManager.instance.onShip;
        spaceShipCam.enabled = GameManager.instance.onShip;
    }
}
