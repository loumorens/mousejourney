using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class LoadSceneInGame : MonoBehaviour
{

    public static LoadSceneInGame instance;


    public bool develMode;

    public String develSceneToTest;

    //The first game scene to play - not menu scene
    private int firstRealScene = 4;

    public List<String> sceneCanBeLoaded;

    public void Start()
    {
        Awake();
        getSceneLoadable();
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
        AudioManager.Instance.PlayMusicFromScene(name);

    }

    public void LoadRandomScene()
    {
        String name = getRandomName();
        Debug.Log("LoadSceneInGame::LoadRandomScene::name = " + name);
        if (name.Equals(SceneManager.GetActiveScene().name))
        {
            Debug.Log("LoadSceneInGame::LoadRandomScene::name = SceneManager.GetActiveScene().name" + SceneManager.GetActiveScene().name);
            LoadRandomScene();
        }
        else
        {
            LoadSceneByName(name);
        }
    }

    private int getRandomIndex()
    {
        return Random.Range(firstRealScene, SceneManager.sceneCountInBuildSettings);
    }
    private String getRandomName()
    {
        String name;
        int index = Random.Range(0, sceneCanBeLoaded.Count);
        name = sceneCanBeLoaded.ElementAt<String>(index);
        Debug.Log("LoadSceneInGame::getRandomName::name = " + name);
        return name;
    }


    private void getSceneLoadable()
    {
        int numberScenes = SceneManager.sceneCountInBuildSettings;
        Debug.Log("LoadSceneInGame::getSceneLoadable::sceneCountInBuildSettings = " + numberScenes);
        for (int i = 0; i < numberScenes; i++)
        {
            String scene = SceneUtility.GetScenePathByBuildIndex(i);
            scene = Path.GetFileNameWithoutExtension(scene);
            if (scene.Contains("Scene"))
            {
                sceneCanBeLoaded.Add(scene);
                Debug.Log("LoadSceneInGame::getSceneLoadable::scene name added= " + scene);
            }
        }
    }
}