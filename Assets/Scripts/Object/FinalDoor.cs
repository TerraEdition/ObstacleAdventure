using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private int key;
    [SerializeField] private string sceneName;
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
            anim.SetBool("Open", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!canOpen)
            {
                gameManager.coin += gameManager.coinTempo;
                gameManager.jewel += gameManager.jewelTempo;
                gameManager.coinTempo = 0;
                gameManager.jewelTempo = 0;
                gameManager.key = 0;
                gameManager.scene = sceneName;
                AdsManager.instance.countAds();
                gameManager.SaveGame();
                gameManager.GetComponent<LoadingManager>().loadLevel();
            }
        }
    }
}
