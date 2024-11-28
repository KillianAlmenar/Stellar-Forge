using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement : Movement
{
    [HideInInspector] public bool onPlanet = false;
    void Start()
    {
        moveScript = GetComponent<MovableObj>();
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
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {

        transform.Rotate(rotationVec);

        if (isEquiped && !onPlanet)
        {
            transform.Rotate(camVec);
        }

    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        move += moveVec.x * transform.right;
        move += moveVec.y * transform.forward;

        if (up)
        {
            move += transform.up * speedCombiUpDown;
        }

        if (down)
        {
            move -= transform.up * speedCombiUpDown;
        }

        moveScript.AddGravity(move * speedOnEquiped);

    }
}
