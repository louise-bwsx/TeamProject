using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    private Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button tutorialBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button[] closeBtns;
    [SerializeField, ReadOnly] private List<string> openMenuNames = new List<string>();
    private bool isInitialized;

    private void Start()
    {
        continueBtn.onClick.AddListener(Unpause);
        saveBtn.onClick.AddListener(() => { OpenMenu("Save"); });
        loadBtn.onClick.AddListener(() => { OpenMenu("Load"); });
        settingsBtn.onClick.AddListener(() => { OpenMenu("Settings"); });
        tutorialBtn.onClick.AddListener(() => { OpenMenu("Tutorial"); });
        quitBtn.onClick.AddListener(BackToMainMenu);
        foreach (var btn in closeBtns)
        {
            btn.onClick.AddListener(CloseBtnOnClick);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscButton();
        }
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

    private void EscButton()
    {
        if (openMenuNames.Count > 0)
        {
            CloseAllMenu();
            //在打開SavePanel狀態下按下ESC
            if (GameStateManager.Inst.CurrentState == GameState.Pausing)
            {
                OpenMenu("Menu");
            }
            return;
        }
        switch (GameStateManager.Inst.CurrentState)
        {
            case GameState.Gaming:
                OpenMenu("Menu");
                GameStateManager.Inst.ChangState(GameState.Pausing);
                Time.timeScale = 0f;
                return;
            case GameState.Pausing:
                Unpause();
                return;
        }
    }

    private void Unpause()
    {
        CloseMenu("Menu");
        GameStateManager.Inst.ChangState(GameState.Gaming);
        if (PlayerManager.Inst.Player.playerHealth > 0)
        {
            Time.timeScale = 1f;
        }
    }

    private void BackToMainMenu()
    {
        CloseAllMenu();
        SceneManager.Inst.LoadLevel("MenuScene");
        Time.timeScale = 1f;
    }

    public void OpenMenu(string menuName)
    {
        Initialize();
        if (!IsMenuExist(menuName))
        {
            return;
        }
        CloseAllMenu();
        openMenuNames.Add(menuName);
        menuDict[menuName].SetActive(true);
    }

    public void CloseMenu(string menuName)
    {
        if (!IsMenuExist(menuName))
        {
            return;
        }
        openMenuNames.Remove(menuName);
        menuDict[menuName].SetActive(false);
    }

    private void CloseAllMenu()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
        openMenuNames.Clear();
    }

    private bool IsMenuExist(string menuName)
    {
        if (!menuDict.ContainsKey(menuName))
        {
            Debug.LogError($"!menuDict.ContainsKey( {menuName} )");
            return false;
        }
        return true;
    }

    private void Initialize()
    {
        if (isInitialized)
        {
            return;
        }
        foreach (GameObject menu in menus)
        {
            menuDict.Add(menu.name, menu);
        }
        isInitialized = true;
    }

    private void CloseBtnOnClick()
    {
        CloseAllMenu();
        OpenMenu("Menu");
    }
}