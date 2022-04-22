using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionCanvas;
    [SerializeField]
    private Slider sfxSound;

    [SerializeField]
    private Slider bgmSound;
    [SerializeField]
    private Button backBtn;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        sfxSound
            .onValueChanged
            .AddListener(delegate
            {
                OnSliderSFX();
            });
        bgmSound
            .onValueChanged
            .AddListener(delegate
            {
                OnSliderBGM();
            });
        loadOption();
        OnSliderBGM();
        OnSliderSFX();
        backBtn.onClick.AddListener(BackBtn);
    }

    void loadOption()
    {
        if (PlayerPrefs.HasKey("sfx"))
        {
            sfxSound.value = PlayerPrefs.GetFloat("sfx");
        }
        if (PlayerPrefs.HasKey("bgm"))
        {
            bgmSound.value = PlayerPrefs.GetFloat("bgm");
        }
    }

    void OnSliderSFX()
    {
        PlayerPrefs.SetFloat("sfx", sfxSound.value);
        audioManager.changeSound();
        audioManager.PlaySound("Click");
    }

    void OnSliderBGM()
    {
        PlayerPrefs.SetFloat("bgm", bgmSound.value);
        audioManager.changeSound();
    }
    public void BackBtn()
    {
        audioManager.PlaySound("Click");
        OptionCanvas.SetActive(false);
    }
}
