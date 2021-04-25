using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSprite : MonoBehaviour
{
    SpriteRenderer sprt;
    public Sprite f, s;
    public VarManager hookcheck;
    private void Awake()
    {
        sprt = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "HookCheck" && VarManager.SkillHook)
        {
            sprt.sprite = s;
            hookcheck.ring = gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "HookCheck" && VarManager.SkillHook)
        {
            sprt.sprite = f;
        }
    }
}
