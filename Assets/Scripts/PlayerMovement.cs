using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float runSpeed;
    private float StoredSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private bool IsRunning;

    void Start()
    {
        StoredSpeed = Speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetBool("IsRunning", IsRunning);

        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;

        rb.velocity = new Vector2(horizontal, vertical);


        if ((horizontal != 0 || vertical != 0) && IsRunning == false)
        {
            anim.SetBool("IsWalking", true);
		}
		else
		{
			anim.SetBool("IsWalking", false);
		}

        if (Input.GetKey(KeyCode.LeftShift) && (horizontal != 0 || vertical != 0))
        {
            IsRunning = true;
            Speed = runSpeed;
        }
        else
        {
            IsRunning = false;
            Speed = StoredSpeed;
        }

        /*
        //Limiting player movement
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -21.75f, 22f),
            Mathf.Clamp(transform.position.y, 12.25f, -12.25f));
        */

    }
}
