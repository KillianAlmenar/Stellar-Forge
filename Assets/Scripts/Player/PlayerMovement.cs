using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MovableObj moveScript;
    private PlayerPhysics physicsScript;
    [SerializeField] private float speedOnPlanet = 0;
    [SerializeField] private float speedOnEquiped = 1;
    [SerializeField] private float speedCombiUpDown = 1;
    [SerializeField] public float jumpForce = 0;
    [SerializeField] public float rotationForce = 0;
    [SerializeField] public float camRotForce = 0;
    private Vector3 rotationVec = Vector3.zero;
    private Vector2 moveVec = Vector2.zero;
    private Vector2 camVec = Vector2.zero;
    [SerializeField] private GameObject head;
    private Rigidbody rb;

    private Vector3 jumpVector = Vector3.zero;

    [SerializeField] private bool isEquiped = false;
    [HideInInspector] public bool isMoving = false;

    private bool up = false;
    private bool down = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveScript = GetComponent<MovableObj>();
        physicsScript = GetComponent<PlayerPhysics>();
    }

    private void OnEnable()
    {
        GameManager.instance.gameInput.Player.Move.performed += OnMovePerformed;
        GameManager.instance.gameInput.Player.Move.canceled += OnMoveCanceled;
        GameManager.instance.gameInput.Player.Jump.performed += OnJumpPerformed;
        GameManager.instance.gameInput.Player.Camera.performed += OnCameraPerformed;
        GameManager.instance.gameInput.Player.Camera.canceled += OnCameraCanceled;
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
        GameManager.instance.gameInput.Player.Camera.performed -= OnCameraPerformed;
        GameManager.instance.gameInput.Player.Camera.canceled -= OnCameraCanceled;
        GameManager.instance.gameInput.Player.Up.performed -= OnUpPerformed;
        GameManager.instance.gameInput.Player.Down.performed -= OnDownPerformed;
        GameManager.instance.gameInput.Player.Up.canceled -= OnUpCanceled;
        GameManager.instance.gameInput.Player.Down.canceled -= OnDownCanceled;
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

            if (physicsScript.jumpBtn)
            {
                jumpVector = transform.up * jumpForce;
                move += jumpVector;
            }

            move += physicsScript.PlanetReference.GetComponent<Rigidbody>().velocity; ;

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
            physicsScript.jumpBtn = false;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveVec = ctx.ReadValue<Vector2>();
        isMoving = true;
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveVec = Vector2.zero;
        if (physicsScript.onPlanet)
        {
            moveScript.ResetMove();
        }
        isMoving = false;

    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (physicsScript.onPlanet)
        {
            physicsScript.jumpBtn = true;
        }
    }

    private void OnCameraPerformed(InputAction.CallbackContext ctx)
    {
        rotationVec = new Vector3(0, ctx.ReadValue<Vector2>().x * rotationForce, 0);

        camVec = new Vector3(-ctx.ReadValue<Vector2>().y * camRotForce, 0, 0);

    }

    private void OnCameraCanceled(InputAction.CallbackContext ctx)
    {
        rotationVec = Vector3.zero;
        camVec = Vector3.zero;
    }

    private void OnUpPerformed(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            up = true;
        }

    }

    private void OnUpCanceled(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            up = false;
        }

    }

    private void OnDownPerformed(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            down = true;
        }

    }

    private void OnDownCanceled(InputAction.CallbackContext ctx)
    {
        if (isEquiped)
        {
            down = false;
        }

    }

}
