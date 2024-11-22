using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private Image logo;
    [SerializeField] private float fadeSpeed = 0;
    [SerializeField] private float camRotateSpeed = 0;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject buttons;

    //bool For animation
    private bool fadeOutBlackScreen = false;
    private bool fadeInLogo = false;
    private bool camRotate = false;
    private bool buttonFadeIn = false;
    private bool logoCheck = false;
    private bool camCheck = false;


    float animationTimer = 0;

    private void Start()
    {
        if (blackScreen != null)
        {
            if (!blackScreen.gameObject.activeSelf)
            {
                blackScreen.gameObject.SetActive(true);
                fadeOutBlackScreen = true;
            }
        }

        if (logo != null)
        {
            if (!logo.gameObject.activeSelf)
            {
                logo.gameObject.SetActive(true);
            }
        }

        if (buttons != null)
        {
            if (buttons.gameObject.activeSelf)
            {
                buttons.gameObject.SetActive(false);
            }

            foreach (var text in buttons.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.alpha = 0f;
            }

        }

    }

    private void Update()
    {
        Animation();

        if (fadeOutBlackScreen)
        {
            FadeImage(blackScreen, true);
        }
        else
        {
            FadeImage(blackScreen, false);
        }

        if (fadeInLogo)
        {
            FadeImage(logo, false);
        }
        else
        {
            FadeImage(logo, true);
        }

        if (camRotate)
        {
            mainCamera.transform.Rotate(new Vector3(camRotateSpeed * Time.deltaTime, 0, 0));
        }

        if (buttonFadeIn)
        {
            ButtonFadeIn();
        }

    }

    private void FadeImage(Image _image, bool isOut)
    {
        if (isOut && _image.color.a > 0)
        {
            Color screenColor = _image.color;

            screenColor.a -= Time.deltaTime * fadeSpeed;

            _image.color = screenColor;

        }
        else if (!isOut && _image.color.a < 1)
        {
            Color screenColor = _image.color;

            screenColor.a += Time.deltaTime * fadeSpeed;

            _image.color = screenColor;
        }


    }

    private void ButtonFadeIn()
    {
        if(!buttons.activeSelf)
        {
            buttons.SetActive(true);
        }

        foreach (var text in buttons.GetComponentsInChildren<TextMeshProUGUI>())
        {

            if (text.color.a < 1)
            {
                Color alpha = text.color;
                alpha.a += Time.deltaTime * fadeSpeed;
                text.color = alpha;
            }

        }

    }

    private void Animation()
    {
        if (blackScreen.color.a <= 0 && logo.color.a <= 0 && !fadeInLogo && !logoCheck)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer > 2)
            {
                fadeInLogo = true;
                animationTimer = 0;
            }

        }

        if (logo.color.a >= 1)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer > 2)
            {
                fadeInLogo = false;
                logoCheck = true;
                animationTimer = 0;
            }
        }

        if (blackScreen.color.a <= 0 && logoCheck && mainCamera.transform.eulerAngles.x < 40 && logo.color.a <= 0)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer > 1 && !camRotate)
            {
                camRotate = true;
                animationTimer = 0;
            }

        }
        else if (camRotate)
        {
            if (mainCamera.transform.eulerAngles.x >= 40)
            {
                camCheck = true;
            }
            camRotate = false;
        }

        if (camCheck)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer > 2)
            {
                buttonFadeIn = true;
                animationTimer = 0;
            }

        }

    }

}
