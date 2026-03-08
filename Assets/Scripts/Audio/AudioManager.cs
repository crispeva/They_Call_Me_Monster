using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Properties
    public static AudioManager Instance;
    const float MAX_VOLUME = 0.1f;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioMixer audioMixer;

    [Header("FX")]
    [SerializeField] AudioSource ChangeWave;
    [Header("Music")]
    [SerializeField] AudioSource MainGameMusic;
    [SerializeField] AudioSource MainShoopingMusic;

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //Events
        GameController.Instance.WaveManager.OnWaveState += (_) => PlayNextWave();
        GameController.Instance.WaveManager.OnWaveState += (_) => PlayMainGameMusic();
        GameController.Instance.WaveManager.OnWavesCompleted +=  PlayShoopingMusic;
    }

    #endregion

    #region Public Methods
    public void PlaySFX(AudioClip clip, float pitchVariation = 0f)
    {
        if (clip == null) return;

        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);

        sfxSource.PlayOneShot(clip);

        sfxSource.pitch = originalPitch;
    }
    #endregion

    #region GamePlay Methods
    void PlayMainGameMusic()
    {
        if (MainGameMusic == null) return;
        MainShoopingMusic.DOFade(0, 1f);
        MainGameMusic.DOFade(0.1f, 0.1f);
        MainGameMusic.Play();
    }
    void PlayShoopingMusic()
    {
        if (MainShoopingMusic == null) return;
        MainGameMusic.DOFade(0, 1f);
        MainShoopingMusic.DOFade(MAX_VOLUME, 1f);
        MainShoopingMusic.Play();
    }
    void PlayNextWave()
    {
        if (ChangeWave == null) return;
        sfxSource.PlayOneShot(ChangeWave.clip,MAX_VOLUME);
    }
    #endregion
}
