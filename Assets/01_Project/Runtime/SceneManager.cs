using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager> 
{
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoad;
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