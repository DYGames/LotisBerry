using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeTowerObject : MonoBehaviour
{
    [MenuItem("Assets/Create/ScriptableObject/Create Tower Object")]
    public static void CreateTowerAsset()
    {
        var asset = ScriptableObject.CreateInstance<TowerContainer>();
        AssetDatabase.CreateAsset(asset, "Assets/Editor/Tower.asset");
        AssetDatabase.Refresh();

    }
}