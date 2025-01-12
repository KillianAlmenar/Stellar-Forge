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
        buildSystem.selectedBuildable = buildable.buildableObject;
        buildSystem.isBuilding = true;
        buildSystem.asInitBuildable = false;
        GameManager.instance.transform.GetComponent<BuildUI>().isDisplay = false;
    }

}
