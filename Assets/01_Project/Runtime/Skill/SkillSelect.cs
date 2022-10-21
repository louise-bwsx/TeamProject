using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    [SerializeField] private Image[] skillFrames;
    [SerializeField] private Sprite[] skillFrameSprites;
    [field: SerializeField] public int CurrentIndex { get; private set; }
    private int previousIndex = 1;
    private PlayerControl playerControl;
    private GetHitEffect getHitEffect;
    [SerializeField] private Animator animator;
    [field: SerializeField] public Skill[] SkillList { get; private set; }
    /// <summary>
    /// 給其他Script知道當前發動的是什麼技能
    /// </summary>
    public Skill selectSkill { get; private set; }

    private void Awake()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();
        playerControl = FindObjectOfType<PlayerControl>();
        SkillList = GetComponentsInChildren<Skill>();
    }

    private void Start()
    {
        ChangeSelectSkillFrame(0);
    }

    private void Update()
    {
        if (playerControl.isAttack || getHitEffect.playerHealth < 0)
        {
            return;
        }

        ScrollSelect();
        KeyboardTriggerSkill();
        if (Input.GetMouseButtonDown(2))
        {
            MiddleMouseButtonTriggerSkill();
        }
    }

    public void SkillShoot()
    {
        SkillList[CurrentIndex].Shoot();
    }

    private void MiddleMouseButtonTriggerSkill()
    {
        if (SkillList[CurrentIndex].CanShoot())
        {
            //正式版一定要這樣才能讀的到
            //playerControl.isAttack = true;
            selectSkill = SkillList[CurrentIndex];
            animator.SetTrigger("Magic");
        }
    }

    private void KeyboardTriggerSkill()
    {
        int inputKey = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inputKey = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inputKey = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inputKey = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inputKey = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inputKey = 4;
        }
        if (inputKey == -1)
        {
            return;
        }
        CurrentIndex = inputKey;
        ChangeSelectSkillFrame(CurrentIndex);
        if (SkillList[inputKey].CanShoot())
        {
            selectSkill = SkillList[inputKey];
            animator.SetTrigger("Magic");
        }
    }

    private void ScrollSelect()
    {
        float scrollSelect = Input.GetAxis("Mouse ScrollWheel");
        if (scrollSelect != 0)
        {
            if (scrollSelect > 0.0f)
            {
                CurrentIndex--;
            }
            else if (scrollSelect < 0.0f)
            {
                CurrentIndex++;
            }
            if (CurrentIndex < 0)
            {
                CurrentIndex = 0;
            }
            if (CurrentIndex > skillFrames.Length - 1)
            {
                CurrentIndex = skillFrames.Length - 1;
            }
            if (previousIndex != CurrentIndex)
            {
                ChangeSelectSkillFrame(CurrentIndex);
            }
        }
        return;
    }

    private void ChangeSelectSkillFrame(int selectIndex)
    {
        skillFrames[selectIndex].sprite = skillFrameSprites[0];
        skillFrames[previousIndex].sprite = skillFrameSprites[1];
        previousIndex = selectIndex;
    }
}