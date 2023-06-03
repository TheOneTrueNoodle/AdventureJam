using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<Monster> monsters;
    public bool playerInArea;

    [Header("Monster Attack Stats")]
    private float currentAttackInterval;
    public float minAttackInterval = 60f, maxAttackInterval = 200f;
    public float unfreezeTime = 2f;
    public float killTime = 3f;

    public bool AllMonstersFrozen;
    public bool monsterAttacking;

    public GameObject screenBlack;

    private void Start()
    {
        currentAttackInterval = Random.Range(minAttackInterval, maxAttackInterval);
        Debug.Log(currentAttackInterval);

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].ID = i;
            monsters[i].manager = this;
        }

        checkMonstersFrozen();
    }

    private void Update()
    {
        if(playerInArea)
        {
            Debug.Log("AllMonstersFrozen = " + AllMonstersFrozen);
            if(AllMonstersFrozen != true && monsterAttacking != true)
            {
                if(currentAttackInterval <= 0)
                {
                    currentAttackInterval = Random.Range(minAttackInterval, maxAttackInterval);
                    monsterAttacking = true;
                    StartCoroutine(TriggerAttack());
                }
                else
                {
                    currentAttackInterval -= Time.deltaTime;
                }
            }
        }
    }

    public IEnumerator TriggerAttack()
    {
        Debug.Log("Starting Attack");

        //Screen flashing (Temporary until i set up an animation for it)
        screenBlack.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(true);

        Monster newMonster = null;
        for(int i = 0; i < monsters.Count; i++)
        {
            if(monsters[i].isFrozen != true)
            {
                newMonster = monsters[i];
                break;
            }
        }
        if(newMonster != null && newMonster.isAttacking != true)
        {
            newMonster.unfreezeTime = unfreezeTime;
            newMonster.killTime = killTime;

            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width * 0.85f), Random.Range(0, Screen.height * 0.9f)));
            screenPosition.z = Vector3.zero.z;
            newMonster.gameObject.transform.position = screenPosition;

            newMonster.BeginAttack();
        }

        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(false);
    }

    public void checkMonstersFrozen()
    {
        bool check = true;
        for(int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].isFrozen != true)
            {
                check = false;
            }
        }

        AllMonstersFrozen = check;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInArea = true;
            Debug.Log("playerInArea = " + playerInArea);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInArea = false;
            Debug.Log("playerInArea = " + playerInArea);
        }
    }
}
