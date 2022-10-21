using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp;
    [SerializeField] private GameObject[] treasure;

    public void GainExp(int gainExp)
    {
        currentExp += gainExp;
        while (currentExp > level * 100)
        {
            currentExp -= level * 100;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        Debug.Log("升級! 目前等級: " + level);
        UnlockSometing();
    }

    private GameObject UnlockSometing()
    {
        Debug.Log("取得道具: " + treasure[level]);
        return treasure[level];
    }
}