using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    private PlayerAction lobbyAction;

    private int position;

    [SerializeField]
    private Image imageSprite;

    private GameManager gameManager;

    private ShopManager shopManager;

    private AudioManager audioManager;

    SpriteRenderer spriteRenderer;

    private AdsManager adsManager;

    [SerializeField]
    private GameObject confirmBtn;

    [SerializeField]
    private Text nameTxt;

    [SerializeField]
    private Text priceTxt;

    [SerializeField]
    private Slider speedSlider;

    [SerializeField]
    private Slider jumpSlider;

    private void Awake()
    {
        lobbyAction = new PlayerAction();
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        adsManager = AdsManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        adsManager.ShowBanner();
        position = gameManager.selectedCharacter;
        imageSprite.sprite = shopManager.character[position].spriteChar;
        CheckCharacter();
        ShowStatus();
        lobbyAction.Lobby.Prev.performed += _ => PrevCharacter();
        lobbyAction.Lobby.Next.performed += _ => NextCharacter();
    }

    private void OnEnable()
    {
        lobbyAction.Enable();
    }

    private void OnDisable()
    {
        lobbyAction.Disable();
    }

    void NextCharacter()
    {
        position = (position + 1) % shopManager.character.Length;
            if (position > shopManager.character.Length)
            {
                position = 0;
            }
        if (shopManager.character[position].buyed)
        {
            audioManager.PlaySound("Click");
            gameManager.selectedCharacter = position;
            CheckCharacter();
            ShowStatus();
        }
        else
        {
            NextCharacter();
        }
    }

    void CheckCharacter()
    {
        imageSprite.sprite = shopManager.character[position].spriteChar;
    }

    void PrevCharacter()
    {
        position = (position - 1) % shopManager.character.Length;
            if (position < 0)
            {
                position = shopManager.character.Length - 1;
            }
        if (shopManager.character[position].buyed)
        {
            audioManager.PlaySound("Click");
            gameManager.selectedCharacter = position;
            CheckCharacter();
            ShowStatus();
        }
        else
        {
         if(position > 0){
            PrevCharacter();
            }
        }
    }

    void ShowStatus()
    {
        nameTxt.text = shopManager.character[position].name;
        speedSlider.value = shopManager.character[position].speed;
        jumpSlider.value = shopManager.character[position].jumpForce;
        priceTxt.text =
            (shopManager.character[position].buyed)
                ? "Owned"
                : shopManager.character[position].price.ToString() + " Coin";
    }

    public void BackScene()
    {
        audioManager.PlaySound("Click");
        gameManager.scene = "Menu";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }

    public void NextScene()
    {
        audioManager.PlaySound("Click");
        gameManager.scene = "Level";
        gameManager.GetComponent<LoadingManager>().loadLevel();
    }
}
