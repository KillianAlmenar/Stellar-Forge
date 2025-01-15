using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField] private float speedOnPlanet = 0;

    [SerializeField] public float jumpForce = 0;

    [HideInInspector] public bool jumpBtn = false;

    private Vector3 jumpVector = Vector3.zero;

    [SerializeField] private GameObject head;

    private Vector3 targetRotationVec = Vector3.zero;
    private Vector3 targetCamVec = Vector3.zero;
    [SerializeField] private float rotationDamping = 10f;
    private BuildSystem buildSystem;

    private void Start()
    {
        moveScript = GetComponent<MovableObj>();
        physicsScript = GetComponent<PlayerPhysics>();
        buildSystem = GetComponent<BuildSystem>();
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
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

        transform.Rotate(rotationVec * speedMultiplier);

        if (physicsScript.onPlanet || physicsScript.timeWithoutAlign < 1)
        {
            head.transform.Rotate(camVec * speedMultiplier);

            if (head.transform.localRotation.eulerAngles.x > 100 || head.transform.localRotation.eulerAngles.x <= 30)
            {
                head.transform.Rotate(-camVec * speedMultiplier);

            }
        }
        else if (isEquiped)
        {
            head.transform.localRotation = new Quaternion(0.6f, 0, 0, 1);
            transform.Rotate(camVec * speedMultiplier);
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
            jumpBtn = false;
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
        if (physicsScript.onPlanet && !physicsScript.onStation && !buildSystem.isBuilding)
        {
            jumpBtn = true;
        }
    }

}
