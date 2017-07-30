using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : TowerInfo, IUnit
{
    private BoxCollider boxCollider;

    public override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (boxCollider == null || !isActive)
            return;
        var colliders = Physics.BoxCastAll(transform.position + boxCollider.center, boxCollider.size * 0.5f, -Vector3.up);
        for (int i = 0; i < colliders.Length; i++)
        {
            float dis = Vector3.Distance(colliders[i].transform.position, transform.position);
            if (colliders[i].transform.CompareTag("Monster") && dis < 0.5f)
            {
                colliders[i].transform.GetComponent<Monster>().HP -= damage;
                colliders[i].transform.GetComponent<Monster>().Snare(1);
                GetComponent<Animator>().SetTrigger("Snare");
                Destroy(boxCollider);
                Destroy(gameObject, 1);
                destoryCallback.Invoke();
            }
        }
    }
    public void Hit(int hp)
    {
    }
}
