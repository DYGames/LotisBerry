using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    [SerializeField]
    private float TileSize;

    [SerializeField]
    private GameObject TilePrefab;

    [SerializeField]
    private float TileOffset;

    void Start()
    {
        float xsize = TilePrefab.GetComponent<BoxCollider>().size.x, ysize = TilePrefab.GetComponent<BoxCollider>().size.y;

        for (int y = 0; y < TileSize; y++)
        {
            for (int x = 0; x < TileSize; x++)
            {
                GameObject t = Instantiate(TilePrefab);
                t.name = string.Format("{0} {1}", x, y);
                t.transform.localPosition = new Vector3((x * xsize) + (x * xsize) * TileOffset, 0, (y * ysize) + (y * ysize) * TileOffset);
                t.transform.SetParent(transform);
            }
        }
        transform.localPosition = new Vector3((TileSize * (xsize * 0.5f)) + (TileSize * TileOffset * (xsize * 0.5f)), 0, (-TileSize * (ysize * 0.5f)) - (TileSize * TileOffset * (ysize * 0.5f)));
        transform.localEulerAngles = new Vector3(0, -45, 0);
    }

}
