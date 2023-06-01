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
            if(AllMonstersFrozen != true)
            {
                if(currentAttackInterval <= 0)
                {
                    TriggerAttack();
                }
                else
                {
                    currentAttackInterval -= Time.deltaTime;
                }
            }
        }
    }

    public void TriggerAttack()
    {
        Debug.Log("Starting Attack");

        //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5)); //will get the middle of the screen
        Monster newMonster = null;
        for(int i = 0; i < monsters.Count; i++)
        {
            if(monsters[i].isFrozen != true)
            {
                newMonster = monsters[i];
                break;
            }
        }
        if(newMonster != null)
        {
            newMonster.unfreezeTime = unfreezeTime;
            newMonster.killTime = killTime;

            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0));
            newMonster.gameObject.transform.position = screenPosition;

            newMonster.BeginAttack();
        }
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
