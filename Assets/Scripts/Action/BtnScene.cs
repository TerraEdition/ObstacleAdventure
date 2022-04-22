using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnScene : MonoBehaviour
{
    public string sceneName;

    private GameManager gameManager;

    private AudioManager audioManager;

    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
        btn = GetComponent<Button>();
        btn.onClick.AddListener (Click);
    }

    // Update is called once per frame
    void Click()
    {
        if (sceneName != "")
        {
            audioManager.PlaySound("Click");
            gameManager.scene = sceneName;
            gameObject.transform.parent.gameObject.SetActive(false);
            gameManager.GetComponent<LoadingManager>().loadLevel();
        }
    }
}
