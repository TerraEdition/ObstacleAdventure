using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject

            loadingScreen,
            scrollBar;

    public Text progressText;

    public Sprite[] loadingSprites;

    public Image backgroundImage;

    private GameManager gameManager;

    private AudioManager audioManager;

    void Start()
    {
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
    }

    public void loadLevel()
    {
        audioManager.StopAllSoundBGM();
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AdsManager.instance.HideBanner();
        loadingScreen.SetActive(true);
        Time.timeScale = 1;
        AsyncOperation operation =
            SceneManager.LoadSceneAsync(gameManager.scene);
        backgroundImage.sprite =
            loadingSprites[Random.Range(0, loadingSprites.Length)];
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = progress * 100f + "%";
            scrollBar.GetComponent<Scrollbar>().size = progress;
            yield return false;
        }
        gameManager.scene = "";
        audioManager.PlaySound("Menu");
        loadingScreen.SetActive(false);
    }
}
