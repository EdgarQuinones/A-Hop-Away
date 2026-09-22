using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.loop = true;
        musicSource.Play();
    }


    public void PlaySound(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }

}
