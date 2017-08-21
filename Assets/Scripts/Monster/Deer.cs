using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : Monster
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        CoinValue = 30;
    }

    protected override void Update()
    {
        base.Update();

        UpdateAnimation();
    }

    public override IEnumerator FindAndTarget()
    {
        return base.FindAndTarget();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMove", (Vector3.Distance(transform.position, navMeshAgent.destination) >= navMeshAgent.stoppingDistance));
        animator.SetBool("isAttack", isStopped && attackDelay <= 0);
        animator.SetBool("isDie", HP <= 0);
        if (attackDelay <= 0)
        {
            attackDelay = 2;
        }
    }
}
