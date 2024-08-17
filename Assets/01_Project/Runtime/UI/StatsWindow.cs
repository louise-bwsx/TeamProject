using TMPro;
using UnityEngine;

public class StatsWindow : MonoBehaviour
{
    [SerializeField] private StatWindowItem[] statWindowItems;
    [SerializeField] private string[] statNames;
    [SerializeField] private TMP_Text dustText;

    public void Init()
    {
        PlayerManager.Inst.Player.OnDustChange.AddListener(ChangeDust);
        for (int i = 0; i < statWindowItems.Length; i++)
        {
            int index = i;
            string level = PlayerManager.Inst.Player.GetStatLevel(index).ToString();
            statWindowItems[i].Init(statNames[i], level, index);
        }
    }

    private void ChangeDust(int dust)
    {
        dustText.text = $"³Ñ¾lÅ]¹Ð: {dust}";
    }
}