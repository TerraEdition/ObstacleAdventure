using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlayer : MonoBehaviour
{
    private ShopManager shopManager;

    private GameManager gameManager;

    private GameObject player;

    void Awake()
    {
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        Instantiate(shopManager
            .character[gameManager.selectedCharacter]
            .prefabChar,
        transform.position,
        Quaternion.identity);
    }

    private void Update()
    {
        if (gameManager.playerDie)
        {
            if (gameManager.heart >= 0)
            {
                gameManager.playerDie = false;
                player =
                    Instantiate(shopManager
                        .character[gameManager.selectedCharacter]
                        .prefabChar,
                    transform.position,
                    Quaternion.identity);
                player.SetActive(false);
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        player.SetActive(true);
    }
}
