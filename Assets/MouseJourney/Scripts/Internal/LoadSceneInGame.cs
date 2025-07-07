using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;


public class LoadSceneInGame : MonoBehaviour
{

    public static LoadSceneInGame instance;

    //The first game scene to play - not menu scene
    private int firstRealScene = 3;

    public void Start()
    {
        Awake();
    }
    public void Awake()
    {
        Debug.Log("LoadSceneInGame::Awake::");
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
            Debug.Log("LoadSceneInGame::Awake::instance initiated");
        }
        else if (instance != this)
        {
            // If instance already exists and it's not this, then destroy this to enforce the singleton.
            Destroy(gameObject);
            Debug.Log("LoadSceneInGame::Awake::instance destroyed");
        }

        // Set this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadSceneByName(String name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadRandomScene()
    {
        int index = Random.Range(firstRealScene, SceneManager.sceneCountInBuildSettings - 1);
        Debug.Log("LoadSceneInGame::LoadRandomScene::index = " + index);
        LoadSceneByIndex(index);
    }
}