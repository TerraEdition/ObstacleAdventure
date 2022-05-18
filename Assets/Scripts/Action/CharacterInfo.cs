using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    private PlayerAction lobbyAction;

    private int position;

    [SerializeField]
    private Image imageSprite;

    [SerializeField]
    private Text nameTxt;

    [SerializeField]
    private Text priceTxt;

    [SerializeField]
    private Slider speedSlider;

    [SerializeField]
    private Slider jumpSlider;

    [SerializeField]
    private Button buyBtn;

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
        position = gameManager.selectedCharacter;
        CheckCharacter();
        ShowStatus();
        lobbyAction.Lobby.Prev.performed += _ => PrevCharacter();
        lobbyAction.Lobby.Next.performed += _ => NextCharacter();
    }

    private void OnEnable()
    {
        lobbyAction.Enable();
        CheckCharacter();
        ShowStatus();
    }

    private void OnDisable()
    {
        lobbyAction.Disable();
    }

    void NextCharacter()
    {
        audioManager.PlaySound("Click");
        position = (position + 1) % shopManager.character.Length;
        if (position > shopManager.character.Length)
        {
            position = 0;
        }
        CheckCharacter();
    }

    private void Update()
    {
        if (gameManager.updateCharacter)
        {
            gameManager.updateCharacter = false;
            CheckCharacter();
        }
    }

    void CheckCharacter()
    {
        imageSprite.sprite = shopManager.character[position].spriteChar;
        if (
            shopManager.character[position].buyed ||
            gameManager.coin < shopManager.character[position].price
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

    void PrevCharacter()
    {
        audioManager.PlaySound("Click");
        position = (position - 1) % shopManager.character.Length;
        if (position < 0)
        {
            position = shopManager.character.Length - 1;
        }
        CheckCharacter();
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

    public void BuyChar()
    {
        shopManager.BuyChar(shopManager.character[position].name);
    }
}
