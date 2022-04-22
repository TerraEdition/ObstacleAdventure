using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public int selectedCharacter;

    [HideInInspector]
    public int selectedLevel;

    [HideInInspector]
    public string actionName;

    [HideInInspector]
    public int jewelTempo = 0;

    [HideInInspector]
    public int coinTempo = 0;

    [HideInInspector]
    public int jewel = 1000;

    [HideInInspector]
    public int heart = 6;

    [HideInInspector]
    public int coin = 500000;

    [HideInInspector]
    public int key = 0;

    [HideInInspector]
    public bool updateJewel = true;

    [HideInInspector]
    public bool updateHeart = true;

    [HideInInspector]
    public bool updateCoin = true;

    [HideInInspector]
    public bool updateKey = true;

    [HideInInspector]
    public bool updateCharacter = false;

    [HideInInspector]
    public bool updateLevel = false;

    [HideInInspector]
    public bool playerDie = false;

    [HideInInspector]
    public string scene;

    [HideInInspector]
    public string historyScene;

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
        if (PlayerPrefs.HasKey("coin"))
        {
            SetContinue();
        }
        // ResetGame();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("heart", 6);
        PlayerPrefs.DeleteKey("level");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("jewel", jewel);
        PlayerPrefs.SetInt("heart", heart);
        PlayerPrefs.SetString("level", scene);
        PlayerPrefs.Save();
    }

    public void SetContinue()
    {
        coin = PlayerPrefs.GetInt("coin");
        jewel = PlayerPrefs.GetInt("jewel");
        heart = PlayerPrefs.GetInt("heart");
    }

    public void GetContinue()
    {
        scene = PlayerPrefs.GetString("level");
        GetComponent<LoadingManager>().loadLevel();
    }

    void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
