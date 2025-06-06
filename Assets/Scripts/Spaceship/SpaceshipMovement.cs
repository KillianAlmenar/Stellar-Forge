using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement : Movement
{
    [SerializeField] private Transform playerSpawn;
    void Start()
    {
        physicsScript = GetComponent<SpaceshipPhysics>();
    }

    private void OnEnable()
    {
        GameManager.instance.gameInput.Spaceship.Move.performed += OnMovePerformed;
        GameManager.instance.gameInput.Spaceship.Move.canceled += OnMoveCanceled;
        GameManager.instance.gameInput.Spaceship.Up.performed += OnUpPerformed;
        GameManager.instance.gameInput.Spaceship.Up.canceled += OnUpCanceled;
        GameManager.instance.gameInput.Spaceship.Down.performed += OnDownPerformed;
        GameManager.instance.gameInput.Spaceship.Down.canceled += OnDownCanceled;
        GameManager.instance.gameInput.Spaceship.Rotate.performed += OnRotatePerformed;
        GameManager.instance.gameInput.Spaceship.Rotate.canceled += OnRotateCanceled;
        GameManager.instance.gameInput.Spaceship.Leave.performed += OnLeavePerformed;

    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Spaceship.Move.performed -= OnMovePerformed;
        GameManager.instance.gameInput.Spaceship.Move.canceled -= OnMoveCanceled;
        GameManager.instance.gameInput.Spaceship.Up.performed -= OnUpPerformed;
        GameManager.instance.gameInput.Spaceship.Up.canceled -= OnUpCanceled;
        GameManager.instance.gameInput.Spaceship.Down.performed -= OnDownPerformed;
        GameManager.instance.gameInput.Spaceship.Down.canceled -= OnDownCanceled;
        GameManager.instance.gameInput.Spaceship.Rotate.performed -= OnRotatePerformed;
        GameManager.instance.gameInput.Spaceship.Rotate.canceled -= OnRotateCanceled;
        GameManager.instance.gameInput.Spaceship.Leave.performed -= OnLeavePerformed;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        float speedMultiplier;

        if (GameManager.instance.onKeyboard)
        {
            speedMultiplier = Settings.instance.mouseSensitivity;
        }
        else
        {
            speedMultiplier = Settings.instance.gamepadSensitivity;
        }

        if (!physicsScript.onPlanet && !physicsScript.onStation)
        {
            transform.Rotate(rotationVec * speedMultiplier);
            transform.Rotate(camVec * speedMultiplier);
        }

    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        if (physicsScript.stationNear || (physicsScript.onPlanet && !physicsScript.onStation))
        {
            rb.velocity = physicsScript.PlanetReference.GetComponent<Rigidbody>().velocity;
        }

        if (!physicsScript.onPlanet && !physicsScript.onStation)
        {
            move += moveVec.x * transform.right;
            move += moveVec.y * transform.forward;

            if (down)
            {
                move -= transform.up * speedCombiUpDown;
            }
        }

        if (up)
        {
            move += transform.up * speedCombiUpDown;
        }

        if (physicsScript.stationNear)
        {
            stationVec += move * speedOnEquiped;
            rb.velocity += stationVec;
            physicsScript.AddGravity(stationVec);
        }
        else
        {
            physicsScript.AddGravity(move * speedOnEquiped);
        }


    }

    public void PlayerInteract()
    {
        //Get player into spaceship
        GameManager.instance.onShip = true;
        GameManager.instance.Player.SetActive(false);
        HUDManager.Instance.SwitchInteractObject(false);
        HUDManager.Instance.SwitchLeaveSpaceship(true);
        CameraManager.instance.SwitchSpaceshipCam();
        GameManager.instance.gameInput.Spaceship.Enable();
        GameManager.instance.gameInput.Player.Disable();
    }

    public void OnLeavePerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.onShip = false;


        GameManager.instance.Player.GetComponent<PlayerPhysics>().SpawnPlayer(physicsScript.onStation, playerSpawn.position);

        HUDManager.Instance.SwitchLeaveSpaceship(false);
        CameraManager.instance.SwitchSpaceshipCam();
        GameManager.instance.gameInput.Spaceship.Disable();
        GameManager.instance.gameInput.Player.Enable();
    }
}