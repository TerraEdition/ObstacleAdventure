using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    private GameManager gameManager;

    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Heart")
        {
            audioManager.PlaySound("Coin");
            gameManager.heart++;
            Destroy(other.gameObject);
            gameManager.updateHeart = true;
        }
        else if (other.gameObject.tag == "Jewel")
        {
            audioManager.PlaySound("Coin");
            gameManager.jewelTempo++;
            Destroy(other.gameObject);
            gameManager.updateJewel = true;
        }
        else if (other.gameObject.tag == "BronzeCoin")
        {
            audioManager.PlaySound("Coin");
            gameManager.coinTempo++;
            Destroy(other.gameObject);
            gameManager.updateCoin = true;
        }
        else if (other.gameObject.tag == "SilverCoin")
        {
            audioManager.PlaySound("Coin");
            gameManager.coinTempo += 5;
            Destroy(other.gameObject);
            gameManager.updateCoin = true;
        }
        else if (other.gameObject.tag == "GoldCoin")
        {
            audioManager.PlaySound("Coin");
            Destroy(other.gameObject);
            gameManager.coinTempo += 10;
            gameManager.updateCoin = true;
        }
        else if (other.gameObject.tag == "Key")
        {
            audioManager.PlaySound("Coin");
            gameManager.key++;
            Destroy(other.gameObject);
            gameManager.updateKey = true;
        }
    }
}
