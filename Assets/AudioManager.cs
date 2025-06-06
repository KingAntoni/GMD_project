using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;

    public AudioClip backgroundMusic;
    public float BgMusicVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            if (backgroundMusic != null)
            {
                audioSource.clip = backgroundMusic;
                audioSource.volume = BgMusicVolume;
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
