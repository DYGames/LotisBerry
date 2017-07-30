using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TowerObject
{

    [SerializeField]
    public string towerName;
    [SerializeField]
    public TowerType towerType;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int price;
    [SerializeField]
    public GameObject towerPrefab;
}

[System.Serializable]
public enum TowerType
{
    GROUND,
    AIR,
    ROUND,
}