using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    static public Transform LotisBerry;

    [SerializeField]
    public string towerName;
    [SerializeField]
    public TowerType towerType;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int price;
    [SerializeField]
    public Vector2 TowerCoordinate;
    [SerializeField]
    public int HP;
    [SerializeField]
    public bool isActive;
    public Action destoryCallback;
    public Action hitCallback;

    [SerializeField]
    private AudioClip hit;
    public virtual void Start()
    {
        hitCallback = () =>
        {
            GameObject h = Instantiate(Context.prefabLoader.TowerHitEffect);
            h.transform.position = transform.position;
            transform.GetComponent<AudioSource>().clip = hit;
            transform.GetComponent<AudioSource>().Play();
        };
    }

}
