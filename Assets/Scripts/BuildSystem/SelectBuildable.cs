using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuildable : MonoBehaviour
{
    private BuildSystem buildSystem;
    public Buildable buildable;

    private void Start()
    {
        buildSystem = GameManager.instance.Player.GetComponent<BuildSystem>();
    }

    public void onButtonClicked()
    {
        Inventory PlayerInv = GameManager.instance.Player.GetComponent<Inventory>();

        List<Ressources> playerRessources = new List<Ressources>();

        foreach (ItemInventory ressources in PlayerInv.items)
        {
            if(ressources is Ressources res)
            {
                playerRessources.Add(res);
            }
        }

        foreach (Ressources ressources in buildable.ressources)
        {
            if (playerRessources.Contains(ressources))
            {
                playerRessources.Remove(ressources);
            }
            else
            {
                Debug.Log("Not Enought Ressources");
                return;
            }
        }

        buildSystem.selectedBuildable = buildable.buildableObject;
        buildSystem.isBuilding = true;
        buildSystem.asInitBuildable = false;
        buildSystem.ResetBuildable();
        GameManager.instance.transform.GetComponent<BuildUI>().isDisplay = false;
    }

}
