using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    private CollisionDetection coll;
    [SerializeField] private GameObject gfx;

    [Header("Stats")]
    public float speed = 10f;
    public float maxSpeed = 10f;
    public float jumpForce = 5f;
    public float slideSpeed = 5f;
    public float wallJumpLerp = 10f;

    public int side = 1;

    [Header("Booleans")]
    public bool canMove = true;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool limitVelocity = true;   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CollisionDetection>();
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //float xRaw = Input.GetAxisRaw("Horizontal");
        //float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        //Horizontal Movement
        Walk(dir);
        anim.SetFloat("Horizontal", Mathf.Abs(x));
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("canMove", canMove);

        //Call Wall Slide

        /*
        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }
        */

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        //Reset bools
        if (coll.onGround)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
            anim.ResetTrigger("Jump");
        }

        //Jumping & Wall jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (coll.onGround)
            {
                anim.SetTrigger("Jump");
                Jump(dir, false);
            }
            /*
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
            */
        }

        //Limit X and Y velocity
        if (limitVelocity)
        {
            Vector2 clampedVelocityX = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            rb.velocity = new Vector2(clampedVelocityX.x, rb.velocity.y);
        }

        //Facing direction and moving direction
        if (x > 0)
        {
            side = 1; 
            //anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            //anim.Flip(side);
        }


        gfx.transform.localScale = new Vector2(side, gfx.transform.localScale.y);
    }
    private void Walk(Vector2 dir)
    {
        if (!canMove) { return; }

        if (!wallJumped)
        {
            //Has not wall jumped
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else if(dir == Vector2.zero)
        {
            //Dont change velocity
        }
        else
        {
            //Has wall jumped
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)),  wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        dir.Normalize();
        Vector2 force;
        if (!wall)
        {
            force = Vector2.up * jumpForce;
        }
        else
        {
            force = dir * jumpForce;
        }
        force.x = dir.x * speed;
        rb.velocity += force;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || (side == -1 && coll.onLeftWall))
        {
            side *= -1;
        }

        StartCoroutine(DisableMovement(.15f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        Debug.Log(Vector2.up / 1.5f + wallDir / 1.5f);
        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (!canMove) { return; }
        if (rb.velocity.y > 0) { return; }

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;
        rb.velocity = new Vector2(push, -slideSpeed);
    }

    IEnumerator DisableMovement(float time)
    {
        limitVelocity = false;
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
        limitVelocity = true;
    }
}
