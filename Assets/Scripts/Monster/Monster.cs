using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IUnit, IMonster
{
    protected NavMeshAgent Agent { get; set; }
    Coroutine snareRoutine;

    [SerializeField]
    public int HP;
    [SerializeField]
    private AudioClip die;
    [SerializeField]
    private AudioClip move;

    private float agentSpeed;

    protected bool isStopped;
    protected float attackDelay;

    protected Transform target;
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        agentSpeed = Agent.speed;
        StartCoroutine(FindAndTarget());
    }

    protected virtual void Update()
    {
        isStopped = Vector3.Distance(transform.position, Agent.destination) <= Agent.stoppingDistance;
        attackDelay -= Time.deltaTime;
    }

    public void Snare(float t)
    {
        if (Agent.isOnNavMesh)
            Agent.SetDestination(transform.position);
        Agent.speed = 0;
        if (snareRoutine != null)
            StopCoroutine(snareRoutine);
        snareRoutine = StartCoroutine(SnareRoutine(t));
    }

    protected IEnumerator SnareRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        Agent.speed = agentSpeed;
    }

    public void Hit(int dmg)
    {
        if (HP > 0)
        {
            HP -= dmg;
        }
        if (HP <= 0)
        {
            HP = -1;
            StartCoroutine(DestroySelf(1));
        }
        Snare(0.2f);
    }

    private IEnumerator DestroySelf(float c)
    {
        yield return new WaitForSeconds(c);
        if (GameObject.FindGameObjectsWithTag("Monster").Length == 1)
            WaveMng.WaveEndCallback.Invoke();
        GetComponent<AudioSource>().clip = die;
        GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public void Attack()
    {
        if (target != null)
            GetInterfaceInComponent.Invoke<IUnit>(target).Hit(1);
    }


    public virtual IEnumerator FindAndTarget()
    {
        while (true)
        {
            List<Transform> tgs = new List<Transform>();
            List<float> tDiss = new List<float>();

            foreach (var item in Context.Towers)
            {
                if (!item.GetComponent<TowerInfo>().towerName.Equals("Trap"))
                    tgs.Add(item);
            }
            tgs.Add(Context.Player);
            foreach (var item in tgs)
            {
                tDiss.Add(Vector3.Distance(transform.position, item.transform.position));
            }
            float d = Mathf.Min(tDiss.ToArray());
            foreach (var item in tgs)
            {
                if (Vector3.Distance(transform.position, item.transform.position) == (d))
                {
                    Agent.SetDestination(item.transform.position);
                    target = item.transform;
                    Agent.stoppingDistance = item.GetComponentInChildren<MeshFilter>().mesh.bounds.size.x * 0.5f;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

}
