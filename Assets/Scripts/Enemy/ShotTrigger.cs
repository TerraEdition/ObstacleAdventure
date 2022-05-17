using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float durationShot = 2f;

    private int currentWayPointIndex = 0;

    [SerializeField]
    private bool changeRotation;

    private SpriteRenderer sprite;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject projectile;

    private GameObject player;

    private bool isAttack;

    private bool lockTarget;

    private void Start()
    {
        sprite = enemy.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (!lockTarget)
        {
            if (wayPoints.Length > 0)
            {
                if (
                    Vector2
                        .Distance(wayPoints[currentWayPointIndex]
                            .transform
                            .position,
                        enemy.transform.position) <
                    .1f
                )
                {
                    if (changeRotation)
                    {
                        if (
                            wayPoints[currentWayPointIndex]
                                .transform
                                .position
                                .x >
                            enemy.transform.position.x
                        )
                        {
                            sprite.flipX = true;
                        }
                        else
                        {
                            sprite.flipX = false;
                        }
                    }
                    currentWayPointIndex++;
                    if (currentWayPointIndex >= wayPoints.Length)
                    {
                        currentWayPointIndex = 0;
                    }
                }
                enemy.transform.position =
                    Vector2
                        .MoveTowards(enemy.transform.position,
                        wayPoints[currentWayPointIndex].transform.position,
                        Time.deltaTime * speed);
            }
        }
        else
        {
            if (isAttack)
            {
                if (changeRotation)
                {
                    if (player.transform.position.x > enemy.transform.position.x
                    )
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                }
                StartCoroutine(Shot());
            }
        }
    }

    IEnumerator Shot()
    {
        isAttack = false;
        GameObject bullet;
        if (player.transform.position.x >= enemy.transform.position.x)
        {
            bullet =
                Instantiate(projectile,
                new Vector3(enemy.transform.position.x + 1,
                    enemy.transform.position.y,
                    enemy.transform.position.z),
                Quaternion.identity);
            bullet.GetComponent<PickAxe>().positiveRotate = true;
        }
        else
        {
            bullet =
                Instantiate(projectile,
                new Vector3(enemy.transform.position.x - 1,
                    enemy.transform.position.y,
                    enemy.transform.position.z),
                Quaternion.identity);
            bullet.GetComponent<PickAxe>().positiveRotate = false;
        }
        Vector2 direction =
            player.transform.position - bullet.transform.position;

        bullet.GetComponent<PickAxe>().SetDirection(direction);
        yield return new WaitForSeconds(durationShot);
        isAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockTarget = true;
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockTarget = false;
        }
    }
}
