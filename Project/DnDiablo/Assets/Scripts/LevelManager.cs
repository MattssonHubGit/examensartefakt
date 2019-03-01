using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    

    [Header("Settings")]
    private int currentLevel = 0;
    private List<Scene> allLevelScenes = new List<Scene>();


    [Header("Debug")]
    [SerializeField] private string sceneName = "";
    [SerializeField] private KeyCode key = KeyCode.Keypad7;

    public static LevelManager Instance;

    private void Start()
    {
        #region Singleton
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            

            //Get all level scenes and put them in a list
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
            {
                Debug.Log(i);
                if (SceneManager.GetSceneByBuildIndex(i).name.Contains("Level_"))
                {
                    allLevelScenes.Add(SceneManager.GetSceneByBuildIndex(i));
                }
            }

        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    }

    void Update()
    {
        LoadDebugScene();
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        string _nextLevelName = "Level_" + currentLevel;

        if (currentLevel > allLevelScenes.Count)
        {
            currentLevel = 0;
            SceneManager.LoadScene("MainMenu");
            return;
        }

        SceneManager.LoadScene(_nextLevelName);

    }

    private void LoadDebugScene()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Application.Quit()");
        Application.Quit();
    }

}
