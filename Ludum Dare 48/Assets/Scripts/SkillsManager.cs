using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public bool DoubleJump, Dash, WallClimb, Hook, PickaxeOne, PickaxeTwo, Lantern;
    public List<GameObject> LanternOff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mh")
        {
            if (DoubleJump)
            {
                Movement.tjump = 2;
                Movement.defaultTjump = 2;
            }
            else if (Dash)
            {
                VarManager.SkillDash = true;
                Movement.AbleToDash = true;
            }
            else if (WallClimb)
            {
                VarManager.SkillWallClimbing = true;
            }
            else if (Hook)
            {
                VarManager.SkillHook = true;
            }
            else if (PickaxeOne)
            {
                Movement.AbleToHit = true;
            }
            else if (PickaxeTwo)
            {
                Movement.PickaxeLevel = 2;
            }
            else if (Lantern)
            {
                foreach (GameObject obj in LanternOff)
                {
                    obj.SetActive(false);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
