using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string name;

    public GameObject prefabChar;

    public Sprite spriteChar;

    public float speed;

    public float jumpForce;

    public int price;

    public bool buyed;
}

[System.Serializable]
public class Level
{
    public string name;

    public Sprite spriteLevel;

    public string sceneName;

    public int price;

    public bool buyed;

    public bool development;
}

[System.Serializable]
public class Item
{
    public string name;

    public string type;

    public Sprite spriteItem;

    public int price;

    public int value;
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private GameManager gameManager;

    public Character[] character;

    public Level[] level;

    public Item[] item;

    public string nameBuyLevel;

    void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy (gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        LoadGameCharacter();
        LoadGameLevel();
    }

    // public
    public void BuyChar(string name)
    {
        for (int i = 0; i < character.Length; i++)
        {
            if (character[i].name == name)
            {
                if (!character[i].buyed)
                {
                    if (gameManager.coin >= character[i].price)
                    {
                        gameManager.coin -= character[i].price;
                        character[i].buyed = true;
                        gameManager.updateCoin = true;
                        gameManager.updateCharacter = true;
                        SaveGameCharacter();
                        gameManager.SaveGame();
                    }
                }
            }
        }
    }

    public void BuyLevel(string name)
    {
        for (int i = 0; i < level.Length; i++)
        {
            if (level[i].name == name)
            {
                if (!level[i].buyed && !level[i].development)
                {
                    if (gameManager.jewel >= level[i].price)
                    {
                        gameManager.jewel -= level[i].price;
                        level[i].buyed = true;
                        gameManager.updateJewel = true;
                        gameManager.updateLevel = true;
                        SaveGameLevel();
                        gameManager.SaveGame();
                    }
                }
            }
        }
    }

    public void BuyItem(string name)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].name == name)
            {
                if (gameManager.coin >= item[i].price)
                {
                    gameManager.coin -= item[i].price;
                    if (item[i].type == "heart")
                    {
                        gameManager.heart += item[i].value;
                        gameManager.updateHeart = true;
                    }
                    else if (item[i].type == "jewel")
                    {
                        gameManager.jewel += item[i].value;
                        gameManager.updateJewel = true;
                    }
                    gameManager.SaveGame();
                    gameManager.updateCoin = true;
                }
            }
        }
    }

    void SaveGameCharacter()
    {
        for (int i = 0; i < character.Length; i++)
        {
            PlayerPrefs.SetInt("char" + i, boolToInt(character[i].buyed));
        }
    }

    void LoadGameCharacter()
    {
        for (int i = 0; i < character.Length; i++)
        {
            if (PlayerPrefs.HasKey("char" + i))
            {
                character[i].buyed = intToBool(PlayerPrefs.GetInt("char" + i));
            }
        }
    }

    void SaveGameLevel()
    {
        for (int i = 0; i < level.Length; i++)
        {
            PlayerPrefs.SetInt("level" + i, boolToInt(level[i].buyed));
        }
    }

    void LoadGameLevel()
    {
        for (int i = 0; i < level.Length; i++)
        {
            if (PlayerPrefs.HasKey("level" + i))
            {
                level[i].buyed = intToBool(PlayerPrefs.GetInt("level" + i));
            }
        }
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
