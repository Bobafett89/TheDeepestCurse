using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VarManager : MonoBehaviour
{
    float maxY, walltouches, HookTouches;
    public Text t, t2;
    public GameObject ring;
    public bool GroundCheck, WallCheck, HookCheck, SpikesCheck, CeilingHitCheck;
    public float hp, runes, hprune, time;
    public bool starta, starta2;
    static public bool IsOnGround, SkillDash, SkillWallClimbing, SkillHook, end;
    Vector2 checkpoint;
    private void Awake()
    {
        checkpoint = new Vector2(0, 0);
        walltouches = 0;
        HookTouches = 0;
        SkillDash = false;
        SkillWallClimbing = false;
        SkillHook = false;
        end = false;
        hp = PlayerPrefs.GetFloat("Hp");
        time = PlayerPrefs.GetFloat("Time");
        hprune = PlayerPrefs.GetFloat("HpRune");
    }
    private void Start()
    {
        maxY = Movement.trsn.gameObject.GetComponent<Movement>().maxY;
        if (SpikesCheck)
        {
            StartCoroutine(HpTime());
        }
    }
    private void Update()
    {
        if (HookCheck)
        {
            Movement.ln.SetPosition(0, Movement.trsn.position);
            if (Movement.AbleToHook)
            {
                Movement.ln.SetPosition(1, new Vector3(Movement.trsn.position.x, ring.transform.position.y, 0));
            }
            else
            {
                Movement.ln.SetPosition(1, Movement.trsn.position);
            }
        }
        if (IsOnGround)
        {
            Movement.anim.SetBool("Jump", false);
        }
        else
        {
            Movement.anim.SetBool("Jump", true);
        }
        if (SpikesCheck)
        {
            t.text = hp.ToString();
            if (hp <= 0)
            {
                if (!end)
                {
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    SceneManager.LoadScene("BadEnd");
                }
            }
            if (starta)
            {
                starta = false;
                StartCoroutine(HpTime());
            }
            if (starta2)
            {
                starta2 = false;
                StartCoroutine(cd());
            }
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
                walltouches += 1;
                Movement.tjump = Movement.defaultTjump;
                Movement.ceiling = Movement.trsn.position.y + maxY;
            }
            else if (CeilingHitCheck && Movement.trsn.position.y < Movement.ceiling && !IsOnGround && !Movement.IsHooked && !Movement.IsClimbing)
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
                    AudioManager.p = true;
                    runes += 1;
                    hp += hprune;
                    collision.gameObject.SetActive(false);
                }
                if (collision.name.Contains("(CP)"))
                {
                    checkpoint = collision.transform.position;
                }
                else if (collision.name.Contains("(D)"))
                {
                    AudioManager.h = true;
                    Movement.trsn.position = checkpoint;
                    Movement.IsClimbing = false;
                    Movement.rgd.gravityScale = 2;
                    Movement.AbleToJump = true;
                    Movement.tjump = 0;
                    walltouches = 0;
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
                HookTouches += 1;
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
                if (!Input.GetKey(KeyCode.Space) && !Movement.IsDashActive)
                {
                    StartCoroutine(Ctime());
                }
                else
                {
                    IsOnGround = false;
                    if (Movement.IsDashActive)
                    {
                        Movement.tjump -= 1;
                    }
                }
            }
            else if (WallCheck && SkillWallClimbing)
            {
                walltouches -= 1;
                if (walltouches == 0)
                {
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        StartCoroutine(Ctime());
                    }
                    else
                    {
                        Movement.IsClimbing = false;
                        if (!Movement.IsHooked)
                        {
                            Movement.rgd.gravityScale = 2;
                            Movement.AbleToJump = true;
                            Movement.AbleToDash = true;
                        }
                    }
                }
            }
        }
        else if (HookCheck && collision.name.Contains("Hook") && SkillHook)
        {
            HookTouches -= 1;
            if (HookTouches == 0)
            {
                Movement.AbleToHook = false;
            }
        }
    }
    private IEnumerator Ctime()
    {
        yield return new WaitForSeconds(0.1f);
        if (GroundCheck)
        {
            IsOnGround = false;
        }
        else if (WallCheck && walltouches == 0)
        {
            Movement.IsClimbing = false;
            if (!Movement.IsHooked)
            {
                Movement.rgd.gravityScale = 2;
                Movement.AbleToJump = true;
                Movement.AbleToDash = true;
            }
        }
        if (!Input.GetKey(KeyCode.Space) && !IsOnGround)
        {
            Movement.tjump -= 1;
        }
    }
    private IEnumerator HpTime()
    {
        yield return new WaitForSeconds(time);
        hp -= 1;
        StartCoroutine(HpTime());
    }
    private IEnumerator cd()
    {
        yield return new WaitForSeconds(10);
        t2.enabled = false;
    }
}
