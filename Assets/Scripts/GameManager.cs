using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public GameObject Player;
    public GameObject Sun;
    public GameInput gameInput;
    public bool Line = false;
    [SerializeField]public float gravitationalConstant = 10f;

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

}
