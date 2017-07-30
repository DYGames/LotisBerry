using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public void RandomRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0, 360));
    }
}
