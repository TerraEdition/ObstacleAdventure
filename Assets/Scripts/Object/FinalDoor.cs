using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField]
    private int key;

    private bool canOpen = true;

    private Animator anim;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.key == key && canOpen)
        {
            canOpen = false;
            AudioManager.instance.PlaySound("Door_Open");
            anim.SetBool("Open", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!canOpen)
            {
                gameManager.finished = true;
            }
        }
    }
}
