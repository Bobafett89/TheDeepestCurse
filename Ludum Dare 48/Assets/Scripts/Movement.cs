using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speedX, speedY, maxY;
    public GameObject RightHitZone, LeftHitZone;
    static public int defaultTjump, tjump, PickaxeLevel;
    static public float ceiling;
    static public Transform trsn;
    static public SpriteRenderer sprt;
    static public bool AbleToHit, AbleToHook, AbleToDash, AbleToJump, IsTurned, IsDashActive, IsClimbing, IsHooked;
    static public Rigidbody2D rgd;
    static public Animator anim;
    static public LineRenderer ln;
    private void Awake()
    {
        rgd = GetComponent<Rigidbody2D>();
        trsn = GetComponent<Transform>();
        sprt = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ln = GetComponent<LineRenderer>();
        PickaxeLevel = 1;
        ceiling = 0;
        tjump = 1;
        defaultTjump = tjump;
        AbleToHit = false;
        AbleToHook = false;
        AbleToDash = false;
        AbleToJump = false;
        IsTurned = false;
        IsDashActive = false;
        IsHooked = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (!IsDashActive)
        {

            if (!IsHooked)
            {

                if (Input.GetKey(KeyCode.D))
                {
                    rgd.velocity = new Vector2(speedX, rgd.velocity.y);
                    IsTurned = false;
                    anim.SetBool("Running", true);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rgd.velocity = new Vector2(-speedX, rgd.velocity.y);
                    IsTurned = true;
                    anim.SetBool("Running", true);
                }
                else
                {
                    rgd.velocity = new Vector2(0, rgd.velocity.y);
                    anim.SetBool("Running", false);
                }
                sprt.flipX = IsTurned;


                if (!IsClimbing)
                {
                    anim.SetBool("Climbing", false);
                    if (Input.GetKeyDown(KeyCode.Space) && !VarManager.IsOnGround && tjump > 0)
                    {
                        ceiling = trsn.position.y + maxY;
                        AbleToJump = true;
                        AudioManager.j = true;
                    }
                    if (Input.GetKey(KeyCode.Space) && trsn.position.y < ceiling && AbleToJump && tjump > 0)
                    {
                        rgd.velocity = new Vector2(rgd.velocity.x, speedY);
                        if (VarManager.IsOnGround)
                        {
                            AudioManager.j = true;
                        }
                    }
                    else if ((Input.GetKeyUp(KeyCode.Space) || trsn.position.y >= ceiling) && AbleToJump)
                    {
                        tjump -= 1;
                        AbleToJump = false;
                    }

                }
                else
                {
                    anim.SetBool("Climbing", true);
                    if (Input.GetKey(KeyCode.Space))
                    {
                        rgd.velocity = new Vector2(rgd.velocity.x, speedY);
                        ceiling = trsn.position.y + maxY;
                        anim.SetFloat("s", 1);
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        rgd.velocity = new Vector2(rgd.velocity.x, -speedY);
                        anim.SetFloat("s", -1);
                    }
                    else
                    {
                        rgd.velocity = new Vector2(rgd.velocity.x, 0);
                        anim.SetFloat("s", 0);
                    }

                }


                if (AbleToHook && Input.GetKeyDown(KeyCode.RightControl))
                {
                    AudioManager.hk = true;
                    IsHooked = true;
                    tjump = defaultTjump - 1;
                    rgd.velocity = new Vector2(0, 0);
                    rgd.gravityScale = 0;
                    ln.enabled = true;
                }


                if (Input.GetKeyDown(KeyCode.RightShift) && AbleToHit)
                {
                    if (IsTurned)
                    {
                        LeftHitZone.SetActive(true);
                    }
                    else
                    {
                        RightHitZone.SetActive(true);
                    }
                    StartCoroutine(pickcd());
                }


                if (Input.GetKeyDown(KeyCode.LeftShift) && AbleToDash && !IsHooked)
                {
                    StartCoroutine(dash());
                }


            }
            else
            {

                if (Input.GetKey(KeyCode.Space))
                {
                    rgd.velocity = new Vector2(rgd.velocity.x, speedY);
                    ceiling = trsn.position.y + maxY;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    rgd.velocity = new Vector2(rgd.velocity.x, -speedY);
                }
                else
                {
                    rgd.velocity = new Vector2(rgd.velocity.x, 0);
                }

                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    AudioManager.hk = true;
                    IsHooked = false;
                    AbleToDash = true;
                    rgd.gravityScale = 2;
                    ln.enabled = false;
                }
            }
        }
    }
    IEnumerator dash()
    {
        AudioManager.d = true;
        IsDashActive = true;
        anim.SetBool("Dash", true);
        AbleToDash = false;
        rgd.velocity = new Vector2((IsTurned ? -speedX : speedX) * 2, 0);
        rgd.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds((4f / (speedX * 2f)));
        rgd.velocity = new Vector2(0, 0);
        rgd.constraints = RigidbodyConstraints2D.FreezeRotation;
        IsDashActive = false;
        anim.SetBool("Dash", false);
        yield return new WaitForSeconds(0.2f);
        if (VarManager.IsOnGround)
        {
            AbleToDash = true;
        }
    }
    IEnumerator pickcd()
    {
        AbleToHit = false;
        yield return new WaitForSeconds(0.25f);
        RightHitZone.SetActive(false);
        LeftHitZone.SetActive(false);
        AbleToHit = true;
    }
}
