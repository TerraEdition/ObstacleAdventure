using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    [SerializeField]
    private float speed = 2f;

    private int currentWayPointIndex = 0;

    [SerializeField]
    private bool changeRotation;

    private SpriteRenderer sprite;

    private bool lockPlayer = false;

    private GameObject player;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (!lockPlayer)
        {
            if (
                Vector2
                    .Distance(wayPoints[currentWayPointIndex]
                        .transform
                        .position,
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
        else
        {
            transform.position =
                Vector2
                    .MoveTowards(transform.position,
                    player.transform.position,
                    Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockPlayer = false;
        }
    }
}
