using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup OptionsPanel;

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

    public void Back()
    {
        OptionsPanel.alpha = 0;
        OptionsPanel.blocksRaycasts = false;
        OptionsPanel.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
