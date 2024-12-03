using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField] private float speedOnPlanet = 0;

    [SerializeField] public float jumpForce = 0;

    [HideInInspector] public bool jumpBtn = false;

    private Vector3 jumpVector = Vector3.zero;

    [SerializeField] private GameObject head;

    private void Start()
    {
        moveScript = GetComponent<MovableObj>();
        physicsScript = GetComponent<PlayerPhysics>();
    }

    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Move.performed += OnMovePerformed;
        GameManager.instance.gameInput.Player.Move.canceled += OnMoveCanceled;
        GameManager.instance.gameInput.Player.Jump.performed += OnJumpPerformed;
        GameManager.instance.gameInput.Player.Rotate.performed += OnRotatePerformed;
        GameManager.instance.gameInput.Player.Rotate.canceled += OnRotateCanceled;
        GameManager.instance.gameInput.Player.Up.performed += OnUpPerformed;
        GameManager.instance.gameInput.Player.Down.performed += OnDownPerformed;
        GameManager.instance.gameInput.Player.Up.canceled += OnUpCanceled;
        GameManager.instance.gameInput.Player.Down.canceled += OnDownCanceled;
        GameManager.instance.gameInput.Player.Inventory.performed += OnInventoryPerformed;
    }

    private void OnDisable()
    {
        GameManager.instance.gameInput.Player.Move.performed -= OnMovePerformed;
        GameManager.instance.gameInput.Player.Move.canceled -= OnMoveCanceled;
        GameManager.instance.gameInput.Player.Jump.performed -= OnJumpPerformed;
        GameManager.instance.gameInput.Player.Rotate.performed -= OnRotatePerformed;
        GameManager.instance.gameInput.Player.Rotate.canceled -= OnRotateCanceled;
        GameManager.instance.gameInput.Player.Up.performed -= OnUpPerformed;
        GameManager.instance.gameInput.Player.Down.performed -= OnDownPerformed;
        GameManager.instance.gameInput.Player.Up.canceled -= OnUpCanceled;
        GameManager.instance.gameInput.Player.Down.canceled -= OnDownCanceled;
        GameManager.instance.gameInput.Player.Inventory.performed -= OnInventoryPerformed;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        transform.Rotate(rotationVec);


        if (physicsScript.onPlanet || physicsScript.timeWithoutAlign < 1)
        {
            head.transform.Rotate(camVec);

            if (head.transform.localRotation.eulerAngles.x > 80 || head.transform.localRotation.eulerAngles.x <= 30)
            {
                head.transform.Rotate(-camVec);

            }
        }
        else if (isEquiped)
        {
            transform.Rotate(camVec);
        }
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        if (physicsScript.onPlanet)
        {

            move += moveVec.x * transform.right;
            move += moveVec.y * transform.forward;

            move *= speedOnPlanet;

            if (jumpBtn)
            {
                jumpVector = transform.up * jumpForce;
                move += jumpVector;
            }

            move += physicsScript.PlanetReference.GetComponent<Rigidbody>().velocity;

            moveScript.SetMove(move);
        }
        else if (isEquiped)
        {
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
        else
        {
            jumpBtn = false;
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (physicsScript.onPlanet)
        {
            jumpBtn = true;
        }
    }

    private void OnInventoryPerformed(InputAction.CallbackContext ctx)
    {
        GameManager.instance.gameInput.Player.Disable();
        GameManager.instance.gameInput.UI.Enable();
        InventoryUI.instance.isDisplay = true;
    }

}
