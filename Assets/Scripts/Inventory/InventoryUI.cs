using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [HideInInspector] public bool isDisplay = false;
    public GameObject InventoryObj;
    public Inventory Inventory;
    public GameObject itemUI;
    public GameObject contentUI;
    public TMP_Dropdown sortingDropDown;
    [HideInInspector] public ItemInventory selectedItem;
    public GameObject interaction;
    protected List<bool> sortingTypeBool = new List<bool> { false, false, false, false, false };
    [HideInInspector] public GameObject buttonPressed = null;
    protected GameObject previousObjectSelected = null;
    public ItemDatabase ItemDatabase;
    public GameObject informationUI;

    public enum SORTINGMODE
    {
        ID,
        TYPE,
        RARITY
    }

    public SORTINGMODE mode;

    public enum SORTINGTYPE
    {
        ALL,
        KEYITEM,
        CONSOMMABLE,
        WEAPON,
        ARMOR,
        RESSOURCE

    }

    public SORTINGTYPE sortingType;

    private void Start()
    {
        InventoryObj.SetActive(false);

    }

    protected void Update()
    {
        if (isDisplay && !InventoryObj.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            InventoryObj.SetActive(true);
            updateUI();
        }
        else if (!isDisplay && InventoryObj.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            DesactivateUI();
            InventoryObj.SetActive(false);
        }
    }

    public void updateUI()
    {
        DesactivateUI();
        Dictionary<int, int> itemsId = new Dictionary<int, int>();
        List<string> itemsNameDisplayed = new List<string>();

        for (int i = 0; i < ItemDatabase.items.Count; i++)
        {
            itemsId.Add(ItemDatabase.items[i].name.GetHashCode(), 0);
        }

        foreach (ItemInventory item in Inventory.items)
        {
            itemsId[item.id] += 1;
        }

        SortList();

        foreach (ItemInventory item in Inventory.items)
        {
            if (!itemsNameDisplayed.Contains(item.name))
            {
                itemsNameDisplayed.Add(item.name);

                switch (sortingType)
                {
                    case SORTINGTYPE.KEYITEM:
                        if (item is KeyItem)
                        {
                            sortingTypeBool[0] = true;
                        }
                        break;
                    case SORTINGTYPE.CONSOMMABLE:
                        if (item is Consommable)
                        {
                            sortingTypeBool[1] = true;
                        }
                        break;
                    case SORTINGTYPE.WEAPON:
                        if (item is Weapon)
                        {
                            sortingTypeBool[2] = true;
                        }
                        break;
                    case SORTINGTYPE.ARMOR:
                        if (item is Armor)
                        {
                            sortingTypeBool[3] = true;
                        }
                        break;
                    case SORTINGTYPE.RESSOURCE:
                        if (item is Ressources)
                        {
                            sortingTypeBool[4] = true;
                        }
                        break;
                }

                if (itemsId[item.id] > 0 && GetItemById(item.id).stackSize == 1 && (sortingType == SORTINGTYPE.ALL || sortingTypeBool[0] || sortingTypeBool[1] || sortingTypeBool[2] || sortingTypeBool[3] || sortingTypeBool[4]))
                {
                    for (int j = 0; j < itemsId[item.id]; j++)
                    {
                        GameObject tempItemUI = Instantiate(itemUI, contentUI.transform);
                        tempItemUI.GetComponentInChildren<TextMeshProUGUI>().text = " ";

                        if(EventSystem.current.currentSelectedGameObject == null)
                        {
                            EventSystem.current.SetSelectedGameObject(tempItemUI.GetComponentInChildren<Button>().gameObject);
                        }
                        SelectItem tempItemSelect = tempItemUI.GetComponentInChildren<SelectItem>();
                        tempItemSelect.item = GetItemById(item.id);
                        tempItemSelect.inventoryUI = this;
                        tempItemUI.GetComponentsInChildren<Image>()[1].sprite = GetItemById(item.id).icon;


                        switch (GetItemById(item.id).rarity)
                        {
                            case ItemInventory.RARITY.LEGENDARY:
                                tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(255, 215, 0, 1);
                                break;
                            case ItemInventory.RARITY.RARE:
                                tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(146, 0, 255, 1);
                                break;
                        }

                    }
                }
                else if (itemsId[item.id] == 1 && (sortingType == SORTINGTYPE.ALL || sortingTypeBool[0] || sortingTypeBool[1] || sortingTypeBool[2] || sortingTypeBool[3] || sortingTypeBool[4]))
                {
                    GameObject tempItemUI = Instantiate(itemUI, contentUI.transform);
                    tempItemUI.GetComponentInChildren<TextMeshProUGUI>().text = " ";

                    SelectItem tempItemSelect = tempItemUI.GetComponentInChildren<SelectItem>();
                    tempItemSelect.item = GetItemById(item.id);
                    tempItemSelect.inventoryUI = this;

                    tempItemUI.GetComponentsInChildren<Image>()[1].sprite = GetItemById(item.id).icon;

                    switch (GetItemById(item.id).rarity)
                    {
                        case ItemInventory.RARITY.LEGENDARY:
                            tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(255, 215, 0, 1);
                            break;
                        case ItemInventory.RARITY.RARE:
                            tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(146, 0, 255, 1);
                            break;
                    }
                }
                else if (itemsId[item.id] > 0 && (sortingType == SORTINGTYPE.ALL || sortingTypeBool[0] || sortingTypeBool[1] || sortingTypeBool[2] || sortingTypeBool[3] || sortingTypeBool[4]))
                {
                    GameObject tempItemUI = Instantiate(itemUI, contentUI.transform);
                    tempItemUI.GetComponentInChildren<TextMeshProUGUI>().text = itemsId[item.id].ToString();
                    tempItemUI.GetComponentsInChildren<Image>()[1].sprite = GetItemById(item.id).icon;

                    SelectItem tempItemSelect = tempItemUI.GetComponentInChildren<SelectItem>();
                    tempItemSelect.item = GetItemById(item.id);
                    tempItemSelect.inventoryUI = this;

                    switch (GetItemById(item.id).rarity)
                    {
                        case ItemInventory.RARITY.LEGENDARY:
                            tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(255, 215, 0, 1);
                            break;
                        case ItemInventory.RARITY.RARE:
                            tempItemUI.GetComponentsInChildren<Image>()[0].color = new Color(146, 0, 255, 1);
                            break;
                    }
                }

                for(int i =0; i < 5; i++)
                {
                    sortingTypeBool[i] = false;
                }

            }
        }
    }

    private void DesactivateUI()
    {
        foreach (Transform child in contentUI.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SortList()
    {
        switch (mode)
        {
            case SORTINGMODE.ID:
                SortByID(Inventory.items);
                break;
            case SORTINGMODE.TYPE:
                SortByType();
                break;
            case SORTINGMODE.RARITY:
                SortByRarity();
                break;
        }

    }

    private ItemInventory GetItemById(int id)
    {
        return ItemDatabase.items.Find(x => x.id == id);
    }

    public void SortByID(List<ItemInventory> itemList)
    {
        int n = itemList.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (itemList[j].id < itemList[minIndex].id)
                {
                    minIndex = j;
                }
            }
            ItemInventory temp = itemList[i];
            itemList[i] = itemList[minIndex];
            itemList[minIndex] = temp;
        }
    }

    public void SortByType()
    {
        List<ItemInventory> itemListSorted = new List<ItemInventory>();
        List<ItemInventory> itemTrash = new List<ItemInventory>();
        foreach (ItemInventory item in Inventory.items)
        {
            if (!itemListSorted.Contains(item))
            {
                if (itemListSorted.Count > 0)
                {

                    for (int i = 0; i < itemListSorted.Count;)
                    {

                        if (item.type < itemListSorted[i].type)
                        {
                            itemListSorted.Insert(i, item);
                            break;
                        }
                        else if (item.type > itemListSorted[i].type)
                        {
                            int compteur = i;
                            while (itemListSorted.Count > compteur)
                            {
                                if (item.type < itemListSorted[compteur].type)
                                {
                                    break;
                                }
                                compteur++;
                            }
                            itemListSorted.Insert(compteur, item);
                            break;
                        }
                        else
                        {
                            itemListSorted.Insert(i, item);
                            break;
                        }
                    }

                }
                else
                {
                    itemListSorted.Add(item);
                }
            }
            else
            {
                itemTrash.Add(item);
            }

        }

        int n = itemListSorted.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (itemListSorted[j].type == itemListSorted[minIndex].type && itemListSorted[j].id < itemListSorted[minIndex].id)
                {
                    minIndex = j;
                }
            }
            ItemInventory temp = itemListSorted[i];
            itemListSorted[i] = itemListSorted[minIndex];
            itemListSorted[minIndex] = temp;
        }

        foreach (ItemInventory item in itemTrash)
        {
            itemListSorted.Add(item);
        }

        Inventory.items = itemListSorted;
    }

    public void SortByRarity()
    {
        List<ItemInventory> itemListSorted = new List<ItemInventory>();
        List<ItemInventory> itemTrash = new List<ItemInventory>();
        foreach (ItemInventory item in Inventory.items)
        {
            if (!itemListSorted.Contains(item))
            {
                if (itemListSorted.Count > 0)
                {

                    for (int i = 0; i < itemListSorted.Count;)
                    {

                        if (item.rarity < itemListSorted[i].rarity)
                        {
                            itemListSorted.Insert(i, item);
                            break;
                        }
                        else if (item.rarity > itemListSorted[i].rarity)
                        {
                            int compteur = i;
                            while (itemListSorted.Count > compteur)
                            {
                                if (item.rarity < itemListSorted[compteur].rarity)
                                {
                                    break;
                                }
                                compteur++;
                            }
                            itemListSorted.Insert(compteur, item);
                            break;
                        }
                        else
                        {
                            itemListSorted.Insert(i, item);
                            break;
                        }
                    }

                }
                else
                {
                    itemListSorted.Add(item);
                }
            }
            else
            {
                itemTrash.Add(item);
            }

        }

        int n = itemListSorted.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (itemListSorted[j].rarity == itemListSorted[minIndex].rarity && itemListSorted[j].type < itemListSorted[minIndex].type)
                {
                    minIndex = j;
                }
            }
            ItemInventory temp = itemListSorted[i];
            itemListSorted[i] = itemListSorted[minIndex];
            itemListSorted[minIndex] = temp;
        }

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (itemListSorted[j].rarity == itemListSorted[minIndex].rarity && itemListSorted[j].type == itemListSorted[minIndex].type && itemListSorted[j].id < itemListSorted[minIndex].id)
                {
                    minIndex = j;
                }
            }
            ItemInventory temp = itemListSorted[i];
            itemListSorted[i] = itemListSorted[minIndex];
            itemListSorted[minIndex] = temp;
        }

        foreach (ItemInventory item in itemTrash)
        {
            itemListSorted.Add(item);
        }

        Inventory.items = itemListSorted;
    }

    public void SortInventory()
    {
        mode = (SORTINGMODE)sortingDropDown.value;
        updateUI();
    }

    public void ActivateInteraction()
    {
        interaction.SetActive(true);

        if (buttonPressed != null)
        {
            interaction.transform.position = buttonPressed.transform.position;
        }

        if (this as PlayerInventoryUI && !(selectedItem as Consommable))
        {
            interaction.GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.grey;
            interaction.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.grey;
        }
        else
        {
            interaction.GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.white;
            interaction.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.white;
        }
    }

    public void UpdateInformationUI()
    {
        if (Inventory.selectedItem != null && selectedItem != Inventory.selectedItem)
        {
            selectedItem = Inventory.selectedItem;
            previousObjectSelected = EventSystem.current.currentSelectedGameObject;
            informationUI.GetComponent<InformationInv>().SetInformations(Inventory.selectedItem);
        }

        if (Inventory.selectedItem != null && !informationUI.activeSelf)
        {
            informationUI.SetActive(true);
            previousObjectSelected = EventSystem.current.currentSelectedGameObject;
            informationUI.GetComponent<InformationInv>().SetInformations(Inventory.selectedItem);

        }
        else if (Inventory.selectedItem == null && informationUI.activeSelf)
        {
            informationUI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(previousObjectSelected);
        }

    }
}