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
        quitBtn.onClick.AddListener(BackToMainMenu);
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
        //當背包是開的 或是 教學介面是開著的
        if (IsMenuActive("Inventory") || IsMenuActive("TutorialImage") || IsMenuActive("SkillWindow"))
        {
            //把他們關起來
            CloseMenu("Inventory");
            CloseMenu("TutorialImage");
            CloseMenu("SkillWindow");
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
                CloseMenu("Menu");
                GameStateManager.Inst.ChangState(GameState.Gaming);
                Time.timeScale = 1f;
                return;
        }
    }

    private void GameContinue()
    {
        if (getHitEffect.playerHealth > 0)
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

    public bool IsMenuActive(string menuName)
    {
        Initialize();
        if (!IsMenuExist(menuName))
        {
            return false;
        }
        return menuDict[menuName].activeSelf;
    }

    private void CloseAllMenu()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
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
            //註解掉是因為測試SkillWindow時在editMode下手動開啟
            //進入遊戲後呼叫PlayerControl.Attack()時會確認IsMenuActive然後Initialize
            //不確定為什麼要關閉
            //menu.SetActive(false);
            menuDict.Add(menu.name, menu);
        }
        isInitialized = true;
    }
}