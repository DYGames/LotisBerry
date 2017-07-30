using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;
    public int attackDmg;
    void Start()
    {
        StartCoroutine(Run());
        Destroy(gameObject, 1);
    }

    private void FixedUpdate()
    {
        foreach (var col in Physics.SphereCastAll(transform.position, 0.5f, Vector3.up))
        {
            if (col.transform.gameObject.CompareTag("Monster"))
            {
                GetInterfaceInComponent.Invoke<IUnit>(col.transform).Hit(attackDmg);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Run()
    {
        float target_Distance = Vector3.Distance(transform.position, target.transform.position);
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * 45.0f * Mathf.Deg2Rad) / 9.8f);
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(45.0f * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(45.0f * Mathf.Deg2Rad);
        float flightDuration = target_Distance / Vx;

        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (9.8f * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
