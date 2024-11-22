using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGamePressed()
    {
        SceneManager.LoadScene("WorkingPlace");
    }

    public void ContinuePressed()
    {

    }

    public void SettingsPressed()
    {

    }

    public void CreditPressed()
    {

    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
