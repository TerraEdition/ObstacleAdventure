using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    private PlayerAction lobbyAction;

    private int position;

    [SerializeField]
    private Image imageSprite;

    [SerializeField]
    private Button buyBtn;

    [SerializeField]
    private Text priceText;

    private GameManager gameManager;

    private ShopManager shopManager;

    private AudioManager audioManager;

    private void Awake()
    {
        lobbyAction = new PlayerAction();
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        CheckLevel();
    }

    private void Update()
    {
        if (gameManager.updateLevel)
        {
            gameManager.updateLevel = false;
            CheckLevel();
        }
    }

    public void NextLevel()
    {
        audioManager.PlaySound("Click");
        position = (position + 1) % shopManager.level.Length;
        if (position > shopManager.level.Length)
        {
            position = 0;
        }
        imageSprite.sprite = shopManager.level[position].spriteLevel;
        CheckLevel();
    }

    void CheckLevel()
    {
        if (
            shopManager.level[position].buyed ||
            gameManager.jewel < shopManager.level[position].price
        )
        {
            buyBtn.interactable = false;
        }
        else
        {
            buyBtn.interactable = true;
        }
        ShowStatus();
    }

    void ShowStatus()
    {
        if (shopManager.level[position].buyed)
        {
            priceText.text = "Owned";
        }
        else
        {
            priceText.text = shopManager.level[position].price.ToString();
        }
    }

    public void PrevLevel()
    {
        audioManager.PlaySound("Click");
        position = (position - 1) % shopManager.level.Length;
        if (position < 0)
        {
            position = shopManager.level.Length - 1;
        }
        imageSprite.sprite = shopManager.level[position].spriteLevel;
        CheckLevel();
    }

    public void BuyLevel()
    {
        shopManager.BuyLevel(shopManager.level[position].name);
    }
}
