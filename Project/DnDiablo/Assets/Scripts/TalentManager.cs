using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentManager : MonoBehaviour
{
    public List<Skill> allSkills = new List<Skill>();
    public Dictionary<Skill, int> skillData = new Dictionary<Skill, int>();

    public static TalentManager Instance;
    public Player player;

    /*[HideInInspector] */public int spendableTalentPoints = 5;

    [SerializeField] private Text amountText;

    [Header("Stem generation")]
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private Skill skillToAffect;
    [SerializeField] private GameObject talentSunParent;
    [SerializeField] private Vector3 offSets = new Vector3(0,0,0);
    [Range(0, 10)] [SerializeField] private int amountOfNodes;
    [Space]
    [SerializeField] private Sprite icon;
    [SerializeField] private bool changeButtonColor = false;
    [SerializeField] private Color buttonColor = Color.white;
    [Space]
    [SerializeField] private List<int> specialLevels = new List<int>();
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Sprite specialFrame;


    [Header("UI")]
    [SerializeField] public Transform skillButtonParent;
    [SerializeField] public GameObject skillButtonPrefab;
    

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            //Set up array and don't destroy on load
            SetUpSkillsInDictionary();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleTalentSun();
        }
    }

    private void SetUpSkillsInDictionary()
    {
        for (int i = 0; i < allSkills.Count; i++)
        {
            skillData.Add(allSkills[i], 0);
        }
    }

    [ContextMenu("Generate talent stem based on settings")]
    private void GenerateStem()
    {
        GameObject _branchParent = new GameObject(skillToAffect.name + " Branch");
        _branchParent.transform.parent = talentSunParent.transform;
        _branchParent.transform.localPosition = Vector3.zero;

        List<TalentNode> _generatedNodes = new List<TalentNode>();

        for (int i = 0; i < amountOfNodes; i++)
        {
            GameObject _objNode = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, _branchParent.transform);
            _objNode.transform.localPosition = new Vector3(0 + (offSets.x * i), 0 + (offSets.y * i), 0 + (offSets.z * i));
            TalentNode _scrNode = _objNode.GetComponent<TalentNode>();

            //Set up node data
            _scrNode.SkillToLevel = skillToAffect;
            _scrNode.LevelToSet = i;
            _scrNode.myButton = _objNode.GetComponent<Button>();
            if(icon != null) _scrNode.icon.sprite = icon;
            _scrNode.frame.sprite = defaultFrame;
            _scrNode.myButton.interactable = false;
            _scrNode.icon.color = new Color(1, 1, 1, 0.3f);
            
            //Special frame?
            for (int j = 0; j < specialLevels.Count; j++)
            {
                if (i == specialLevels[j])
                {
                    _scrNode.frame.sprite = specialFrame;
                }
            }


            //Color
            if (changeButtonColor)
            {
                _scrNode.myButton.image.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 1f);
            }

            //First Node
            if (i == 0)
            {
                _scrNode.IsFirst = true;
                _scrNode.myButton.interactable = true;
                _scrNode.icon.color = new Color(1, 1, 1, 1);

            }

            _generatedNodes.Add(_scrNode);
        }

        //Set up neighbours
        for (int i = 0; i < _generatedNodes.Count; i++)
        {
            //Previous node for all but the first
            if (i != 0)
            {
                _generatedNodes[i].PreviousNode = _generatedNodes[i - 1];
            }

            //Next node for all but last
            if (i != _generatedNodes.Count - 1)
            {
                _generatedNodes[i].NextNode = _generatedNodes[i + 1];
            }
        }
    }

    public void ToggleTalentSun()
    {
        UpdateTalentUI();
        talentSunParent.SetActive(!talentSunParent.active);
        //Disable other UI elements
        OnHovoerUI.Instance.StopDisplayOnHover();

    }

    public void UpdateTalentUI()
    {

        amountText.text = spendableTalentPoints.ToString();
    }

}
