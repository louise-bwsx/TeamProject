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
    [SerializeField, ReadOnly] private float hp;
    [SerializeField] private float maxHp;
    [SerializeField, ReadOnly] private float invincibleTimer;
    [SerializeField] private float invincibleDuration = 1f;
    [SerializeField, ReadOnly] private int dust = 100;
    [SerializeField] private HealthBarOnGame healthbarongame;
    [SerializeField] private GameObject bossUltHit;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerControl playerControl;
    private Collider collider;
    private int statsLevelNeed = 100;
    private int skillLevelNeed = 100;
    [SerializeField, ReadOnly] private int[] statsLevel;
    [SerializeField, ReadOnly] private int[] skillLevels;

    [HideInInspector] public UnityEvent<float> OnHealthChange = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<int> OnDustChange = new UnityEvent<int>();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerControl = GetComponentInChildren<PlayerControl>();
        collider = GetComponent<Collider>();
        SaveManager.Inst.ISaves.Add(this);
        //為了讓StatsWindow讀到所以放Awake
        Load();
    }

    private void Start()
    {
        hp = maxHp;
        healthbarongame.SetMaxHealth(maxHp);//人物身上的血條
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
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    public int GetStatLevel(StatType type)
    {
        return statsLevel[(int)type];
    }

    public int GetStatLevel(int statIndex)
    {
        return statsLevel[statIndex];
    }

    public int GetSkillLevel(SkillType type)
    {
        return skillLevels[(int)type];
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
        if (IsDead())
        {
            return;
        }
        if (playerControl.isInvincible)
        {
            return;
        }
        if (other.gameObject.CompareTag("BossUlt") && GetStatLevel(StatType.DEF) - 20 < 0)
        {
            //絕對值(人物的防禦值-20)<0
            hp -= Mathf.Abs(GetStatLevel(StatType.DEF) - 20);
            playerControl.isInvincible = true;
            invincibleTimer = invincibleDuration;
            //生出被大招打中的特效
            GameObject bossUltHitFX = Instantiate(bossUltHit, transform.position, transform.rotation);
            Destroy(bossUltHitFX, 1f);
            //將血量輸入到頭頂的UI
            healthbarongame.SetHealth(hp);
            //將血量輸入到畫面上的UI
            OnHealthChange?.Invoke(hp / maxHp);
            spriteRenderer.color = Color.red;
        }
        else if (other.gameObject.CompareTag("MonsterAttack") && GetStatLevel(StatType.DEF) - 10 < 0)
        {
            Debug.Log("GetHit");
            //絕對值(人物的防禦值-10)<0
            hp -= Mathf.Abs(GetStatLevel(StatType.DEF) - 10);
            playerControl.isInvincible = true;
            invincibleTimer = invincibleDuration;
            //將血量輸入到頭頂的UI
            healthbarongame.SetHealth(hp);
            //將血量輸入到畫面上的UI
            OnHealthChange?.Invoke(hp / maxHp);
            spriteRenderer.color = Color.red;
            Debug.Log($"IsInvincible: {playerControl.isInvincible}");
        }

        // 暫時先這樣
        if (hp <= 0)
        {
            playerControl.isAttack = false;
            animator.SetTrigger("Dead");
            collider.enabled = false;
            AudioManager.Inst.PlayBGM("Dead");
            GameStateManager.Inst.ChangState(GameState.PlayerDead);
            return;
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
        gameSave.dust = dust;
        gameSave.skillLevels = skillLevels;
        gameSave.statsLevel = statsLevel;
        gameSave.playerHp = hp;
        gameSave.playerMaxHp = maxHp;
        gameSave.SaveTransform(transform);
    }

    public void Load()
    {
        Debug.Log("Load");
        GameSaveData gameSave = SaveManager.Inst.GetGameSave();
        transform.name = transform.name.Split('(')[0];
        dust = gameSave.dust;
        skillLevels = gameSave.skillLevels;
        statsLevel = gameSave.statsLevel;
        hp = gameSave.playerHp;
        maxHp = gameSave.playerMaxHp;
        //位置在playerManager那邊設定
    }
}