using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private GameManager gameManager;

    private ShopManager shopManager;

    private AdsManager adsManager;

    private AudioManager audioManager;

    [SerializeField]
    private Text

            coinText,
            jewelText,
            heartText;

    [SerializeField]
    private GameObject

            itemCanvas,
            charCanvas,
            levelCanvas,
            adsCanvas;

    [SerializeField]
    private GameObject

            itemCanvasBtn,
            charCanvasBtn,
            levelCanvasBtn;

    [HideInInspector]
    public GameObject

            canvasImageItem,
            canvasImageItemOld;

    private void Awake()
    {
        gameManager = GameManager.instance;
        adsManager = AdsManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        adsManager.ShowBanner();
        audioManager.PlaySound("Menu");
        ShowUI();
        ShowItems();
    }

    public void BackLobby()
    {
        if (gameManager.historyScene != "")
        {
            gameManager.scene = gameManager.historyScene;
            gameManager.historyScene = "";
        }
        else
        {
            gameManager.scene = "Menu";
        }
        audioManager.PlaySound("Click");
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    void Update()
    {
        if (gameManager.updateJewel)
        {
            gameManager.updateJewel = false;
            jewelText.text = (gameManager.jewel).ToString();
        }
        else if (gameManager.updateHeart)
        {
            gameManager.updateHeart = false;
            heartText.text = gameManager.heart.ToString();
        }
        else if (gameManager.updateCoin)
        {
            gameManager.updateCoin = false;
            coinText.text = (gameManager.coin).ToString();
        }
    }

    void ShowUI()
    {
        jewelText.text = (gameManager.jewel).ToString();
        heartText.text = gameManager.heart.ToString();
        coinText.text = (gameManager.coin).ToString();
    }

    public void ShowItems()
    {
        audioManager.PlaySound("Click");
        itemCanvas.SetActive(true);
        charCanvas.SetActive(false);
        levelCanvas.SetActive(false);
        adsCanvas.SetActive(false);

        itemCanvasBtn.SetActive(true);
        charCanvasBtn.SetActive(false);
        levelCanvasBtn.SetActive(false);
    }

    public void ShowChar()
    {
        audioManager.PlaySound("Click");
        itemCanvas.SetActive(false);
        charCanvas.SetActive(true);
        levelCanvas.SetActive(false);
        adsCanvas.SetActive(false);

        itemCanvasBtn.SetActive(false);
        charCanvasBtn.SetActive(true);
        levelCanvasBtn.SetActive(false);
    }

    public void ShowLevel()
    {
        audioManager.PlaySound("Click");
        itemCanvas.SetActive(false);
        charCanvas.SetActive(false);
        levelCanvas.SetActive(true);
        adsCanvas.SetActive(false);

        itemCanvasBtn.SetActive(false);
        charCanvasBtn.SetActive(false);
        levelCanvasBtn.SetActive(true);
    }

    public void ShowAds()
    {
        audioManager.PlaySound("Click");
        itemCanvas.SetActive(false);
        charCanvas.SetActive(false);
        levelCanvas.SetActive(false);
        adsCanvas.SetActive(true);

        itemCanvasBtn.SetActive(false);
        charCanvasBtn.SetActive(false);
        levelCanvasBtn.SetActive(false);
    }

    public void StoreItem(GameObject _gameObject)
    {
        if (canvasImageItem == null)
        {
            canvasImageItem = _gameObject;
        }
        else
        {
            canvasImageItemOld = canvasImageItem.gameObject;
            if (canvasImageItemOld != null)
            {
                canvasImageItemOld.GetComponent<Image>().color =
                    new Color32(255, 255, 225, 0);
            }
            canvasImageItem = _gameObject;
        }
        canvasImageItem.GetComponent<Image>().color =
            new Color32(255, 255, 225, 255);
    }

    public void BuyItem()
    {
        audioManager.PlaySound("Click");
        string name = canvasImageItem.GetComponent<ItemStatus>().name;
        shopManager.BuyItem (name);
    }
}
