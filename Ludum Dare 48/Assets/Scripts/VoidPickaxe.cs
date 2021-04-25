using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPickaxe : MonoBehaviour
{
    public VarManager SpikesCheck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mh" && SpikesCheck.runes == 6)
        {
            gameObject.SetActive(false);
        }
    }
}
