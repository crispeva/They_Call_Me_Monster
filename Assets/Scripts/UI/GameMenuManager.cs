using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{

    #region Fields
    //[SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _mainmenuButton;
    [SerializeField] private Button _mainmenuDeathButton;

    [Header("Menu Pause")]
    [SerializeField] private Button _pauseRetrybutton;
    [SerializeField] private Button _pauseMainMenubutton;
    [SerializeField] private Button _pauseExitbutton;
    [SerializeField] private Button _pauseOptionbutton;
    public Action _onActiveSettingsMenu;
        [Header("Settings")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _fxSlider;
    [SerializeField] private Button _lowQuality;
    [SerializeField] private Button _mediumQuality;
    [SerializeField] private Button _highQuality;
    [SerializeField] private Button _closeButton;

    [SerializeField] private GameObject _settingsPanel;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        //Death Menu
        _mainmenuDeathButton.onClick.AddListener(ExitGame);
        //Pause Menu
        _pauseExitbutton.onClick.AddListener(ExitGame);
        _pauseRetrybutton.onClick.AddListener(RetryGame);
        _pauseOptionbutton.onClick.AddListener(Options);
        //Panel ENDDEMO
        _mainmenuButton.onClick.AddListener(ExitGame);
        //Settings events
        _musicSlider.onValueChanged.AddListener(MusicVolumeChange);
        _fxSlider.onValueChanged.AddListener(FXVolumeChange);
        _lowQuality.onClick.AddListener(() => SetQuality(1));
        _mediumQuality.onClick.AddListener(() => SetQuality(2));
        _highQuality.onClick.AddListener(() => SetQuality(3));
        _closeButton.onClick.AddListener(CloseSettings);

        GameController.Instance.InputController.OnActiveMenu += CloseSettings;
    }


    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);// Reinicia el juego cargando la escena actual
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Options()
    {
        _settingsPanel.SetActive(true);
    }

    #endregion
    #region Settings

    private void CloseSettings()
    {
        _settingsPanel.SetActive(false);
    }
    private void FXVolumeChange(float value)
    {
        _mixer.SetFloat("FXVolume", Mathf.Lerp(-80f, 0f, value));
    }

    private void MusicVolumeChange(float value)
    {
        _mixer.SetFloat("MusicVolume", Mathf.Lerp(-80f, 0f, value));

    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        Debug.Log("Nivel de calidad cambiado a: " + QualitySettings.names[index]);
    }
    #endregion

}
