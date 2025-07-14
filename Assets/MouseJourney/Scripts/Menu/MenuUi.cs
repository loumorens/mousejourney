using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MenuUi : MonoBehaviour
{
    public void StartNewGame()
    {
        if (LoadSceneInGame.instance != null)
        {
            if (LoadSceneInGame.instance.develMode)
            {
                StartNewGameScene(LoadSceneInGame.instance.develSceneToTest);
            }
            else
            {
                StartNewGameScene("HouseScene");
            }
        }
        else
        {
            Debug.Log("MenuUi::StartNewGame::LoadSceneInGame doesn't initialize");
            SceneManager.LoadScene("HouseScene");
        }
    }

    private void StartNewGameScene(String name)
    { LoadSceneInGame.instance.LoadSceneByName(name);
    }


    public void Exit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Debug.Log("MenuUi::RestartLevel::scene to load = "+SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Debug.Log("MenuUi::NextLevel::Choose randomly next level");
        LoadSceneInGame.instance.LoadRandomScene();
    }

    public void GoToMainMenu()
    {
        Debug.Log("MenuUi::GoToMainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToOptionMenu()
    {
        Debug.Log("MenuUi::GoToOptionMenu");
        SceneManager.LoadScene("OptionMenu");
    }

    public void GoToCredits()
    {
        Debug.Log("MenuUi::GoToCredits");
        SceneManager.LoadScene("Credits");
    }
}
