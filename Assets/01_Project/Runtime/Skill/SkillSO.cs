using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObject/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string prefabName;
    public float staminaCost;
    public float destroyTime;
    public float skillCD;//冷卻時間
    public float skillRate;//射擊間隔
    public float skillForce = 15f;
    public UnityEvent<float, float> CoolDownChange = new UnityEvent<float, float>();

    public bool CanShoot()
    {
        return skillCD <= 0;
    }

    public IEnumerator StartCoolDown()
    {
        skillCD = skillRate;
        while (true)
        {
            //不要將 yield return null 移到判斷pause底下 會無限迴圈
            yield return null;
            if (GameStateManager.Inst.CurrentState == GameState.Pausing)
            {
                continue;
            }
            skillCD -= Time.deltaTime;
            CoolDownChange?.Invoke(skillCD, skillRate);
            if (skillCD < 0)
            {
                break;
            }
        }
    }
}