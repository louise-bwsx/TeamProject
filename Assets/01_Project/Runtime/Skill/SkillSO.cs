using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string prefabName;
    public float staminaCost;
    public float destroyTime;
    public float skillCD;//冷卻時間
    public float skillRate;//射擊間隔
    public float skillForce = 15f;

    public bool CanShoot()
    {
        return skillCD <= 0;
    }
}