using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTrigger : MonoBehaviour
{
    private float normalSpeed;

    [SerializeField]
    private float increaseSpeed = 5f;

    private bool lockTarget = false;

    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    private EnemyMove enemyMove;

    private Vector3 lastPlayerPosition;

    private Vector3 lastEnemyPosition;

    [SerializeField]
    private float idleTime;

    private bool isAttack;

    private void Start()
    {
        enemyMove = enemy.GetComponent<EnemyMove>();
        normalSpeed = enemyMove.speed;
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (lockTarget)
        {
            if (isAttack)
            {
                if (
                    (
                    (lastEnemyPosition.x >= lastPlayerPosition.x) &&
                    (enemy.transform.position.x <= lastPlayerPosition.x)
                    ) ||
                    (
                    (lastEnemyPosition.x <= lastPlayerPosition.x) &&
                    (enemy.transform.position.x >= lastPlayerPosition.x)
                    )
                )
                {
                    isAttack = false;
                    enemyMove.goToNewTarget = false;
                    StartCoroutine(Idle());
                }
                else
                {
                    enemyMove.speed = increaseSpeed;
                    enemyMove.newTarget = lastPlayerPosition;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lastPlayerPosition = other.transform.position;
            lastEnemyPosition = enemy.transform.position;
            lockTarget = true;
            isAttack = true;
            enemyMove.speed = increaseSpeed;
            enemyMove.goToNewTarget = true;
            enemyMove.newTarget = lastPlayerPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockTarget = false;
            enemyMove.speed = normalSpeed;
            enemyMove.goToNewTarget = false;
        }
    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);
        lastPlayerPosition = player.transform.position;
        enemyMove.goToNewTarget = true;
        isAttack = true;
    }
}
