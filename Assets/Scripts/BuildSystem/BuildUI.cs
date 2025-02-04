using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BuildUI : MonoBehaviour
{
    [SerializeField] GameObject BuildMenu;
    [SerializeField] private BuildableDatabase BuildableDatabase;
    [SerializeField] public static BuildUI instance;
    [SerializeField] private GameObject BuildItemUI;
    [SerializeField] private GameObject InformationContent;
    [SerializeField] private GameObject InformationObj;
    [SerializeField] private GameObject RessourceIconObj;
    public bool isDisplay = false;
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
        CheckSwitchState();
        CheckCursorOnButton();
    }

    private void CheckSwitchState()
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

    private void CheckCursorOnButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool isFind = false;

        foreach (RaycastResult r in results)
        {
            SelectBuildable selectBuildable = r.gameObject.GetComponent<SelectBuildable>();
            if (selectBuildable != null)
            {
                DisplayInformation(selectBuildable.buildable);
                isFind = true;
                break;
            }
        }

        if(!isFind)
        {
            DisableInformation();
        }

    }

    void DisableInformation()
    {
        if (InformationObj.activeSelf)
        {
            InformationObj.SetActive(false);
            foreach (Transform child in InformationContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void DisplayInformation(Buildable buildable)
    {
        if (!InformationObj.activeSelf)
        {
            InformationObj.SetActive(true);
            TextMeshProUGUI[] textObj = InformationObj.GetComponentsInChildren<TextMeshProUGUI>();
            textObj[0].text = buildable.itemName;
            textObj[1].text = buildable.description;
            List<int> idSaved = new List<int>();

            foreach (Ressources ressources in buildable.ressources)
            {
                if(idSaved.Contains(ressources.id))
                {
                    continue;
                }
                else
                {
                    GameObject ressourceObj = Instantiate(RessourceIconObj, InformationContent.transform);
                    ressourceObj.GetComponentsInChildren<Image>()[1].sprite = ressources.icon;
                    TextMeshProUGUI[] textRessources = ressourceObj.GetComponentsInChildren<TextMeshProUGUI>();
                    textRessources[0].text = ressources.name;
                    textRessources[1].text = $"x{GetNumberOfItem(buildable, ressources)}";
                    idSaved.Add(ressources.id);
                }

            }
        }
    }

    public int GetNumberOfItem(Buildable buildable, ItemInventory _item)
    {
        if (buildable.ressources.Contains(_item))
        {
            int quantity = 0;
            foreach (ItemInventory item in buildable.ressources)
            {
                if (item == _item)
                {
                    quantity++;
                }
            }

            return quantity;
        }

        return 0;
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
