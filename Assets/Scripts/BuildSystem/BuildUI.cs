using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    [SerializeField] GameObject BuildMenu;
    [SerializeField] private BuildableDatabase BuildableDatabase;
    [SerializeField] public static BuildUI instance;
    [SerializeField] private GameObject BuildItemUI;
    public bool isDisplay = false;
    [SerializeField] private float MenuRadius = 1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (isDisplay && !BuildMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            BuildMenu.SetActive(true);
            updateUI();
        }
        else if (!isDisplay && BuildMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            DesactivateUI();
            BuildMenu.SetActive(false);
            GameManager.instance.gameInput.UI.Disable();
            GameManager.instance.gameInput.Player.Enable();
        }
    }

    public void updateUI()
    {
        int totalBuildableUnlocked = 0;


        for (int i = 0; i < BuildableDatabase.buildables.Count; i++)
        {
            if (BuildableDatabase.buildables[i].isUnlocked)
            {
                totalBuildableUnlocked++;
            }
        }

        List<Vector3> circlePosition = Utility.CalculateCirclePositions(BuildMenu.transform.position, Screen.height / 4, totalBuildableUnlocked, Utility.Axis.XY);

        for (int i = 0; i < BuildableDatabase.buildables.Count; i++)
        {
            if (BuildableDatabase.buildables[i].isUnlocked)
            {
                GameObject MenuFrame = Instantiate(BuildItemUI, BuildMenu.transform);
                MenuFrame.transform.position = circlePosition[i];
                MenuFrame.GetComponent<SelectBuildable>().buildable = BuildableDatabase.buildables[i];
                MenuFrame.GetComponentsInChildren<Image>()[1].sprite = BuildableDatabase.buildables[i].icon;
            }
        }

    }

    private void DesactivateUI()
    {
        foreach (Transform child in BuildMenu.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
