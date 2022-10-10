using UnityEngine;

public enum MenuType
{
    Welcome,
    Menu,
    Load,
    Settings,
    Tutorial,
    Credit,
    Loading
}

public class MenuManager : MonoSingleton<MenuManager>
{
    [SerializeField] private GameObject[] menus;
    //private GameObject welcomeImage;

    private void Start()
    {
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