using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public List<GameObject> UniversalObject;
    public GameObject Sun;
    public GameInput gameInput;
    public bool Line = false;
    [SerializeField]public float gravitationalConstant = 10f;
    public bool onShip = false;
    public GameObject Player;
    public bool onKeyboard = true;
    public PlayerInventoryUI playerInventoryUI;
    public OtherInventoryUI otherInventoryUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        gameInput = new GameInput();
        gameInput.Enable();
        gameInput.UI.Disable();
    }

    private void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        CheckOnShip();
    }

    private void CheckOnShip()
    {
        if(onShip && gameInput.Player.enabled)
        {
            gameInput.Player.Disable();
            gameInput.Spaceship.Enable();
        }
        else if(!onShip && gameInput.Spaceship.enabled)
        {
            gameInput.Spaceship.Disable();
            gameInput.Player.Enable();
        }
    }

}
