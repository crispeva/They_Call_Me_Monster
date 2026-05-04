using System.Collections;
using System.Collections.Generic;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    #region Fields
    [SerializeField] Button _startGameButton;
    [SerializeField] Button _exitGameButton;
    [SerializeField] Button _gameplayGameButton;
    [SerializeField] Button _closeGameplayGameButton;
    [SerializeField] CanvasGroup _panelInfo;

    #endregion

    #region Unity Callbacks
    void Start()
    {
       
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
        _gameplayGameButton.onClick.AddListener(() => StartCoroutine(FadeIn(_panelInfo, 2f)));
        _closeGameplayGameButton.onClick.AddListener(() => StartCoroutine(FadeOut(_panelInfo, 2f)));
    }



    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    #region Animations
    public IEnumerator FadeIn(CanvasGroup group, float duration)
    {
        float t = 0f;
        group.interactable = true;
        group.blocksRaycasts = true;

        while (t < duration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        group.alpha = 1f;
    }
    public IEnumerator FadeOut(CanvasGroup group, float duration)
    {
        float t = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;

        while (t < duration)
        {
            group.alpha = Mathf.Lerp(1f, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        group.alpha = 0f;
    }
    #endregion
}


