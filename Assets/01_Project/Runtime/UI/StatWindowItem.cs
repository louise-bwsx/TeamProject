using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button levelUpBtn;
    private int level;

    public void Initialize(string name, int level)
    {
        nameText.text = name;
        this.level = level;
        levelText.text = level.ToString();
        levelUpBtn.onClick.RemoveAllListeners();
        levelUpBtn.onClick.AddListener(LevelUP);
    }

    private void LevelUP()
    {
        Debug.Log(nameText.text + "等級提升");
        level++;
        levelText.text = level.ToString();
    }
}