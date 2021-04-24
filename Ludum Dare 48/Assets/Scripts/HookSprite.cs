using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSprite : MonoBehaviour
{
    SpriteRenderer sprt;
    public Sprite f, s;
    private void Awake()
    {
        sprt = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Movement.AbleToHook)
        {
            sprt.sprite = s;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Movement.AbleToHook)
        {
            sprt.sprite = f;
        }
    }
}
