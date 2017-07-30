using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Berry : TowerInfo, IUnit
{

    public override void Start()
    {
        base.Start();
        TowerInfo.LotisBerry = transform;
        Context.Towers = new List<Transform>();
        Context.Towers.Add(transform);
        destoryCallback = () =>
        {
            Context.Towers.Remove(transform);
            Invoke("DestroyEffect", 1.5f);

            PlayMode.mode = PlayMode.Mode.EDIT;

            Context.userInterface.red.gameObject.SetActive(false);
            Context.userInterface.respawn.gameObject.SetActive(false);
        };

    }

    public void Hit(int hp)
    {
        if (HP > 0)
        {
            hitCallback.Invoke();

            HP -= hp;
            if (HP <= 0)
            {
                destoryCallback.Invoke();
                Destroy(gameObject, 1.5f);
            }
        }
    }

    void DestroyEffect()
    {
        GameObject bb = Instantiate(Context.prefabLoader.TowerDestroyEffect);
        bb.transform.position = transform.position;
        bb.GetComponent<ParticleSystemMultiplier>().multiplier = 5;
        GameObject bb1 = Instantiate(Context.prefabLoader.TowerDestroyEffect);
        bb1.transform.position = transform.position;
        bb1.GetComponent<ParticleSystemMultiplier>().multiplier = 5;
        GameObject bb2 = Instantiate(Context.prefabLoader.TowerDestroyEffect);
        bb2.transform.position = transform.position;
        bb2.GetComponent<ParticleSystemMultiplier>().multiplier = 5;



        Context.userInterface.continueButton.gameObject.SetActive(true);
        Context.userInterface.defeat.gameObject.SetActive(true);
    }
}
