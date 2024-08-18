using UnityEngine;
using UnityEngine.UI;

public class MobileUI : MonoBehaviour
{
    [SerializeField] private Button escMenuBtn;
    [SerializeField] private Button statsMenuBtn;
    [SerializeField] private Button skillMenuBtn;

    private void Start()
    {
        gameObject.SetActive(GameStateManager.Inst.IsMobile);
        escMenuBtn.onClick.AddListener(UIManager.Inst.EscBtnOnClick);
        statsMenuBtn.onClick.AddListener(() => UIManager.Inst.OpenMenu("StatsWindow"));
        skillMenuBtn.onClick.AddListener(() => UIManager.Inst.OpenMenu("SkillWindow"));
    }
}