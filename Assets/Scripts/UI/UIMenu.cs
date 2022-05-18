using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    private GameManager gameManager;

    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
        StartCoroutine(PlayBgm());
    }

    public void StartBtn()
    {
        audioManager.PlaySound("Start_Game");
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);
        gameManager.scene = "Lobby";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    IEnumerator PlayBgm()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySound("Menu");
    }
}
