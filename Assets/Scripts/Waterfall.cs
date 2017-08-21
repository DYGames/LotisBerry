using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField]
    private Material material;

    void Update()
    {
        material.mainTextureOffset += new Vector2(0, Time.deltaTime);
        if (material.mainTextureOffset.y >= 1)
            material.mainTextureOffset = new Vector2();
    }
}
