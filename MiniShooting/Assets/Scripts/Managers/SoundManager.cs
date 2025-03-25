using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("오디오 소스")]
    [SerializeField]
    private AudioSource BGMSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("오디오 클립")]
    public AudioClip bgmclip;
    public AudioClip attackClip;
    public AudioClip hitClip;
    public AudioClip damageClip;
    public AudioClip dieClip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playBGM()
    {
        if(bgmclip != null)
        {
            BGMSource.clip = bgmclip;
            BGMSource.loop = true;
            BGMSource.Play();
        }
    }

    private void Start()
    {
        if (!BGMSource.isPlaying)
            playBGM();
    }

    public void PlayerAttack()
    {
        SFXSource.PlayOneShot(attackClip);
    }

    public void PlayerDamage()
    {
        SFXSource.PlayOneShot(damageClip);
    }

    public void PlayerHit()
    {
        SFXSource.PlayOneShot(hitClip);
    }

    public void PlayerDie()
    {
        SFXSource.PlayOneShot(dieClip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "TitleScene":
                bgmclip = Resources.Load<AudioClip>("Audio/BGM_Title");
                break;
            case "MainScene":
                bgmclip = Resources.Load<AudioClip>("Audio/BGM_Main");
                break;
            case "BossScene":
                bgmclip = Resources.Load<AudioClip>("Audio/BGM_Boss");
                break;
            default:
                bgmclip = null;
                break;
        }

        playBGM();
    }

}
