using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsInfo : MonoBehaviour
{
    private GameManager gameManager;

    private AdsManager adsManager;

    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        adsManager = AdsManager.instance;
        audioManager = AudioManager.instance;
    }

    public void RewardedCoin()
    {
        audioManager.PlaySound("Click");
        adsManager.ShowRewarded (AddCoin);
    }

    void AddCoin()
    {
        gameManager.coin += Random.Range(30, 100);
        gameManager.updateCoin = true;
        gameManager.SaveGame();
    }

    public void RewardedHeart()
    {
        audioManager.PlaySound("Click");
        adsManager.ShowRewarded (AddHeart);
    }

    void AddHeart()
    {
        gameManager.coin += Random.Range(1, 3);
        gameManager.updateCoin = true;
        gameManager.SaveGame();
    }
}
