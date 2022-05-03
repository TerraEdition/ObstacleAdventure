using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0.5f, 1.5f)]
    public float pitch = 0.1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop;

    public bool isSfx = false;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        source.volume =
            volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch =
            pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void ChangeVolume()
    {
        source.volume =
            volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go =
                new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    void Start()
    {
        StopAllSound();
        changeSound();
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager : Sound not found in List : " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning("AudioManager : Sound not found in List : " + _name);
    }

    public void StopAllSound()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Stop();
        }
    }

    public void StopAllSoundBGM()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (!sounds[i].isSfx)
            {
                sounds[i].Stop();
            }
        }
    }

    public void changeSound()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isSfx)
            {
                sounds[i].volume = PlayerPrefs.GetFloat("sfx");
            }
            else
            {
                sounds[i].volume = PlayerPrefs.GetFloat("bgm");
            }
            sounds[i].ChangeVolume();
        }
    }
}
