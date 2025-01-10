#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ScriptableObjectAutoRegister : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if (asset is ItemInventory item)
            {
                AddToItemDatabase(item);
            }
            else if(asset is Buildable buildable)
            {
                AddToBuildableDatabase(buildable);
            }
        }
    }

    private static void AddToBuildableDatabase(Buildable buildable)
    {
        string[] databases = AssetDatabase.FindAssets("t:BuildableDatabase");
        if (databases.Length > 0)
        {
            string databasePath = AssetDatabase.GUIDToAssetPath(databases[0]);
            var database = AssetDatabase.LoadAssetAtPath<BuildableDatabase>(databasePath);
            if (database != null)
            {
                database.AddBuildable(buildable);
                EditorUtility.SetDirty(database); // Marque le fichier comme modifi�
                Debug.Log($"Buildable {buildable.name} ajout� � la base de donn�es.");
            }
        }
        else
        {
            Debug.LogWarning("Aucune base de donn�es de Buildable trouv�e !");
        }
    }

    private static void AddToItemDatabase(ItemInventory item)
    {
        string[] databases = AssetDatabase.FindAssets("t:ItemDatabase");
        if (databases.Length > 0)
        {
            string databasePath = AssetDatabase.GUIDToAssetPath(databases[0]);
            var database = AssetDatabase.LoadAssetAtPath<ItemDatabase>(databasePath);
            if (database != null)
            {
                database.AddItem(item);
                EditorUtility.SetDirty(database); // Marque le fichier comme modifi�
                Debug.Log($"Item {item.name} ajout� � la base de donn�es.");
            }
        }
        else
        {
            Debug.LogWarning("Aucune base de donn�es d'items trouv�e !");
        }
    }
}
#endif
