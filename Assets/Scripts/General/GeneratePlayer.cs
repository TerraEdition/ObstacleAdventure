using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlayer : MonoBehaviour
{
    private ShopManager shopManager;

    private GameManager gameManager;

    private AudioManager audioManager;

    private GameObject player;

    private GameObject portal;

    private Animator animPortal;

    void Start()
    {
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
        animPortal = GetComponent<Animator>();
        Respawn();
    }

    void Respawn()
    {
        animPortal.SetInteger("state", 1);
    }

    void InPortal()
    {
        audioManager.PlaySound("Respawn");
        Instantiate(shopManager
            .character[gameManager.selectedCharacter]
            .prefabChar,
        transform.position,
        Quaternion.identity);
        animPortal.SetInteger("state", 2);
    }

    void OutPortal()
    {
        animPortal.SetInteger("state", 0);
    }

    private void Update()
    {
        if (gameManager.playerDie)
        {
            if (gameManager.heart >= 0)
            {
                gameManager.playerDie = false;
                Respawn();
            }
        }
    }
}
