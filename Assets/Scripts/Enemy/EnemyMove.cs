using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    public float speed = 2f;

    public bool goToNewTarget = false;

    [HideInInspector]
    public Vector3 newTarget;

    private int currentWayPointIndex = 0;

    [SerializeField]
    private bool changeRotation;

    private SpriteRenderer sprite;

    private EnemyDeath enemyDeath;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyDeath = GetComponent<EnemyDeath>();
    }

    private void Update()
    {
        if (!enemyDeath.isDie)
        {
            if (goToNewTarget)
            {
                MoveToTarget();
            }
            else
            {
                MoveToWayPoints();
            }
        }
    }

    private void MoveToWayPoints()
    {
        if (changeRotation)
        {
            if (
                wayPoints[currentWayPointIndex].transform.position.x >
                transform.position.x
            )
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        if (
            Vector2
                .Distance(wayPoints[currentWayPointIndex].transform.position,
                transform.position) <
            .1f
        )
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position =
            Vector2
                .MoveTowards(transform.position,
                wayPoints[currentWayPointIndex].transform.position,
                Time.deltaTime * speed);
    }

    private void MoveToTarget()
    {
        if (changeRotation)
        {
            if (newTarget.x > transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        transform.position =
            Vector2
                .MoveTowards(transform.position,
                newTarget,
                Time.deltaTime * speed);
    }
}
