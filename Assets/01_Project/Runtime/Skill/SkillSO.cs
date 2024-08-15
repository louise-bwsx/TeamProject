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
    [HideInInspector] public float skillForce = 500f;
    public UnityEvent<float, float> CoolDownChange = new UnityEvent<float, float>();

    public bool CanShoot()
    {
        return skillCD < skillRate;
    }

    public IEnumerator StartCoolDown()
    {
        skillCD = skillRate;
        while (true)
        {
            if (GameStateManager.Inst.CurrentState == GameState.Pausing)
            {
                continue;
            }
            skillCD -= Time.deltaTime;
            CoolDownChange?.Invoke(skillCD, skillRate);
            yield return null;
            if (skillCD < 0)
            {
                break;
            }
        }
    }
}