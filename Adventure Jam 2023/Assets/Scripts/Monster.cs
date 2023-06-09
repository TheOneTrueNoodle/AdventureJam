using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public MonsterManager manager;
    [HideInInspector] public int ID;

    public bool isFrozen = false;
    private bool lightNear;
    public bool isAttacking;

    public float unfreezeTime;
    public float killTime;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D col;
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
        col.enabled = false;
    }
    
    private void Update()
    {
        if(isAttacking)
        {
            if(!isFrozen)
            {
                killTime -= Time.deltaTime;
                if (killTime <= 0)
                {
                    Kill();
                }
            }
            else if(!lightNear)
            {
                unfreezeTime -= Time.deltaTime;
                if(unfreezeTime <= 0)
                {
                    Unfreeze();
                }
            }
        }
    }

    public void Freeze()
    {
        rb.gravityScale = 12f;
        anim.speed = 0.0f;
        float frame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("Monster Idle Statue", 0, frame);

        isFrozen = true;
        manager.monsterAttacking = false;
        manager.checkMonstersFrozen();
    }

    public void Unfreeze()
    {
        rb.gravityScale = 0f;
        anim.speed = 1f;
        float frame = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("Monster Idle", 0, frame);
        isFrozen = false;
        isAttacking = false;
        spriteRenderer.enabled = false;
        col.enabled = false;
        manager.checkMonstersFrozen();
    }

    public void BeginAttack()
    {
        Debug.Log("BeginAttack has begun");

        isAttacking = true;
        spriteRenderer.enabled = true;
        col.enabled = true;
    }

    public void Kill()
    {
        Debug.Log("YOU ARE DEAD BOZOOOOOO");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAttacking)
        {
            if (collision.tag == "Mouse Light")
            {
                lightNear = true;
                if (!isFrozen)
                {
                    Freeze();
                }
            }
            else if(!isFrozen && collision.tag == "Player")
            {
                Kill();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (collision.tag == "Mouse Light")
            {
                lightNear = false;
            }
        }
    }
}
