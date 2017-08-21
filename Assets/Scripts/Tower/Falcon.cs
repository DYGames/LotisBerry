using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : TowerInfo, IUnit
{
    [SerializeField]
    private GameObject falcon;
    private Monster target;
    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootRoutine());
    }
    private void Update()
    {
        if (target != null)
        {
            falcon.transform.position = Vector3.Lerp(falcon.transform.position, target.transform.position, 0.05f);
            falcon.transform.LookAt(target.transform);
            if (Vector3.Distance(falcon.transform.position, target.transform.position) < 0.5f)
            {
                target.Hit(damage);
                target = null;
            }
        }
        else
        {
            falcon.transform.position = Vector3.Lerp(falcon.transform.position, transform.position, 0.1f);
        }
    }
    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(1);

        var monsters = FindObjectsOfType<Monster>();
        Monster nearmonster = null;
        float distance = int.MaxValue;
        foreach (var monster in monsters)
        {
            float tempdis;
            if (distance > (tempdis = Vector3.Distance(monster.transform.position, transform.position)))
            {
                nearmonster = monster;
                distance = tempdis;
            }
        }

        if (nearmonster != null)
        {
            target = nearmonster;
        }

        StartCoroutine(ShootRoutine());
    }

    public void Hit(int hp)
    {
        hitCallback.Invoke();
        HP -= hp;
        if (HP <= 0)
        {
            destoryCallback.Invoke();
            Destroy(gameObject);
        }
    }
}
