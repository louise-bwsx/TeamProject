using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button levelUpBtn;
    [SerializeField] private Image[] levelImage;
    private int skillIndex;

    public void Init(Sprite iconSprite, string name, string info, int level, int index)
    {
        icon.sprite = iconSprite;
        nameText.text = name;
        infoText.text = info;
        skillIndex = index;
        for (int i = 0; i < level; i++)
        {
            levelImage[i].enabled = true;
        }
        levelUpBtn.onClick.AddListener(LevelUP);
    }

    private void LevelUP()
    {
        PlayerManager.Inst.Player.SkillLevelUp(skillIndex);
        int level = PlayerManager.Inst.Player.GetSkillLevel(skillIndex);
        for (int i = 0; i < level; i++)
        {
            levelImage[i].enabled = true;
        }
    }
}