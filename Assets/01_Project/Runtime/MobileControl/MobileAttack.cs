using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MobileAttack : MonoBehaviour
{
    [SerializeField] private Button swingBtn;
    [SerializeField] private Button spikeBtn;
    [SerializeField] private FixedJoystick[] joysticks;
    [SerializeField] private Image[] joysticksOutline;
    [SerializeField] private Image[] skillHandles;
    [SerializeField] private Image[] skillCDImages;
    [SerializeField] private Image selectCircle;
    [SerializeField] private SkillSO[] skillSO;

    public void Init(PlayerControl playerControl, SkillSelector skillSelector, RemoteSkillPosition remoteSkillPosition)
    {
        swingBtn.onClick.RemoveAllListeners();
        swingBtn.onClick.AddListener(() => playerControl.Attack(AttackType.NormalAttack));
        spikeBtn.onClick.RemoveAllListeners();
        spikeBtn.onClick.AddListener(() => playerControl.Attack(AttackType.SpikeAttack));

        //記得將SkillBtn的raycastPadding 全部改成80 不然會因為範圍過大按到其他按鈕
        for (int i = 0; i < joysticksOutline.Length; i++)
        {
            int index = i;
            EventTrigger trigger = joysticksOutline[i].gameObject.AddComponent<EventTrigger>();

            skillSO[i].CoolDownStart.AddListener(() => DisableHandle(index));
            skillSO[i].CoolDownEnd.AddListener(() => EnableHandle(index));
            skillSO[i].CoolDownChange.AddListener((cd, rate) => UICoolDown(index, cd, rate));

            // 設定 PointerDown 事件
            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener(data => { SkillBtnOnClick(index, skillSelector, remoteSkillPosition); });
            trigger.triggers.Add(pointerDownEntry);

            // 設定 PointerUp 事件
            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((data) => { SkillBtnOnRelease(index, skillSelector, remoteSkillPosition); });
            trigger.triggers.Add(pointerUpEntry);
        }
    }

    private void SkillBtnOnClick(int index, SkillSelector skillSelector, RemoteSkillPosition remoteSkillPosition)
    {
        if (!skillSelector.IsSkillCanShoot(index))
        {
            Debug.Log("冷卻中");
            return;
        }
        Debug.Log("SkillBtnOnClick");
        selectCircle.gameObject.SetActive(true);
        selectCircle.transform.SetParent(skillHandles[index].transform);
        selectCircle.rectTransform.anchoredPosition = Vector3.zero;
        joysticksOutline[index].color = Color.white;
        skillSelector.SkillPrepare(index);
        bool isDirectionalTypeSkill = index == 1 || index == 4
                                    ? true
                                    : false;

        remoteSkillPosition.SkillBtnOnClick(joysticks[index], skillHandles[index].rectTransform, joysticksOutline[index].rectTransform, isDirectionalTypeSkill);
    }

    private void SkillBtnOnRelease(int index, SkillSelector skillSelector, RemoteSkillPosition remoteSkillPosition)
    {
        Debug.Log("SkillBtnOnRelease");
        selectCircle.gameObject.SetActive(false);
        selectCircle.transform.SetParent(null);
        joysticksOutline[index].color = Color.clear;
        skillSelector.SkillShoot();
        remoteSkillPosition.SkillBtnOnRelease();
    }

    private void UICoolDown(int currentIndex, float skillCD, float skillRate)
    {
        //Debug.Log(currentIndex);
        skillCDImages[currentIndex].fillAmount = skillCD / skillRate;
    }

    private void EnableHandle(int index)
    {
        joysticksOutline[index].raycastTarget = true;
    }

    private void DisableHandle(int index)
    {
        joysticksOutline[index].raycastTarget = false;
    }
}