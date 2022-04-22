using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlay : MonoBehaviour
{
    private GameManager gameManager;

    private AdsManager adsManager;

    private AudioManager audioManager;

    private PlayerAction pauseAction;

    [SerializeField]
    private Text jewelText;

    [SerializeField]
    private Text heartText;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text keyText;

    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private GameObject gameOverCanvas;

    private void Awake()
    {
        pauseAction = new PlayerAction();
        pauseAction.Pause.Exit.performed += _ => GoMenu();
        pauseAction.Pause.Show.performed += _ => ShowPause();
        pauseAction.Pause.Option.performed += _ => ShowOption();
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
        adsManager = AdsManager.instance;
    }

    private void OnDestroy()
    {
        pauseAction.Pause.Exit.performed -= _ => GoMenu();
        pauseAction.Pause.Show.performed -= _ => ShowPause();
    }

    void Start()
    {
        adsManager.HideBanner();
        ResetItem();
        PlayerPrefs.SetString("level", SceneManager.GetActiveScene().name);
    }

    void ResetItem()
    {
        gameManager.updateKey = true;
        gameManager.updateJewel = true;
        gameManager.updateHeart = true;
        gameManager.updateCoin = true;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    void ShowPause()
    {
        audioManager.PlaySound("Click");
        if (!pauseCanvas.activeSelf)
        {
            adsManager.ShowBanner();
            pauseCanvas.SetActive(true);
            Pause();
        }
        else
        {
            adsManager.HideBanner();
            pauseCanvas.SetActive(false);
            Resume();
        }
    }

    void ShowOption()
    {
        if (pauseCanvas.activeSelf)
        {
            audioManager.PlaySound("Click");
            gameManager
                .GetComponent<OptionManager>()
                .OptionCanvas
                .SetActive(true);
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
    }

    void Resume()
    {
        Time.timeScale = 1;
    }

    void GoMenu()
    {
        if (pauseCanvas.activeSelf || gameOverCanvas.activeSelf)
        {
            audioManager.PlaySound("Click");
            gameManager.key = 0;
            gameManager.coinTempo = 0;
            gameManager.jewelTempo = 0;
            gameManager.scene = "Menu";
            gameManager.GetComponent<LoadingManager>().loadLevel();
        }
    }

    void Update()
    {
        if (gameManager.updateJewel)
        {
            gameManager.updateJewel = false;
            jewelText.text =
                (gameManager.jewelTempo + gameManager.jewel).ToString();
        }
        else if (gameManager.updateHeart)
        {
            gameManager.updateHeart = false;
            heartText.text = gameManager.heart.ToString();
        }
        else if (gameManager.updateCoin)
        {
            gameManager.updateCoin = false;
            coinText.text =
                (gameManager.coinTempo + gameManager.coin).ToString();
        }
        else if (gameManager.updateKey)
        {
            gameManager.updateKey = false;
            keyText.text = gameManager.key.ToString();
        }
        else if (gameManager.playerDie && gameManager.heart <= 0)
        {
            gameManager.NewGame();
            gameManager.playerDie = false;
            Pause();
            gameOverCanvas.SetActive(true);
        }
    }
}
