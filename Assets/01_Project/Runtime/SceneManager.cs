using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoSingleton<SceneManager>
{
    private AsyncOperation async;// SceneManager.LoadSceneAsync()這個方法的返回值類型是AsyncOperation
    private float currentProgress = 0;// 當前進度
    [SerializeField] private Image loadingFill;
    [SerializeField] private Button tutorial;
    [SerializeField] private Button loadGame1;
    [SerializeField] private Button loadGame2;
    [SerializeField] private Button loadGame3;

    [SerializeField] private GameMenuController gameMenu;
    [SerializeField] private SkillUI skillUI;
    [SerializeField] private MiniMap miniMap;
    [SerializeField] private UIBarControl uiBarControl;

    public string currentScene { get; private set; }

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
        //tutorial.onClick.AddListener(() => { LoadLevel("GameScene"); });
        loadGame1.onClick.AddListener(() => { LoadLevel("GameScene"); });
        loadGame2.onClick.AddListener(() => { LoadLevel("GameScene"); });
        loadGame3.onClick.AddListener(() => { LoadLevel("GameScene"); });
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public bool IsGameScene() => currentScene == "GameScene";

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
        CentralData.GetInst();
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        //設定讀取完後不能自動跳場景
        async.allowSceneActivation = false;
        loadingFill.fillAmount = 0;
    }

    private void Update()
    {
        if (async == null)
        {
            currentProgress = 0;
            return;
        }
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
                GameStateManager.Inst.ChangState(GameState.Gaming);
                UIManager.Inst.CloseMenu("Loading");
                AudioManager.Inst.PlayBGM("GameSceneIntro");
                PlayerManager.Inst.SpawnPlayer();
                skillUI.Init(PlayerManager.Inst.SkillSelector);
                miniMap.Init(PlayerManager.Inst.Player.transform);
                uiBarControl.Init(PlayerManager.Inst.PlayerStamina);
                //TODO: 暫時這樣用 在GameMenuController.Awake太慢
                UIManager.Inst.OpenMenu("IntroDialog");
                break;
            default:
                break;
        }
    }
}