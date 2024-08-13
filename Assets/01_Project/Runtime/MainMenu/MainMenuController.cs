using UnityEngine;
using UnityEngine.UI;

public enum MainMenuType
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

public class MainMenuController : MonoSingleton<MainMenuController>
{
    [SerializeField] private GameObject[] menus;
    [SerializeField] private Button startGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Button settings;
    [SerializeField] private Button tutorial;
    [SerializeField] private Button credit;
    [SerializeField] private Button quitGame;

    [SerializeField] private Button tutorialBackgrounBtn;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Sprite pcTutorial;
    [SerializeField] private Sprite mobileTutorial;
    private bool isStarting;

    private void Start()
    {
        startGame.onClick.AddListener(StartBtnOnClick);
        loadGame.onClick.AddListener(() => { OpenMenu(MainMenuType.Load); });
        settings.onClick.AddListener(() => { OpenMenu(MainMenuType.Settings); });
        tutorial.onClick.AddListener(() => { OpenMenu(MainMenuType.Tutorial); });
        tutorialBackgrounBtn.onClick.AddListener(TutorialBackgroundBtnOnClick);
        credit.onClick.AddListener(() => { OpenMenu(MainMenuType.Credit); });
        quitGame.onClick.AddListener(QuitGame);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuScene")
        {
            OpenMenu(MainMenuType.Welcome);
        }
        ChangeTutorialSprite();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscButton();
        }
    }

    private void EscButton()
    {
        if (menus[(int)MainMenuType.Menu].activeSelf)
        {
            return;
        }
        OpenMenu(MainMenuType.Menu);
    }

    public void OpenMenu(MainMenuType type)
    {
        if (!IsMenuExist(type))
        {
            return;
        }
        CloseAllMenu();
        menus[(int)type].SetActive(true);
    }

    public void CloseMenu(MainMenuType type)
    {
        if (!IsMenuExist(type))
        {
            return;
        }
        menus[(int)type].SetActive(false);
    }

    private void CloseAllMenu()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
    }

    private void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private bool IsMenuExist(MainMenuType type)
    {
        if (menus[(int)type] == null)
        {
            Debug.LogError(type.ToString() + "為空");
            return false;
        }
        return true;
    }

    private void StartBtnOnClick()
    {
        isStarting = true;
        OpenMenu(MainMenuType.Tutorial);
    }

    private void TutorialBackgroundBtnOnClick()
    {
        Debug.Log(isStarting);
        if (isStarting)
        {
            SceneManager.Inst.LoadLevel("GameScene");
            return;
        }
        OpenMenu(MainMenuType.Menu);
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
}