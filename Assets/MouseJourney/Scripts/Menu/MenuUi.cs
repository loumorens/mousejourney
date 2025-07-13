using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUi : MonoBehaviour
{
    public void StartNewGame()
    {
        if (LoadSceneInGame.instance != null)
        {
            LoadSceneInGame.instance.LoadSceneByName("HouseScene");

        }
        else
        {
            Debug.Log("MenuUi::StartNewGame::LoadSceneInGame doesn't initialize");
            SceneManager.LoadScene("HouseScene");
        }
    }


    public void Exit()
    {
        //if (EditorApplication.isPlaying)
        //{
       //     EditorApplication.ExitPlaymode();
       // }
       // else
        //{
            Application.Quit();
       // }
    }
}
