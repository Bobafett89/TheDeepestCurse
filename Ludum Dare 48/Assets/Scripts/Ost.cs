using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ost : MonoBehaviour
{
    public AudioSource ost;
    private void Update()
    {
        ost.volume = PlayerPrefs.GetFloat("Sound", 25) / 100;
    } 
}
