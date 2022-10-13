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

    private void Start()
    {
        startGame.onClick.AddListener(() => { OpenMenu(MainMenuType.Tutorial); });
        loadGame.onClick.AddListener(() => { OpenMenu(MainMenuType.Load); });
        settings.onClick.AddListener(() => { OpenMenu(MainMenuType.Settings); });
        tutorial.onClick.AddListener(() => { OpenMenu(MainMenuType.TutorialForButton); });
        credit.onClick.AddListener(() => { OpenMenu(MainMenuType.Credit); });
        quitGame.onClick.AddListener(QuitGame);

        OpenMenu(MainMenuType.Welcome);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscButton();
        }
    }

    public void EscButton()
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

    public void QuitGame()
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
}