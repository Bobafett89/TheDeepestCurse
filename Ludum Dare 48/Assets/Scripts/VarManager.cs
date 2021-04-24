using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VarManager : MonoBehaviour
{
    float maxY;
    public Text t;
    public bool GroundCheck, WallCheck, HookCheck, SpikesCheck, CeilingHitCheck;
    static public int hp, runes;
    static public bool IsOnGround, SkillDash, SkillWallClimbing, SkillHook;
    Vector2 checkpoint;
    BoxCollider2D bxl;
    private void Awake()
    {
        bxl = GetComponent<BoxCollider2D>();
        checkpoint = new Vector2(0, 0);
        hp = 25;
        runes = 0;
        SkillDash = false;
        SkillWallClimbing = false;
        SkillHook = false;
    }
    private void Start()
    {
        maxY = Movement.trsn.gameObject.GetComponent<Movement>().maxY;
    }
    private void Update()
    {
        if (GroundCheck)
        {
            t.text = hp.ToString();
        }
        if (hp == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (GroundCheck)
            {
                IsOnGround = true;
                Movement.AbleToJump = true;
                Movement.ceiling = Movement.trsn.position.y + maxY;
                Movement.tjump = Movement.defaultTjump;
                if (SkillDash)
                {
                    Movement.AbleToDash = true;
                }
            }
            else if (WallCheck && SkillWallClimbing)
            {
                Movement.IsClimbing = true;
                Movement.AbleToDash = true;
                Movement.rgd.gravityScale = 0;
                Movement.tjump = Movement.defaultTjump;
                Movement.ceiling = Movement.trsn.position.y + maxY;
            }
            else if (CeilingHitCheck && Movement.trsn.position.y < Movement.ceiling && !IsOnGround)
            {
                Movement.tjump -= 1;
                Movement.AbleToJump = false;
            }

        }
        else
        {
            if (SpikesCheck)
            {
                if (collision.name.Contains("Rune"))
                {
                    runes += 1;
                    hp += 25;
                    collision.gameObject.SetActive(false);
                }
                if (collision.name.Contains("(CP)"))
                {
                    checkpoint = collision.transform.position;
                }
                else if (collision.name.Contains("(D)"))
                {
                    Movement.trsn.position = checkpoint;
                    Movement.IsClimbing = false;
                    Movement.rgd.gravityScale = 2;
                    Movement.AbleToJump = true;
                    Movement.tjump = 0;
                    IsOnGround = false;
                    Movement.AbleToHook = false;
                    Movement.IsHooked = false;
                    Movement.rgd.velocity = new Vector2(0, 0);
                    Movement.rgd.constraints = RigidbodyConstraints2D.FreezeRotation;
                    Movement.IsDashActive = false;
                    hp -= 1;
                }
            }
            else if (HookCheck && collision.name.Contains("Hook") && SkillHook)
            {
                Movement.AbleToHook = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (GroundCheck)
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    StartCoroutine(Ctime());
                }
                else
                {
                    IsOnGround = false;
                }
            }
            else if (WallCheck && SkillWallClimbing)
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    StartCoroutine(Ctime());
                }
                else
                {
                    Movement.IsClimbing = false;
                    Movement.rgd.gravityScale = 2;
                    Movement.AbleToJump = true;
                    Movement.AbleToDash = true;
                }
            }
        }
        else if (HookCheck && SkillHook)
        {
            Movement.AbleToHook = false;
        }
    }
    private IEnumerator Ctime()
    {
        yield return new WaitForSeconds(0.1f);
        if (GroundCheck)
        {
            IsOnGround = false;
        }
        else if (WallCheck)
        {
            Movement.IsClimbing = false;
            Movement.rgd.gravityScale = 2;
            Movement.AbleToJump = true;
            Movement.AbleToDash = true;
        }
        if (!Input.GetKey(KeyCode.Space) && !IsOnGround)
        {
            Movement.tjump -= 1;
        }
    }
}
