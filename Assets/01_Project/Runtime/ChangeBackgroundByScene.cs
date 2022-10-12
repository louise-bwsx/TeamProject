using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundByScene : MonoBehaviour
{
    [SerializeField] private Sprite menuSceneBackground;
    [SerializeField] private Sprite gameSceneBackground;
    [SerializeField] private Color gameSceneColor;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (SceneManager.Inst.currentScene == "GameScene")
        {
            image.sprite = gameSceneBackground;
            image.color = Color.white;
        }
        else if (SceneManager.Inst.currentScene == "MenuScnen")
        {
            image.sprite = menuSceneBackground;
            image.color = gameSceneColor;
        }
    }
}