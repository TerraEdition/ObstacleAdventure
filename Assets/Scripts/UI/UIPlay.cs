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
    private string nextSceneName;

    [SerializeField]
    private Text jewelText;

    [SerializeField]
    private Text heartText;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text keyText;

    [SerializeField]
    private Text textFinished;

    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private GameObject finishedCanvas;

    [SerializeField]
    private GameObject gameOverCanvas;

    private int coin = 0;

    private void Awake()
    {
        pauseAction = new PlayerAction();
        pauseAction.Pause.Exit.performed += _ => GoMenu();
        pauseAction.Pause.Show.performed += _ => ShowPause();
        pauseAction.Pause.Option.performed += _ => ShowOption();
    }

    private void OnDestroy()
    {
        pauseAction.Pause.Exit.performed -= _ => GoMenu();
        pauseAction.Pause.Show.performed -= _ => ShowPause();
    }

    void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
        adsManager = AdsManager.instance;
        audioManager.PlaySound("Menu");
        adsManager.HideBanner();
        Resume();
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
            gameManager.playerDie = false;
            adsManager.ShowBanner();
            gameManager.NewGame();
            Pause();
            gameOverCanvas.SetActive(true);
        }
        else if (gameManager.finished)
        {
            adsManager.ShowBanner();
            gameManager.finished = false;
            gameManager.coin += gameManager.coinTempo;
            gameManager.jewel += gameManager.jewelTempo;
            coin += gameManager.coinTempo;
            gameManager.coinTempo = 0;
            gameManager.jewelTempo = 0;
            gameManager.key = 0;
            Pause();
            AdsManager.instance.countAds();
            gameManager.SaveGame();
            ShowFinishedCanvas();
        }
    }

    void ShowFinishedCanvas()
    {
        finishedCanvas.SetActive(true);
        textFinished.text = "You Got " + coin.ToString() + " Coin";
    }

    public void AdsDoubleCoinTempo()
    {
        adsManager.ShowRewarded (DoubleCoinTempo);
    }

    void DoubleCoinTempo()
    {
        textFinished.text = "You Got " + (coin * 2).ToString() + " Coin";
        gameManager.coin += coin;
        coinText.text = ((coin * 2) + gameManager.coin).ToString();
        gameManager.SaveGame();
    }

    public void NextLevel()
    {
        gameManager.scene = nextSceneName;
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }
}
