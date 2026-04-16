using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{

    #region Fields
    //[SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _mainmenuButton;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _mainmenuButton.onClick.AddListener(MainMenu);

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
