using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameMenuManager : MonoBehaviour
{

    #region Fields
    //[SerializeField] private Button _exitGameButton;
    [SerializeField] private UnityEngine.UI.Button _mainmenuButton;
    [SerializeField] private UnityEngine.UI.Button _mainmenuDeathButton;

    [Header("Menu Pause")]
    [SerializeField] private UnityEngine.UI.Button _pauseRetrybutton;
    [SerializeField] private UnityEngine.UI.Button _pauseMainMenubutton;
    [SerializeField] private UnityEngine.UI.Button _pauseExitbutton;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _mainmenuButton.onClick.AddListener(MainMenu);
        _mainmenuDeathButton.onClick.AddListener(MainMenu);

        _pauseMainMenubutton.onClick.AddListener(MainMenu);
        _pauseExitbutton.onClick.AddListener(ExitGame);
        _pauseRetrybutton.onClick.AddListener(RetryGame);

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
        Application.Quit();
    }
    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

}
