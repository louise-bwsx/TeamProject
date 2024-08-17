using TMPro;
using UnityEngine;

public class SkillWindow : MonoBehaviour
{
    [SerializeField] private SkillWindowItem[] skillWindowItems;
    [SerializeField] private Sprite[] skillIcons;
    [SerializeField] private string[] skillNames;
    [SerializeField] private string[] skillInfo;
    [SerializeField] private TMP_Text dustText;

    public void Init()
    {
        PlayerStats player = PlayerManager.Inst.Player;
        player.OnDustChange.AddListener(ChangeDust);
        for (int i = 0; i < skillWindowItems.Length; i++)
        {
            int index = i;
            int level = player.GetStatLevel(index);
            skillWindowItems[i].Init(skillIcons[i], skillNames[i], skillInfo[i], level, index);
        }
    }

    private void ChangeDust(int dust)
    {
        dustText.text = $"³Ñ¾lÅ]¹Ð: {dust}";
    }
}