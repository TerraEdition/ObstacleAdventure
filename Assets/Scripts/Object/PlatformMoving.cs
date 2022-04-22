using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    [SerializeField]
    private float speed = 2f;

    private int currentWayPointIndex = 0;

    [SerializeField]
    private bool changeRotation;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (
            Vector2
                .Distance(wayPoints[currentWayPointIndex].transform.position,
                transform.position) <
            .1f
        )
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
}
