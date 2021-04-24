using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedX, speedY, maxY;
    public GameObject RightHitZone, LeftHitZone;
    static public int defaultTjump, tjump, PickaxeLevel;
    static public float ceiling;
    static public Transform trsn;
    static public SpriteRenderer sprt;
    static public bool AbleToHit, AbleToHook, AbleToDash, AbleToJump, IsTurned, IsDashActive, IsClimbing, IsHooked, n;
    static public Rigidbody2D rgd;
    private void Awake()
    {
        rgd = GetComponent<Rigidbody2D>();
        trsn = GetComponent<Transform>();
        sprt = GetComponent<SpriteRenderer>();
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
        n = false;
    }
    private void Update()
    {
        if (!IsDashActive)
        {

            if (!IsHooked)
            {

                if (Input.GetKey(KeyCode.D))
                {
                    rgd.velocity = new Vector2(speedX, rgd.velocity.y);
                    IsTurned = false;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rgd.velocity = new Vector2(-speedX, rgd.velocity.y);
                    IsTurned = true;
                }
                else
                {
                    rgd.velocity = new Vector2(0, rgd.velocity.y);
                }
                sprt.flipX = IsTurned;


                if (!IsClimbing)
                {

                    if (Input.GetKeyDown(KeyCode.Space) && !VarManager.IsOnGround && tjump > 0)
                    {
                        ceiling = trsn.position.y + maxY;
                        AbleToJump = true;
                    }
                    if (Input.GetKey(KeyCode.Space) && trsn.position.y < ceiling && AbleToJump && tjump > 0)
                    {
                        rgd.velocity = new Vector2(rgd.velocity.x, speedY);
                    }
                    else if ((Input.GetKeyUp(KeyCode.Space) || trsn.position.y >= ceiling) && AbleToJump)
                    {
                        tjump -= 1;
                        AbleToJump = false;
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

                }


                if (AbleToHook && Input.GetKeyDown(KeyCode.LeftControl))
                {
                    IsHooked = true;
                    rgd.velocity = new Vector2(0, 0);
                    rgd.gravityScale = 0;
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


                if (Input.GetKeyDown(KeyCode.LeftShift) && AbleToDash)
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

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    IsHooked = false;
                    rgd.gravityScale = 2;
                }
            }
        }
    }
    IEnumerator dash()
    {
        if (VarManager.IsOnGround)
        {
            VarManager.IsOnGround = false;
            n = true;
        }
        IsDashActive = true;
        AbleToDash = false;
        rgd.velocity = new Vector2((IsTurned ? -speedX : speedX) * 2, 0);
        rgd.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds((4f / (speedX * 2f)));
        rgd.velocity = new Vector2(0, 0);
        rgd.constraints = RigidbodyConstraints2D.FreezeRotation;
        IsDashActive = false;
        yield return new WaitForSeconds(0.2f);
        if (n)
        {
            tjump = defaultTjump;
            VarManager.IsOnGround = true;
            n = false;
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
