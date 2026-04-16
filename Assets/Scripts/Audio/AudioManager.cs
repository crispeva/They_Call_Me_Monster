using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Enemies;
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
    [SerializeField] AudioSource ClerigoHit;
    [SerializeField] AudioSource ItemBougth;
    [Header("Music")]
    [SerializeField] AudioSource MainGameMusic;
    [SerializeField] AudioSource MainShoopingMusic;
    [SerializeField] AudioSource BossMusic;
    [SerializeField] AudioSource EndDemoMusic;

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
        GameController.Instance.WaveManager.OnMainWave +=  PlayMainGameMusic;
        GameController.Instance.WaveManager.OnWavesCompleted +=  PlayShoopingMusic;
        GameController.Instance.WaveManager.OnBossWave += PlayBossMusic;
        GameController.Instance.WaveManager.OnVictory += PlayVictoryMusic;
        GameController.Instance.ShopManager.Onbougth += PlayBougthItem;
        EnemyController.OnAttackEnemy += PlayClerigoHit;
    }

    private void PlayVictoryMusic()
    {
        sfxSource.Stop();
        MainShoopingMusic.DOFade(0, 1f);
        BossMusic.DOFade(0, 1f);
        MainGameMusic.DOFade(0, 1f);
        EndDemoMusic.DOFade(MAX_VOLUME, 1f);
        EndDemoMusic.Play();
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
        // Detener cualquier música que esté sonando antes de iniciar la transición
        Debug.Log("PlayShoopingMusic llamado");
      MainGameMusic.DOFade(0, 1f);
       MainShoopingMusic.DOFade(MAX_VOLUME, 1f);
        MainShoopingMusic.Play();
    }
    void PlayNextWave()
    {
        if (ChangeWave == null) return;
        sfxSource.PlayOneShot(ChangeWave.clip,MAX_VOLUME);
    }
    void PlayBougthItem()
    {
        if (ItemBougth == null) return;
        sfxSource.PlayOneShot(ItemBougth.clip,MAX_VOLUME);
    }
    #endregion

    #region Enemies Methods
    void PlayBossMusic()
    {
        if (BossMusic == null) return;
        MainShoopingMusic.DOFade(0, 1f);
        Debug.Log("PlayBossMusic llamado");
        BossMusic.DOFade(MAX_VOLUME, 1f);
        BossMusic.Play();
    }
    void PlayClerigoHit()
    {
        if (ClerigoHit == null) return;
        Debug.Log("PlayClerigoHit llamado");
        sfxSource.PlayOneShot(ClerigoHit.clip, MAX_VOLUME);
    }
    #endregion
}
