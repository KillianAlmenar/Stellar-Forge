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
    [SerializeField] public float gravitationalConstant = 10f;
    public bool onShip = false;
    public GameObject Player;
    public bool onKeyboard = true;
    public PlayerInventoryUI playerInventoryUI;
    public OtherInventoryUI otherInventoryUI;

    private void Awake()
    {
        if (instance == null)
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
        gameInput.Spaceship.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 10;
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale = 1;
        }
    }

}
