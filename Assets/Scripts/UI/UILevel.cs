using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    private PlayerAction levelAction;

    private int position = 0;

    public Image imageSprite;

    private GameManager gameManager;

    private ShopManager shopManager;

    private AudioManager audioManager;

    SpriteRenderer spriteRenderer;

    private AdsManager adsManager;

    [SerializeField]
    private GameObject confirmBtn;

    [SerializeField]
    private GameObject shopBtn;

    [SerializeField]
    GameObject continuePlayCanvas;

    private void Awake()
    {
        levelAction = new PlayerAction();
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        adsManager = AdsManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        adsManager.ShowBanner();
        position = gameManager.selectedLevel;
        imageSprite.sprite = shopManager.level[position].spriteLevel;
        CheckLevel();
        levelAction.Level.Prev.performed += _ => PrevLevel();
        levelAction.Level.Next.performed += _ => NextLevel();
        if (PlayerPrefs.HasKey("level") && PlayerPrefs.GetString("level") != "")
        {
            continuePlayCanvas.SetActive(true);
        }
    }

    private void OnEnable()
    {
        levelAction.Enable();
    }

    private void OnDisable()
    {
        levelAction.Disable();
    }

    public void ContinueLevel()
    {
        audioManager.PlaySound("Click");
        gameManager.GetContinue();
    }

    public void ChooseLevel()
    {
        audioManager.PlaySound("Click");
        PlayerPrefs.DeleteKey("level");
        continuePlayCanvas.SetActive(false);
    }

    void NextLevel()
    {
        audioManager.PlaySound("Click");
        position = (position + 1) % shopManager.level.Length;
        if (position > shopManager.level.Length)
        {
            position = 0;
        }
        imageSprite.sprite = shopManager.level[position].spriteLevel;
        gameManager.selectedLevel = position;
        CheckLevel();
    }

    void CheckLevel()
    {
        if (!shopManager.level[position].buyed)
        {
            confirmBtn.SetActive(false);
            shopBtn.SetActive(true);
        }
        else
        {
            confirmBtn.SetActive(true);
            shopBtn.SetActive(false);
        }
    }

    void PrevLevel()
    {
        audioManager.PlaySound("Click");
        position = (position - 1) % shopManager.level.Length;
        if (position < 0)
        {
            position = shopManager.level.Length - 1;
        }
        imageSprite.sprite = shopManager.level[position].spriteLevel;
        gameManager.selectedLevel = position;
        CheckLevel();
    }

    public void BackLobby()
    {
        audioManager.PlaySound("Click");
        gameManager.scene = "Lobby";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    public void Shop()
    {
        audioManager.PlaySound("Click");
        gameManager.historyScene = "Level";
        gameManager.scene = "Shop";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    public void NextPlay()
    {
        audioManager.PlaySound("Click");
        gameManager.SetContinue();

        // gameManager.scene = shopManager.level[position].sceneName;
        gameManager.scene = "PartLevel";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }
}
