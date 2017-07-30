using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : TowerInfo, IUnit
{
    [SerializeField]
    private GameObject arrowPrefab;

    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootRoutine());
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
            Vector3 r = transform.eulerAngles;
            transform.LookAt(nearmonster.transform);
            transform.eulerAngles = new Vector3(r.x, transform.eulerAngles.y, r.z);
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = transform.position + new Vector3(0,1,0);
            arrow.GetComponent<Arrow>().target = nearmonster.transform;
            arrow.GetComponent<Arrow>().attackDmg = damage;
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
