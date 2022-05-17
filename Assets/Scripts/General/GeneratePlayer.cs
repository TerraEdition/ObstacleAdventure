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

    private UIPlay uiPlay;

    void Start()
    {
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
        animPortal = GetComponent<Animator>();
        Respawn();
        uiPlay = GameObject.Find("UI").GetComponent<UIPlay>();
        uiPlay.ShowAdsHeart();
    }

    void Respawn()
    {
        animPortal.Play("In");
    }

    void InPortal()
    {
        audioManager.PlaySound("Respawn");
        Instantiate(shopManager
            .character[gameManager.selectedCharacter]
            .prefabChar,
        transform.position,
        Quaternion.identity);
        animPortal.Play("Out");
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
