using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Properties
    public static AudioManager Instance;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioMixer audioMixer;
    #endregion

    #region Fields
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

    #region Private Methods
    #endregion

}
