using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pig : Monster
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        CoinValue = 100;
    }

    protected override void Update()
    {
        base.Update();

        UpdateAnimation();
    }

    public override IEnumerator FindAndTarget()
    {
        while (true)
        {
            if (target == null)
            {
                target = TowerInfo.LotisBerry;
                Agent.SetDestination(target.transform.position);
                Agent.stoppingDistance = target.GetComponentInChildren<MeshFilter>().mesh.bounds.size.x * 0.5f;
            }
            yield return null;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMove", (Vector3.Distance(transform.position, navMeshAgent.destination) >= navMeshAgent.stoppingDistance));
        animator.SetBool("isAttack", isStopped && attackDelay <= 0);
        animator.SetBool("isDie", HP <= 0);
        if (attackDelay <= 0)
        {
            attackDelay = 3;
        }
    }
}
