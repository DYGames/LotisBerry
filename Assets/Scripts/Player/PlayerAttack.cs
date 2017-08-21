using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerAttack : MonoBehaviour, IUnit
{
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float lookIKWeight;
    [SerializeField]
    private float bodyWeight;
    [SerializeField]
    private float headWeight;
    [SerializeField]
    private float eyesWeight;
    [SerializeField]
    private float clampWeight;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject bulletTrail;
    [SerializeField]
    private Animation muzzleFlash;

    Animator animator;

    RaycastHit hit;

    bool isHit;

    Coroutine hitRoutine;

    public int magAmount;

    public bool isReload;

    public int HP;

    private bool hitEffectAble;

    private Vector3 normalPosition;
    private Vector3 normalRotation;
    private Vector3 normalScale;

    ThirdPersonCharacter TPC;
    ThirdPersonUserControl TPUC;

    AudioSource audioSource;

    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip reload;

    void Start()
    {
        HP = 100;
        Context.Player = transform;
        animator = GetComponent<Animator>();

        magAmount = 30;

        isReload = false;
        enabled = false;
        hitEffectAble = true;
        TPC = GetComponent<ThirdPersonCharacter>();
        TPUC = GetComponent<ThirdPersonUserControl>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (HP <= 0)
        {
            TPC.enabled = false;
            TPUC.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReload)
        {
            StartCoroutine(Reload());
        }

        Camera.main.orthographic = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Camera.main.orthographic = true;
        animator.SetBool("Shoot", false);
        isHit = Physics.Raycast(ray, out hit);
        if(isHit)
            gun.transform.parent.LookAt(hit.point);
        if (Input.GetMouseButton(0) && magAmount > 0 && HP > 0 && !isReload)
        {
            if (isHit)
            {
                CursorMng.cursorMng.CursorSizeTo(40);
                CursorMng.cursorMng.CursorSizeTo(55, true);
                animator.SetBool("Shoot", true);
            }
        }
        else if (isHit && hit.transform.gameObject.CompareTag("Monster"))
        {
            CursorMng.cursorMng.CursorSizeTo(60, true);
        }
        else if (magAmount <= 0 && !isReload)
        {
            StartCoroutine(Reload());
        }


        foreach (var item in GameObject.FindGameObjectsWithTag("Coin"))
        {
            if (Vector3.Distance(item.transform.position, transform.position) < 10)
            {
                int v = item.gameObject.GetComponent<Coin>().value;
                if(v > 0)
                    Context.gameData.Money += v;
                break;
            }
        }
    }

    public void Attack()
    {
        Monster monster;

        if (hit.transform != null)
        {
            audioSource.clip = shoot;
            audioSource.Play();
            magAmount -= 1;
            muzzleFlash.Play("MuzzleFlash");
            GameObject t = Instantiate(bulletTrail);
            t.transform.position = transform.position;
            t.GetComponent<BulletTrail>().targetPosition = hit.point;
            if ((monster = hit.transform.GetComponent<Monster>()) != null)
            {
                if (hitEffectAble && monster.HP > 0)
                {
                    StartCoroutine(HitEffectActive());
                    GameObject o = Instantiate(Context.prefabLoader.HitEffect);
                    o.transform.SetParent(transform.parent);
                    o.transform.position = hit.point;
                }
                monster.Hit(5);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, gun.transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, gun.transform.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gun.transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, gun.transform.rotation);
        if (isHit)
        {
            animator.SetLookAtWeight(lookIKWeight, bodyWeight, headWeight, eyesWeight, clampWeight);

            animator.SetLookAtPosition(hit.point);
        }
    }

    private IEnumerator Reload()
    {
        audioSource.clip = reload;
        audioSource.Play();
        isReload = true;
        yield return new WaitForSeconds(1.2f);
        magAmount = 30;
        isReload = false;
    }

    public void Hit(int dmg)
    {
        if (HP > 0)
        {
            HP -= dmg;
            if (HP <= 0)
            {
                HP = 0;
                normalPosition = transform.position;
                normalRotation = transform.eulerAngles;
                normalScale = transform.localScale;
                transform.eulerAngles = new Vector3(Random.Range(45, 135), 0, Random.Range(45, 135));
                StartCoroutine(Context.userInterface.Respawn(10));
            }
        }
    }

    private IEnumerator HitEffectActive()
    {
        hitEffectAble = false;
        yield return new WaitForSeconds(1);
        hitEffectAble = true;
    }

    public void Respawn()
    {
        transform.position = normalPosition;
        transform.eulerAngles = normalRotation;
        transform.localScale = normalScale;

        HP = 100;
        magAmount = 30;

        if (PlayMode.mode == PlayMode.Mode.TPS)
            TPC.enabled = true;
        TPUC.enabled = true;
    }
}
