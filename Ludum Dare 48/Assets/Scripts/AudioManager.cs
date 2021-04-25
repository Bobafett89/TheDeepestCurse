using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip hit, jump, hook, dash, pickup;
    AudioSource s;
    static public bool h, j, hk, d, p;
    private void Awake()
    {
        h = false;
        j = false;
        hk = false;
        d = false;
        p = false;
        s = GetComponent<AudioSource>();
        s.volume = PlayerPrefs.GetFloat("Sound", 25) / 100 * 2;
    }
    private void Update()
    {
        if (h)
        {
            h = false;
            s.clip = hit;
            s.Play();
        }
        if (j)
        {
            j = false;
            s.clip = jump;
            s.Play();
        }
        if (hk)
        {
            hk = false;
            s.clip = hook;
            s.Play();
        }
        if (d)
        {
            d = false;
            s.clip = dash;
            s.Play();
        }
        if (p)
        {
            p = false;
            s.clip = pickup;
            s.Play();
        }
    }
}
