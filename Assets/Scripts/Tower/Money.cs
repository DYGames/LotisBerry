using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//changed to guard tower
public class Money : TowerInfo, IUnit
{
    public override void Start()
    {
        base.Start();
       // StartCoroutine(MakeMoneyRoutine());
    }

    private IEnumerator MakeMoneyRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        Context.gameData.Money += 10;

        StartCoroutine(MakeMoneyRoutine());
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
