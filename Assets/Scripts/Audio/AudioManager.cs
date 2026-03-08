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
    const float MAX_VOLUME = 0.2f;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioMixer audioMixer;

    [Header("FX")]
    [SerializeField] AudioClip ChangeWave;
    [Header("Music")]
    [SerializeField] AudioClip MainGamePlayMusic;

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
        GameController.Instance.WaveManager.OnWaveState +=  PlayNextWave;
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
    void PlayMainGamePlayMusic()
    {
        if (MainGamePlayMusic == null) return;
        sfxSource.clip = MainGamePlayMusic;
        sfxSource.loop = true;
        sfxSource.Play();
    }
    void PlayNextWave(int a)
    {
        if (ChangeWave == null) return;
        sfxSource.clip = ChangeWave;
        sfxSource.PlayOneShot(ChangeWave,MAX_VOLUME);
    }
    #endregion
}
