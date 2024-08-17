using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button levelUpBtn;
    private int statIndex;

    public void Init(string name, string level, int index)
    {
        nameText.text = name;
        levelText.text = level;
        statIndex = index;
        levelUpBtn.onClick.AddListener(LevelUpBtnOnClick);
    }

    private void LevelUpBtnOnClick()
    {
        PlayerManager.Inst.Player.StatLevelUp(statIndex);
        levelText.text = PlayerManager.Inst.Player.GetStatLevel(statIndex).ToString();
    }
}