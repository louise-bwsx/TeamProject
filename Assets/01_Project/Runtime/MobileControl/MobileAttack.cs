using UnityEngine;
using UnityEngine.UI;


public class MobileAttack : MonoBehaviour
{
    [SerializeField] private Button swingBtn;
    [SerializeField] private Button spikeBtn;
    [SerializeField] private Button[] skillBtns;
    [SerializeField] private Image selectCircle;

    public void Init(PlayerControl playerControl)
    {
        swingBtn.onClick.RemoveAllListeners();
        swingBtn.onClick.AddListener(() => playerControl.Attack(AttackType.NormalAttack));
        spikeBtn.onClick.RemoveAllListeners();
        spikeBtn.onClick.AddListener(() => playerControl.Attack(AttackType.SpikeAttack));
        for (int i = 0; i < skillBtns.Length; i++)
        {
            int index = i;
            skillBtns[i].onClick.RemoveAllListeners();
            skillBtns[i].onClick.AddListener(() => MoveSelectCircle(index));
        }
    }

    private void MoveSelectCircle(int index)
    {
        selectCircle.gameObject.SetActive(true);
        selectCircle.transform.SetParent(skillBtns[index].transform);
        selectCircle.rectTransform.anchoredPosition = Vector3.zero;
    }
}