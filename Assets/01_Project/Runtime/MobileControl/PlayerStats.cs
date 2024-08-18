using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public enum StatType
{
    STR,
    DEF,
    AGI,
    INT,
    SPR,
    Count
}

public class PlayerStats : MonoBehaviour, ISave
{
    public float hp;
    public float maxHp;
    public float invincibleLimit;
    public int dust = 100;
    public HealthBarOnGame healthbarongame;
    public GameObject[] getHitEffect;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float invincibleTimer;

    private PlayerControl playerControl;
    private Collider collider;
    private MobileRoll mobileRoll;
    private int statsLevelNeed = 100;
    private int skillLevelNeed = 100;
    [SerializeField, ReadOnly] private int[] statsLevel;
    [SerializeField, ReadOnly] private int[] skillLevels;

    public UnityEvent<float> OnHealthChange = new UnityEvent<float>();
    public UnityEvent<int> OnDustChange = new UnityEvent<int>();

    //TODOError: 存讀檔的資料 順序 還沒做好
    private void Awake()
    {
        playerControl = GetComponentInChildren<PlayerControl>();
        collider = GetComponent<Collider>();
        mobileRoll = GetComponentInChildren<MobileRoll>();
        Load();
    }

    private void Start()
    {
        hp = maxHp;
        OnHealthChange?.Invoke(1);
        OnDustChange?.Invoke(dust);
    }

    private void Update()
    {
        if (invincibleTimer > 0)
        {
            playerControl.isInvincible = true;
            invincibleTimer -= Time.deltaTime;
        }
        else if (invincibleTimer < 0 && playerControl.isInvincible)
        {
            invincibleTimer = 0;
            playerControl.isInvincible = false;
            spriteRenderer.color = Color.white;
        }
        if (hp <= 0 && collider.enabled)
        {
            playerControl.isAttack = false;
            animator.SetTrigger("Dead");
            collider.enabled = false;
        }
    }

    public int GetStatLevel(StatType type)
    {
        return statsLevel[(int)type];
    }

    public int GetStatLevel(int statIndex)
    {
        return statsLevel[statIndex];
    }

    public int GetSkillLevel(int skillIndex)
    {
        return skillLevels[skillIndex];
    }

    public void StatLevelUp(int statsIndex)
    {
        if (dust < statsLevelNeed)
        {
            Debug.LogError("魔塵不足");
            return;
        }
        dust -= statsLevelNeed;
        statsLevel[statsIndex]++;
        OnDustChange?.Invoke(dust);
    }

    public void SkillLevelUp(int skillIndex)
    {
        if (dust < skillLevelNeed * skillLevels[skillIndex])
        {
            Debug.LogError("魔塵不足");
            return;
        }
        if (skillLevels[skillIndex] >= 4)
        {
            Debug.Log("等級已達最大值");
            return;
        }
        dust -= skillLevelNeed;
        skillLevels[skillIndex]++;
        OnDustChange?.Invoke(dust);
    }

    private void OnTriggerEnter(Collider other)
    {
        //放在Stay會重複傷害因為大招不會因為玩家碰到而消失
        if (hp > 0 && !playerControl.isInvincible)
        {
            if (other.gameObject.CompareTag("BossUlt") && GetStatLevel(StatType.DEF) - 20 < 0)
            {
                getHitEffect[0] = getHitEffect[1];
                //絕對值(人物的防禦值-20)<0
                hp -= Mathf.Abs(GetStatLevel(StatType.DEF) - 20);
                //玩家設定為無敵狀態
                playerControl.isInvincible = true;
                //怪打到玩家時把無敵時間輸入進去
                invincibleTimer = invincibleLimit;
                //生出被大招打中的特效
                GameObject bossUltHitFX = Instantiate(getHitEffect[1], transform.position, transform.rotation);
                Destroy(bossUltHitFX, 1f);
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(hp);
                //將血量輸入到畫面上的UI
                OnHealthChange?.Invoke(hp / maxHp);
                //玩家貼圖變紅
                spriteRenderer.color = Color.red;
            }
            else if (other.gameObject.CompareTag("MonsterAttack") && GetStatLevel(StatType.DEF) - 10 < 0)
            {
                Debug.Log("danger");
                //絕對值(人物的防禦值-10)<0
                hp -= Mathf.Abs(GetStatLevel(StatType.DEF) - 10);
                //玩家設定為無敵狀態
                playerControl.isInvincible = true;
                //怪打到玩家時把無敵時間輸入進去
                invincibleTimer = invincibleLimit;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(hp);
                //將血量輸入到畫面上的UI
                OnHealthChange?.Invoke(hp / maxHp);
                //玩家貼圖變紅
                spriteRenderer.color = Color.red;
                Debug.Log(playerControl.isInvincible);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //碰到牆不能穿牆
        if (other.CompareTag("Wall"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp <= 0)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Gold":
            case "Green":
            case "White":
            case "Blue":
                dust += 5;
                hp += 5;
                Destroy(collision.gameObject);
                OnHealthChange?.Invoke(hp / maxHp);
                healthbarongame.SetHealth(hp);
                break;
        }
    }

    public void Save(GameSaveData gameSave)
    {
    }

    public void Load()
    {
        GameSaveData gameSave = SaveManager.Inst.GetGameSave();
        dust = gameSave.dust;
        skillLevels = gameSave.skillLevels;
        statsLevel = gameSave.charaterStats;
        hp = gameSave.playerHp;
        maxHp = gameSave.playerMaxHp;
        if (!gameSave.transformSaves.ContainsKey(transform.name))
        {
            TransformSave transformSave = new TransformSave(transform);
            gameSave.transformSaves.Add(transform.name, transformSave);
            return;
        }
        transform.position = gameSave.transformSaves[transform.name].position;
        transform.rotation = gameSave.transformSaves[transform.name].rotation;
    }
}