using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public MonsterManager manager;
    [HideInInspector] public int ID;

    [HideInInspector] public bool isFrozen = false;
    private bool lightNear;
    public bool isAttacking;

    public float unfreezeTime;
    public float killTime;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D col;
    
    private void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            }
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        manager.checkMonstersFrozen();
    }

    public void Unfreeze()
    {
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
        col.enabled = false;
    }

    public void Kill()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mouse Light")
        {
            lightNear = true;
            if(!isFrozen)
            {
                Freeze();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse Light")
        {
            lightNear = false;
        }
    }
}
