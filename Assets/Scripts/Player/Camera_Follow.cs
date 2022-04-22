using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Transform player;

    private Vector3 pos;

    private Vector3 velocity = Vector3.zero;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
            transform.position =
                new Vector3(player.position.x,
                    player.position.y,
                    transform.position.z);
            audioManager.PlaySound("Respawn");
            // transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 1f, -10f), ref velocity, 0.20f);
        }
        else
        {
            if (player.position.y < 0f)
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
