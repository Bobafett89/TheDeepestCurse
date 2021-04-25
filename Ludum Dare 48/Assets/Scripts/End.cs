using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject death;
    public VarManager spikesCheck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mh")
        {
            if (Movement.PickaxeLevel == 2)
            {
                gameObject.SetActive(false);
            }
            else
            {
                death.SetActive(true);
                spikesCheck.time = 0.5f;
                spikesCheck.starta = true;
                VarManager.end = true;
            }
        }
    }
}
