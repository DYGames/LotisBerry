using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 startPosition;
    void Start()
    {
        StartCoroutine(Shoot());
        startPosition = transform.position;
    }

    private IEnumerator Shoot()
    {
        float time = 0;
        while(time < 1)
        {
            yield return null;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            time += Time.deltaTime * 10;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
