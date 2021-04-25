using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableWall : MonoBehaviour
{
    public bool IsUpgadeNeeded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("PickZone"))
        {
            if (IsUpgadeNeeded && Movement.PickaxeLevel == 2)
            {
                SceneManager.LoadScene("GoodEnd");
            }
            else if(!IsUpgadeNeeded)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
