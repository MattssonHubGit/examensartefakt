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

    [Header("Level up state")]
    [HideInInspector] public bool currentlyLeveling = false;
    private bool doneTalent = false;
    private bool doneStats = false;

    [SerializeField] private Button continueButton;
    [Space]
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject waveUI;

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


        continueButton.onClick.AddListener(delegate { ContinueLevelUpProcess(); });
    }

    void Update()
    {
        LoadDebugScene();

        if (currentlyLeveling)
        {
            //Player can't move
            Player.Instance.canMove = false;
            Player.Instance.canTakeDamage = false;
            Player.Instance.canMove = false;
        }
    }

    public void StartGame()
    {
        TextWriter.Instance.AddLineToDocument("*******New Session*******");
        SceneManager.LoadScene("Level_1");
        //StartLevelUpProcess();

    }

    private void LoadDebugScene()
    {
        if (Input.GetKeyDown(key))
        {
            //SceneManager.LoadScene(sceneName);
            StartGame();
        }
    }

    public void ExitGame()
    {
        Debug.Log("Application.Quit()");
        currentLevel = -1;
        Application.Quit();
    }

    public void StartLevelUpProcess()
    {
        TextWriter.Instance.AddLineToDocument("- New Level -");
        for (int i = 0; i < Player.Instance.mySkills.Count; i++)
        {
            TextWriter.Instance.AddLineToDocument(Player.Instance.mySkills[i].skillName + " " + Player.Instance.mySkills[i].level);
        }
        TextWriter.Instance.AddLineToDocument("Health: " + Player.Instance.myStats.healthDisplay.ToString());
        TextWriter.Instance.AddLineToDocument("Power: " + Player.Instance.myStats.powerDisplay.ToString());
        TextWriter.Instance.AddLineToDocument("Resource: " + Player.Instance.myStats.resourceDisplay.ToString());
        TextWriter.Instance.AddLineToDocument("Cooldown Reduction: " + Player.Instance.myStats.cooldownRedDisplay.ToString());
        TextWriter.Instance.AddLineToDocument("Movement Speed: " + Player.Instance.myStats.moveSpeedDisplay.ToString());
        TextWriter.Instance.AddLineToDocument("- Choices -");

        //Currently leveling, but have not done either talents or stats
        currentlyLeveling = true;
        doneTalent = false;
        doneStats = false;

        //Deactive other UI
        playerUI.SetActive(false);
        waveUI.SetActive(false);

        //Player can't move
        Player.Instance.RespawnFromLevelComplete();

        Player.Instance.canMove = false;
        Player.Instance.canTakeDamage = false;
        Player.Instance.canMove = false;

        continueButton.transform.parent.gameObject.SetActive(true);

        TalentManager.Instance.ToggleTalentSun();
    }

    private void ContinueLevelUpProcess()
    {
        if (doneTalent == false)
        {
            doneTalent = true;
            TalentManager.Instance.ToggleTalentSun();
            StatsManager.Instance.ToggleStatsWindow();

            TextWriter.Instance.AddLineToDocument("-");
        }
        else if (doneStats == false)
        {
            doneStats = true;
            StatsManager.Instance.ToggleStatsWindow();            
        }
        else
        {
            FinishLevelUpProcess();
        }
    }

    private void FinishLevelUpProcess()
    {
        //No longer leveling, reset it all
        currentlyLeveling = false;
        doneTalent = false;
        doneStats = false;


        continueButton.transform.parent.gameObject.SetActive(false);

        //Activate other ui
        playerUI.SetActive(true);
        waveUI.SetActive(true);

        //Player can move
        Player.Instance.canMove = true;
        Player.Instance.canTakeDamage = true;
        Player.Instance.canMove = true;

    }

    public void LoadVictoryLevel()
    {
        //Change this to another scene (want)
        SceneManager.LoadScene("MainMenu");
    }
}
