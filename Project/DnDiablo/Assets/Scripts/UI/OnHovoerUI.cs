using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHovoerUI : MonoBehaviour {

    [Header("OnHoverObject")]
    [SerializeField] private GameObject onHoverObject;

    [Header("Text")]
    [SerializeField] private Text nameArea;
    [SerializeField] private Text descriptionArea;
    

    #region singleton
    public static OnHovoerUI Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public void DisplayOnHover(string name, string description)
    {
        nameArea.text = name;
        descriptionArea.text = description;
        onHoverObject.SetActive(true);
    }

    public void StopDisplayOnHover()
    {
        onHoverObject.SetActive(false);
    }
}
