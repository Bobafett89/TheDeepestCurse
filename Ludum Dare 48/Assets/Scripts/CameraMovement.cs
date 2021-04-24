using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform mh;
    void Update()
    {
        
        float dist = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(mh.position.y - transform.position.y), 2) + Mathf.Pow(Mathf.Abs(mh.position.x - transform.position.x), 2));
        if (dist >= 3)
        {
            if (dist >= 5)
            {
                transform.position = Vector2.MoveTowards(transform.position, mh.position, Mathf.Abs(Movement.rgd.velocity.y) * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, mh.position, Mathf.Abs(Movement.rgd.velocity.x) * Time.fixedDeltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, mh.position, Mathf.Abs(Movement.rgd.velocity.x) / 2 * Time.fixedDeltaTime);
        }
    }
}
