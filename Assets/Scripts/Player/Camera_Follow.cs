using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Transform player;

    private Vector3 pos;

    private Vector3 velocity = Vector3.zero;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            transform.position =
                Vector3
                    .SmoothDamp(transform.position,
                    new Vector3(0f, 0f, -10f),
                    ref velocity,
                    0.20f);

            player = GameObject.FindWithTag("Player").transform;
            transform.position =
                new Vector3(player.position.x,
                    player.position.y,
                    transform.position.z);
        }
        else
        {
            if (player.position.y < -2.3f || transform.position.x < -9.1f)
            {
                transform.position =
                    new Vector3(player.position.x,
                        transform.position.y,
                        transform.position.z);
            }
            else
            {
                transform.position =
                    new Vector3(player.position.x,
                        player.position.y,
                        transform.position.z);
            }
        }
    }
}
