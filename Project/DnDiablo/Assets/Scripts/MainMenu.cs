using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;


    private void Start()
    {
        playButton.onClick.AddListener(delegate { LevelManager.Instance.LoadNextLevel(); });
        exitButton.onClick.AddListener(delegate { LevelManager.Instance.ExitGame(); });

    }

    void Update()
    {
    }
}
