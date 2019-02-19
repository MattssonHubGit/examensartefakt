using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //Class currently only used for debugging

    [SerializeField] private string sceneName = "";

    [SerializeField] private KeyCode key = KeyCode.Keypad7;

    public static LevelManager Instance;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneName);
        }
    }


}
