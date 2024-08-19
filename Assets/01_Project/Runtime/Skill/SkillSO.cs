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
    //TODOError 會因為不斷進出GameScene 試試看從OnSceneLoadRemoveAll 註冊多次 呼叫多次
    public UnityEvent<float, float> CoolDownChange = new UnityEvent<float, float>();
    public UnityEvent CoolDownStart = new UnityEvent();
    public UnityEvent CoolDownEnd = new UnityEvent();

    public bool CanShoot()
    {
        return skillCD <= 0;
    }

    public IEnumerator StartCoolDown()
    {
        Debug.Log("StartCoolDown");
        skillCD = skillRate;
        CoolDownStart.Invoke();
        while (true)
        {
            //不要將 yield return null 移到判斷pause底下 會無限迴圈
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            skillCD -= Time.deltaTime;
            CoolDownChange?.Invoke(skillCD, skillRate);
            if (skillCD < 0)
            {
                CoolDownEnd.Invoke();
                break;
            }
        }
    }
}