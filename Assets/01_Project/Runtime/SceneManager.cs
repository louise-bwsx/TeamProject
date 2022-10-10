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

    private void Awake()
    {
        tutorial.onClick.AddListener(LoadLevel);
        loadGame1.onClick.AddListener(LoadLevel);
        loadGame2.onClick.AddListener(LoadLevel);
        loadGame3.onClick.AddListener(LoadLevel);
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
    }

    //public為了給TutorialEvnetTrigger用
    public void LoadLevel()
    {
        MenuManager.Inst.OpenMenu(MenuType.Loading);
        CentralData.GetInst();
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        //設定讀取完後不能自動跳場景
        async.allowSceneActivation = false;
        loadingFill.fillAmount = 0;
    }

    private void Update()
    {
        if (async == null)
        {
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
            currentProgress = 0;
        }
    }

    private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
    {
        Debug.Log(scene.name);
        switch (scene.name)
        {
            case "GameScene":
                AudioManager.Inst.PlayBGM("GameSceneIntro");
                break;
            case "MenuScene":
                AudioManager.Inst.PlayBGM("MenuSceneBGM");
                break;
            default:
                break;
        }
    }
}