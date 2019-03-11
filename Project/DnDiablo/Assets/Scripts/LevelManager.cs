using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    

    [Header("Settings")]
    private int currentLevel = -1; //Negative 1 is before the game starts.
    private List<string> allLevelScenes = new List<string>();
    [SerializeField] private int amountOfLevels = 2;

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

            //Set up list with levels
            allLevelScenes.Clear();
            int indexer = 1;
            for (int i = 0; i < amountOfLevels; i++)
            {
                allLevelScenes.Add("Level_" + indexer);
                indexer++;
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
       
        //Is there not another level to load?
        if (currentLevel >= allLevelScenes.Count)
        {
            currentLevel = -1;
            SceneManager.LoadScene("MainMenu");
            return;
        }

        string _nextLevelName = allLevelScenes[currentLevel];
        SceneManager.LoadScene(_nextLevelName);

    }

    private void LoadDebugScene()
    {
        if (Input.GetKeyDown(key))
        {
            //SceneManager.LoadScene(sceneName);
            LoadNextLevel();
        }
    }

    public void ExitGame()
    {
        Debug.Log("Application.Quit()");
        currentLevel = -1;
        Application.Quit();
    }

}
