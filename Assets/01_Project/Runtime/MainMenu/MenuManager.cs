using UnityEngine;
using UnityEngine.UI;

public enum MenuType
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

public class MenuManager : MonoSingleton<MenuManager>
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
        startGame.onClick.AddListener(() => { OpenMenu(MenuType.Tutorial); });
        loadGame.onClick.AddListener(() => { OpenMenu(MenuType.Load); });
        settings.onClick.AddListener(() => { OpenMenu(MenuType.Settings); });
        tutorial.onClick.AddListener(() => { OpenMenu(MenuType.TutorialForButton); });
        credit.onClick.AddListener(() => { OpenMenu(MenuType.Credit); });
        quitGame.onClick.AddListener(QuitGame);

        OpenMenu(MenuType.Welcome);
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
        if (menus[(int)MenuType.Menu].activeSelf)
        {
            return;
        }
        OpenMenu(MenuType.Menu);
    }

    public void OpenMenu(MenuType type)
    {
        CloseAllMenu();
        menus[(int)type].SetActive(true);
    }

    public void CloseMenu(MenuType type)
    {
        menus[(int)type].SetActive(false);
    }

    private void CloseAllMenu()
    {
        foreach (var item in menus)
        {
            item.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}