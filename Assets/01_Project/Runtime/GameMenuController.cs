using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    private Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button quitBtn;
    private bool isInitialized;
    public GetHitEffect getHitEffect;

    private void Start()
    {
        continueBtn.onClick.AddListener(GameContinue);
        quitBtn.onClick.AddListener(QuitGame);
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
        if (IsMenuActive("IntroDialog"))
        {
            return;
        }
        //當背包是開的 或是 教學介面是開著的
        if (IsMenuActive("Inventory") || IsMenuActive("TutorialImage"))
        {
            //把他們關起來
            CloseMenu("Inventory");
            CloseMenu("TutorialImage");
            return;
        }
        if (!IsMenuActive("Menu"))
        {
            OpenMenu("Menu");
            Time.timeScale = 0f;
        }
        else
        {
            CloseMenu("Menu");
            if (getHitEffect.playerHealth > 0)
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void GameContinue()
    {
        if (getHitEffect.playerHealth > 0)
        {
            Time.timeScale = 1f;
        }
    }

    private void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
        menuDict[menuName].SetActive(true);
    }

    public void CloseMenu(string menuName)
    {
        if (!IsMenuExist(menuName))
        {
            return;
        }
        menuDict[menuName].SetActive(false);
    }

    private void CloseAllMenu()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public bool IsMenuActive(string menuName)
    {
        if (!IsMenuExist(menuName))
        {
            return false;
        }
        return menuDict[menuName].activeSelf;
    }

    private bool IsMenuExist(string menuName)
    {
        if (!menuDict.ContainsKey(menuName))
        {
            Debug.LogError(menuName + "為空");
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
            menu.SetActive(false);
            menuDict.Add(menu.name, menu);
        }
        isInitialized = true;
    }
}