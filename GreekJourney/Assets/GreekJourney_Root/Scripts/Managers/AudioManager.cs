using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Declaración de Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    { 
        get 
        { 
            return instance; 
        } 
    }

    [Header("Audio Source References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip Arrays")]
    public AudioClip[] musicList;
    public AudioClip[] sfxList;

    private void Awake()
    {
        //Singleton que no se destruye entre escenas
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public void PlayMusic(int musicIndex)
    {
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        sfxSource.PlayOneShot(sfxList[sfxIndex]);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
