using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    protected Vector3 rotationVec = Vector3.zero;
    protected Vector2 moveVec = Vector2.zero;

    protected SFPhysics physicsScript;
    protected MovableObj moveScript;
    protected Rigidbody rb;

    protected bool up = false;
    protected bool down = false;
    [HideInInspector] public bool isMoving = false;
    [SerializeField] protected bool isEquiped = false;
    [SerializeField] protected float speedOnEquiped = 1;
    [SerializeField] protected float speedCombiUpDown = 1;
    [SerializeField] public float rotationForce = 0;
    [SerializeField] public float camRotForce = 0;
    protected Vector2 camVec = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveVec = ctx.ReadValue<Vector2>();
        isMoving = true;
    }

    protected void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveVec = Vector2.zero;
        if (physicsScript.onPlanet)
        {
            moveScript.ResetMove();
        }
        isMoving = false;

    }

    protected void OnUpPerformed(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            up = true;
        }

    }

    protected void OnUpCanceled(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            up = false;
        }

    }

    protected void OnDownPerformed(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            down = true;
        }

    }

    protected void OnDownCanceled(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            down = false;
        }

    }

    protected void OnRotatePerformed(InputAction.CallbackContext ctx)
    {
        rotationVec = new Vector3(0, ctx.ReadValue<Vector2>().x * rotationForce, 0);

        camVec = new Vector3(-ctx.ReadValue<Vector2>().y * camRotForce, 0, 0);

    }

    protected void OnRotateCanceled(InputAction.CallbackContext ctx)
    {
        rotationVec = Vector3.zero;
        camVec = Vector3.zero;
    }
}
