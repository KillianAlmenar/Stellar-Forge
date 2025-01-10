using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildableDatabase", menuName = "Game/BuildableDatabase")]
public class BuildableDatabase : ScriptableObject
{
    public List<Buildable> buildables = new List<Buildable>();

    public void AddBuildable(Buildable buildable)
    {
        if (!buildables.Contains(buildable))
        {
            buildables.Add(buildable);
            Debug.Log($"Ajout� � la base de donn�es : {buildable.name}");
        }
    }
}
