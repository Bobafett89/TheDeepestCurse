using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TF : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
