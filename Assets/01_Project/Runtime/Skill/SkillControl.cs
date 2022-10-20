using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField] private Image[] skillFrames;
    [SerializeField] private Sprite[] skillFrameSprites;
    [field: SerializeField] public int currentIndex { get; private set; }
    private int previousIndex = 1;
    private PlayerControl playerControl;
    private GetHitEffect getHitEffect;
    [SerializeField] private Animator animator;
    [field: SerializeField] public Skill[] skillList { get; private set; }
    /// <summary>
    /// 給其他Script知道當前發動的是什麼技能
    /// </summary>
    public Skill selectSkill { get; private set; }

    private void Awake()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();
        playerControl = FindObjectOfType<PlayerControl>();
        skillList = GetComponentsInChildren<Skill>();
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
        skillList[currentIndex].Shoot();
    }

    private void MiddleMouseButtonTriggerSkill()
    {
        if (skillList[currentIndex].CanShoot())
        {
            //正式版一定要這樣才能讀的到
            //playerControl.isAttack = true;
            selectSkill = skillList[currentIndex];
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
        currentIndex = inputKey;
        ChangeSelectSkillFrame(currentIndex);
        if (skillList[inputKey].CanShoot())
        {
            selectSkill = skillList[inputKey];
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
                currentIndex--;
            }
            else if (scrollSelect < 0.0f)
            {
                currentIndex++;
            }
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }
            if (currentIndex > skillFrames.Length - 1)
            {
                currentIndex = skillFrames.Length - 1;
            }
            if (previousIndex != currentIndex)
            {
                ChangeSelectSkillFrame(currentIndex);
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