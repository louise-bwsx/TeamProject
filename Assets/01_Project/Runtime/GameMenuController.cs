using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    private Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();

    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button tutorialBtn;
    [SerializeField, ReadOnly] private List<string> openMenuNames = new List<string>();

    private void Start()
    {
        //settingsBtn.onClick.AddListener(() => { OpenMenu("Settings"); });
        //tutorialBtn.onClick.AddListener(() => { OpenMenu("Tutorial"); });


    }

    public bool IsUIOpen(string uiName)
    {
        if (!menuDict.ContainsKey(uiName))
        {
            Debug.Log($"!menuDict.ContainsKey(uiName): {uiName}");
            return false;
        }
        return menuDict[uiName].activeSelf;
    }









}