using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoSingleton<SceneManager>
{
    private AsyncOperation async;// SceneManager.LoadSceneAsync()這個方法的返回值類型是AsyncOperation
    private float currentProgress = 0;// 當前進度
    [SerializeField] private Image loadingFill;
    [SerializeField] private Button tutorial;
    [SerializeField] private SkillSO[] skills;

    private SkillUI skillUI;
    private StatsWindow statsWindow;
    private SkillWindow skillWindow;
    private MiniMap miniMap;
    private UIBarControl uiBarControl;
    private MobileMove mobileMove;
    private MobileAttack mobileAttack;
    private MobileRoll mobileRoll;
    private string currentScene;


    protected override void OnAwake()
    {
        skillUI = GetComponentInChildren<SkillUI>(true);
        statsWindow = GetComponentInChildren<StatsWindow>(true);
        skillWindow = GetComponentInChildren<SkillWindow>(true);
        miniMap = GetComponentInChildren<MiniMap>(true);
        uiBarControl = GetComponentInChildren<UIBarControl>(true);
        mobileMove = GetComponentInChildren<MobileMove>(true);
        mobileAttack = GetComponentInChildren<MobileAttack>(true);
        mobileRoll = GetComponentInChildren<MobileRoll>(true);

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;

        Debug.Log("SceneManagerOnAwake");
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public bool IsGameScene()
    {
        return currentScene == "GameScene";
    }


    //public為了給TutorialEvnetTrigger用
    public void LoadLevel(string sceneName)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogError("找不到該Scene: " + sceneName);
            return;
        }
        GameStateManager.Inst.ChangState(GameState.Loading);
        UIManager.Inst.OpenMenu("Loading");
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        //設定讀取完後不能自動跳場景
        async.allowSceneActivation = false;
        loadingFill.fillAmount = 0;
        LoadAsync();
    }

    private async void LoadAsync()
    {
        currentProgress = 0;

        while (true)
        {
            await Task.Yield();
            // 進度條需要到達的進度值  
            float targetProgress;
            // async.progress 你正在讀取的場景的進度值 0到0.9
            // 因為最大值只到0.9所以要手動++ 不然進度條會有縫隙很醜
            targetProgress = async.progress < 0.9f ? async.progress * 100 : 100;
            if (currentProgress < targetProgress)
            {
                currentProgress++;
            }
            loadingFill.fillAmount = currentProgress / 100f;

            if (currentProgress >= 100)
            {
                // 設定為true的時候，如果場景數據加載完畢，就可以自動跳轉場景  
                async.allowSceneActivation = true;
                async = null;
                return;
            }
        }
    }

    private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
    {
        //這裡會比Start快
        currentScene = scene.name;
        Debug.Log("進入Scene: " + currentScene);

        //不能在這邊開關 他會跑到Menu然後不帶任何GameScene的UI
        //GameMenuController.Inst.enabled = false;
        //GameMenuController.Inst.enabled = true;
        switch (currentScene)
        {
            case "MenuScene":
                GameStateManager.Inst.ChangState(GameState.MainMenu);
                AudioManager.Inst.PlayBGM("MenuSceneBGM");
                UIManager.Inst.OpenMenu("Welcome");
                break;
            case "GameScene":
                //這是為了避免過了開頭劇情後 按ESC存檔 後OpenGameMenu沒調回來
                Time.timeScale = 1f;
                GameStateManager.Inst.ChangState(GameState.Gaming);
                UIManager.Inst.CloseMenu("Loading");
                AudioManager.Inst.PlayBGM("GameSceneIntro");
                PlayerManager.Inst.SpawnPlayer();
                skillUI.Init(PlayerManager.Inst.SkillSelector, PlayerManager.Inst.SkillShooter);
                statsWindow.Init();
                skillWindow.Init();
                miniMap.Init(PlayerManager.Inst.Player.transform);
                uiBarControl.Init(PlayerManager.Inst.PlayerStamina, PlayerManager.Inst.Player);
                mobileMove.Init(PlayerManager.Inst.PlayerControl, PlayerManager.Inst.ShootDirection);
                mobileAttack.Init(PlayerManager.Inst.PlayerControl,
                    PlayerManager.Inst.SkillSelector,
                    PlayerManager.Inst.RemoteSkillPosition,
                    PlayerManager.Inst.SkillShooter);
                mobileRoll.Init(PlayerManager.Inst.PlayerControl);
                //測試用暫時關掉
                DialogueManager.Inst.ShowDialogue("IntroDialogue");
                //TODO 把是否看過dialogue存檔
                break;
            default:
                break;
        }
    }
}