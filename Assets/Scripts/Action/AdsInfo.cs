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

    public void AddCoinNoAds()
    {
        audioManager.PlaySound("Click");
        gameManager.coin += 2000;
        gameManager.updateCoin = true;
        gameManager.SaveGame();
    }

    public void AddJewelNoAds()
    {
        audioManager.PlaySound("Click");
        gameManager.jewel += 2000;
        gameManager.updateJewel = true;
        gameManager.SaveGame();
    }

    public void AddHeartNoAds()
    {
        audioManager.PlaySound("Click");
        gameManager.heart += 100;
        gameManager.updateHeart = true;
        gameManager.SaveGame();
    }
}
