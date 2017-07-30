using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    [SerializeField]
    public GameObject HitEffect;
    [SerializeField]
    public GameObject TowerHitEffect;
    [SerializeField]
    public GameObject TowerDestroyEffect;

    private void Start()
    {
        Context.prefabLoader = this;
    }
}
