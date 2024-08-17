using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Welcome,
    Menu,
    Load,
    Save,
    Settings,
    Tutorial,
    Credit,
    Loading,
    TutorialForButton
}

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Button[] closeBtns;
    [SerializeField] private GameObject[] menus;
    private Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button startGame;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button mainMenuLoadBtn;
    [SerializeField] private Button gameMenuLoadBtn;
    [SerializeField] private Button mainMenuSettingsBtn;
    [SerializeField] private Button gameMenuSettingsBtn;
    [SerializeField] private Button mainMenuTutorialBtn;
    [SerializeField] private Button gameMenuTutorialBtn;
    [SerializeField] private Button credit;
    [SerializeField] private Button quitGame;
    [SerializeField] private Button backToMainMenuBtn;

    [SerializeField] private Button tutorialBackgrounBtn;
    [SerializeField] private Image loadImage;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Image settingsImage;
    [SerializeField] private Sprite pcTutorial;
    [SerializeField] private Sprite mobileTutorial;
    [SerializeField] private Sprite mainMenuSettingsSprite;
    private bool isStarting;
    private bool isInitialized;
    [SerializeField, ReadOnly] private List<string> openMenuNames = new List<string>();

    private void Start()
    {
        startGame.onClick.AddListener(StartBtnOnClick);
        continueBtn.onClick.AddListener(Unpause);
        saveBtn.onClick.AddListener(() => { OpenMenu("Save"); });
        gameMenuLoadBtn.onClick.AddListener(() => { OpenMenu("Load"); });
        mainMenuLoadBtn.onClick.AddListener(() => { OpenMenu("Load"); });
        mainMenuSettingsBtn.onClick.AddListener(() => { OpenMenu("Settings"); });
        gameMenuSettingsBtn.onClick.AddListener(() => { OpenMenu("Settings"); });
        mainMenuTutorialBtn.onClick.AddListener(() => { OpenMenu("Tutorial"); });
        gameMenuTutorialBtn.onClick.AddListener(() => { OpenMenu("Tutorial"); });
        tutorialBackgrounBtn.onClick.AddListener(TutorialBackgroundBtnOnClick);
        credit.onClick.AddListener(() => { OpenMenu("Credit"); });
        backToMainMenuBtn.onClick.AddListener(BackToMainMenu);
        quitGame.onClick.AddListener(QuitGame);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuScene")
        {
            OpenMenu("Welcome");
        }
        ChangeTutorialSprite();
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
        Debug.Log(GameStateManager.Inst.CurrentState);
        switch (GameStateManager.Inst.CurrentState)
        {
            case GameState.Gaming:
                if (openMenuNames.Count > 1)
                {
                    CloseAllMenu();
                    return;
                }
                OpenMenu("GameMenu");
                GameStateManager.Inst.ChangState(GameState.Pausing);
                Time.timeScale = 0f;
                return;
            case GameState.Pausing:
                if (IsUIOpen("GameMenu"))
                {
                    Unpause();
                    return;
                }
                CloseAllMenu();
                //在打開SavePanel狀態下按下ESC
                if (GameStateManager.Inst.CurrentState == GameState.Pausing)
                {
                    OpenMenu("GameMenu");
                }
                return;
        }
    }

    private void Unpause()
    {
        CloseMenu("GameMenu");
        GameStateManager.Inst.ChangState(GameState.Gaming);
        if (PlayerManager.Inst.Player.hp > 0)
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
        switch (menuName)
        {
            case "Settings":
                OpenSettingsMenu();
                break;
            case "Load":
                OpenLoadMenu();
                break;
            case "Save":
                SaveManager.Inst.RefreshUI();
                break;
        }
        menuDict[menuName].SetActive(true);
    }

    private void OpenSettingsMenu()
    {
        if (SceneManager.Inst.IsGameScene())
        {
            settingsImage.sprite = null;
            settingsImage.color = new Color(0, 0, 0, 120f / 255f);
            return;
        }
        settingsImage.sprite = mainMenuSettingsSprite;
        settingsImage.color = Color.white;
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

    private void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private bool IsMenuExist(string menuName)
    {
        Initialize();
        if (!menuDict.ContainsKey(menuName))
        {
            Debug.Log(menuDict.Count);
            Debug.LogError($"!menuDict.ContainsKey( {menuName} )");
            return false;
        }
        return true;
    }

    private void StartBtnOnClick()
    {
        isStarting = true;
        OpenMenu("Tutorial");
    }

    private void TutorialBackgroundBtnOnClick()
    {
        Debug.Log(isStarting);
        Debug.Log(GameStateManager.Inst.CurrentState);

        if (isStarting)
        {
            isStarting = false;
            SceneManager.Inst.LoadLevel("GameScene");
            return;
        }
        if (SceneManager.Inst.IsGameScene())
        {
            OpenMenu("GameMenu");
            return;
        }
        OpenMenu("MainMenu");
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

    private void ChangeTutorialSprite()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            tutorialImage.sprite = mobileTutorial;
        }
        else
        {
            tutorialImage.sprite = pcTutorial;
        }
    }

    private void OpenLoadMenu()
    {
        SaveManager.Inst.RefreshUI();
        if (SceneManager.Inst.IsGameScene())
        {
            loadImage.sprite = null;
            loadImage.color = new Color(0, 0, 0, 120f / 255f);
            return;
        }
        loadImage.sprite = mainMenuSettingsSprite;
        loadImage.color = Color.white;
    }

    private void CloseBtnOnClick()
    {
        CloseAllMenu();
        if (SceneManager.Inst.IsGameScene())
        {
            OpenMenu("GameMenu");
            return;
        }
        OpenMenu("MainMenu");
    }
}