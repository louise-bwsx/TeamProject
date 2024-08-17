using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    public int SkillLevelneed = 100;
    PlayerStats mobileStats;
    public int[] charaterStats;
    public Text[] charaterNumber;

    void Start()
    {
        mobileStats = FindObjectOfType<PlayerStats>();
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            //charaterStats[i] = GameSaveData.GetInst().charaterStats[i];
        }
        //for (int i = 0; i < charaterStats.Length; i++)
        //{
        //    charaterNumber[i].text = charaterStats[i].ToString();
        //}
    }

    public void StatsCheck()
    {
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            //charaterStats[i] = GameSaveData.GetInst().charaterStats[i];
        }
        for (int i = 0; i < charaterStats.Length; i++)
        {
            charaterNumber[i].text = charaterStats[i].ToString();
        }
    }
}