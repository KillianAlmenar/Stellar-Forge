using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public List<GameObject> UniversalObject;
    public GameObject Sun;
    public GameInput gameInput;
    public bool Line = false;
    [SerializeField]public float gravitationalConstant = 10f;
    public bool onShip = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameInput = new GameInput();
        gameInput.Enable();
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