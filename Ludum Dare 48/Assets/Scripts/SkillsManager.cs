using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public VarManager t;
    public Text t2;
    public bool DoubleJump, Dash, WallClimb, Hook, PickaxeOne, PickaxeTwo, Lantern;
    public List<GameObject> LanternOff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mh")
        {
            AudioManager.p = true;
            if (DoubleJump)
            {
                Movement.tjump = 2;
                Movement.defaultTjump = 2;
                t2.text = "You obtained void wings. Now you can make double jump.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (Dash)
            {
                VarManager.SkillDash = true;
                Movement.AbleToDash = true;
                t2.text = "You obtained legs of void. Now you can dash.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (WallClimb)
            {
                VarManager.SkillWallClimbing = true;
                t2.text = "You obtained climbing spikes. Now you can climb walls.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (Hook)
            {
                VarManager.SkillHook = true;
                t2.text = "You obtained hook. Now you can cling to the void rings.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (PickaxeOne)
            {
                Movement.AbleToHit = true;
                t2.text = "You obtained pickaxe. Now you can use pickaxe to break damaged walls.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (PickaxeTwo)
            {
                Movement.PickaxeLevel = 2;
                t2.text = "You obtained void pickaxe. Now you can use it to end with the curse.";
                t2.enabled = true;
                t.starta2 = true;
            }
            else if (Lantern)
            {
                foreach (GameObject obj in LanternOff)
                {
                    obj.SetActive(false);
                }
                t2.text = "You obtained lantern. Now you can walk there where was darkness.";
                t2.enabled = true;
                t.starta2 = true;
            }
            gameObject.SetActive(false);
        }
    }
}
