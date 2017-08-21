using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    public AudioClip coinaudio;

    bool Taken;

    public int Value;
    public int value
    {
        get
        {
            if (Taken)
                return 0;
            else
            {
                GetComponent<AudioSource>().clip = coinaudio;
                GetComponent<AudioSource>().Play();
                Taken = true;
                return Value;
            }
        }
        set
        {
            Value = value;
            transform.localScale = new Vector3(transform.localScale.x * (value / 100.0f), transform.localScale.y * (value / 100.0f), transform.localScale.z * (value / 100.0f));
        }
    }
    
    private void Update()
    {
        if(Taken)
        {
            transform.position = Vector3.Lerp(transform.position, Context.Player.position, 0.1f);
            if (Vector3.Distance(transform.position, Context.Player.position) < 0.25f)
                Destroy(gameObject);
        }
    }
}
