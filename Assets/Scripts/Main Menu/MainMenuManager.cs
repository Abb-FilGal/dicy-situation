using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup OptionsPanel;
    public CanvasGroup InfoPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowOptions()
    {
        OptionsPanel.alpha = 1;
        OptionsPanel.blocksRaycasts = true;
        OptionsPanel.interactable = true;
    }

    public void ShowInfo()
    {
        InfoPanel.alpha = 1;
        InfoPanel.blocksRaycasts = true;
        InfoPanel.interactable = true;
    }

    public void Back()
    {
        OptionsPanel.alpha = 0;
        OptionsPanel.blocksRaycasts = false;
        OptionsPanel.interactable = false;

        InfoPanel.alpha = 0;
        InfoPanel.blocksRaycasts = false;
        InfoPanel.interactable = false;
    }

    public void Website()
    {
        Application.OpenURL("https://github.com/Abb-FilGal/dicy-situation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
