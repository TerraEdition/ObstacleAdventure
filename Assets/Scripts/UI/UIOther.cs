using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOther : MonoBehaviour
{
    private GameManager gameManager;

    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
    }

    public void OptionBtn()
    {
        audioManager.PlaySound("Click");
        gameManager.GetComponent<OptionManager>().OptionCanvas.SetActive(true);
    }

    public void ShopBtn()
    {
        audioManager.PlaySound("Click");
        gameManager.historyScene = SceneManager.GetActiveScene().name;
        gameManager.scene = "Shop";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    public void ExitBtn()
    {
        audioManager.PlaySound("Click");


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
    #endif
    }
}
