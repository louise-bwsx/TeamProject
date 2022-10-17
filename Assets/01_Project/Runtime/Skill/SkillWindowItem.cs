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
    private int level;

    public void Initialize(Sprite iconSprite, string name, string info, int level)
    {
        icon.sprite = iconSprite;
        nameText.text = name;
        infoText.text = info;
        levelUpBtn.onClick.AddListener(LevelUP);
        this.level = level;
        for (int i = 0; i < level; i++)
        {
            levelImage[i].enabled = true;
        }
    }

    private void LevelUP()
    {
        level++;
        for (int i = 0; i < level; i++)
        {
            levelImage[i].enabled = true;
        }
    }
}
