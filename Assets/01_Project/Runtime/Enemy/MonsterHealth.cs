using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    [field: SerializeField, ReadOnly] public float Hp { get; protected set; }
    [field: SerializeField] public float MaxHp { get; protected set; }
    [SerializeField, ReadOnly] protected float invincibleTimer;
    [SerializeField] protected int knockbackForce;

    [SerializeField] protected string[] hitEffectsName;
    protected string hitEffectName;
    public EnemyController EnemyController { get; protected set; }
    private Rigidbody rigidbody;
    private Collider collider;
    protected PlayerStats playerStats;
    protected HealthBarOnGame healthBarOnGame;
    private NavMeshAgent agent;
    protected Animator animator;
    protected int poisonHit = 0;
    private float poisonTimer;
    private const float POISON_DURATION = 0.75f;
    private Coroutine poisonCoroutine;
    protected const float VELOCITY_RESET_TIME = .1f;
    protected const int MAX_POISON_HIT = 20;
    private const int MIN_ITEM_SPAWN = 1;
    private const int MAX_ITEM_SPAWN = 3;
    protected const float INVINCIBLE_DURATION = 0.5f;
    [SerializeField] protected GameObject healthBar;
    [SerializeField] private ItemSTO itemRate;
    [SerializeField] private Transform faceDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        healthBarOnGame = GetComponentInChildren<HealthBarOnGame>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        EnemyController = GetComponentInChildren<EnemyController>();
    }

    protected virtual void Start()
    {
        playerStats = PlayerManager.Inst.Player;
        Hp = MaxHp;
        healthBarOnGame.SetMaxHealth(MaxHp);
    }

    protected virtual void GetHit(float Damage, SkillType type)
    {
        if (Hp <= 0)
        {
            return;
        }


        //不要再這邊設定invincibleTimer 為了讓風 可以打快一點
        if (invincibleTimer > 0)
        {
            //Debug.Log($"{transform.name} 無敵中");
            return;
        }
        //Debug.Log($"Damage: {Damage}");

        SetHitEffect(type);

        //被攻擊後歸零下一次怪物攻擊時間
        if (EnemyController != null)
        {
            EnemyController.attackCD = 0;
        }

        //播放受擊動畫
        animator.SetTrigger("GetHit");
        //為了讓怪物後退關掉
        agent.enabled = false;
        //朝面對的反方向後退
        rigidbody.velocity = -faceDirection.forward * knockbackForce;
        ObjectPool.Inst.SpawnFromPool(hitEffectName, (transform.position + Vector3.up) * 0.8f, transform.rotation, duration: 1f);
        Hp -= Damage;
        healthBarOnGame.SetHealth(Hp);
        StartCoroutine(InvincibleCoroutine());

        if (Hp <= 0)
        {
            MonsterDead();
        }
    }

    protected void SetHitEffect(SkillType type)
    {
        switch (type)
        {
            case SkillType.Poison:
                hitEffectName = hitEffectsName[3];
                //太吵了 取消
                //AudioManager.Inst.PlaySFX("PoisonHit");
                break;
            case SkillType.Water:
                hitEffectName = hitEffectsName[4];
                AudioManager.Inst.PlaySFX("WaterHit");
                break;
            case SkillType.Wind:
                hitEffectName = hitEffectsName[2];
                AudioManager.Inst.PlaySFX("WindHit");
                break;
            case SkillType.Fire:
                hitEffectName = hitEffectsName[1];
                AudioManager.Inst.PlaySFX("FireHit");
                break;
            case SkillType.FireTornado:
                hitEffectName = hitEffectsName[1];
                AudioManager.Inst.PlaySFX("FireTornadoHit");
                break;
            case SkillType.Bomb:
                hitEffectName = hitEffectsName[1];
                AudioManager.Inst.PlaySFX("BombHit");
                break;
            case SkillType.Null:
                hitEffectName = hitEffectsName[0];
                break;
        }
    }

    protected IEnumerator InvincibleCoroutine()
    {
        //Debug.Log("InvincibleCoroutine");
        while (true)
        {
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            invincibleTimer -= Time.deltaTime;
            //Debug.Log(invincibleTimer);
            if (invincibleTimer <= 0)
            {
                //停止被打飛
                rigidbody.velocity = Vector3.zero;
                if (agent)
                {
                    agent.enabled = true;
                }
                yield break;
            }
        }
    }

    private IEnumerator PoisonCoroutine()
    {
        while (poisonHit > 0)
        {
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            poisonTimer -= Time.deltaTime;
            if (poisonTimer <= 0)
            {
                GetHit(1 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetSkillLevel(SkillType.Poison) * 20, SkillType.Poison);
                poisonTimer = POISON_DURATION;
                poisonHit--;
                if (poisonHit <= 0)
                {
                    poisonCoroutine = null;
                    break;
                }
            }
        }
    }

    protected virtual void MonsterDead()
    {
        //避免怪物死亡後還繼續追著玩家
        EnemyController.enabled = false;
        agent.enabled = false;
        collider.enabled = false;
        animator.SetBool("Dead", true);
        healthBar.SetActive(false);

        int rewardItems = Random.Range(MIN_ITEM_SPAWN, MAX_ITEM_SPAWN);//隨機裝備產生值
        for (int i = 0; i < rewardItems; i++)
        {
            //Instantiate(gold, transform.position, transform.rotation);
            Vector3 randomItemPosition = transform.position;
            randomItemPosition += new Vector3(Random.Range(-1, 1), 0.2f, Random.Range(-1, 1));//在死亡地點周圍隨機分布
            float RateCnt_ = 0;//物品產生的最小值
            float ItemRandom_ = Random.Range(0, 100) / 100f;//隨機裝備機率值
            for (int j = 0; j < itemRate.ItemObjList.Length; j++)
            {
                RateCnt_ += itemRate.ItemObjRateList[j];
                if (ItemRandom_ < RateCnt_)
                {
                    Instantiate(itemRate.ItemObjList[j], randomItemPosition, Quaternion.identity);
                    break;
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //打出的傷害數值 失敗
        //text = Instantiate(text, new Vector3(x, 0.7f, z), transform.rotation);
        //為了讓MonsterDead只執行一次
        if (other.CompareTag("Sword"))
        {
            GetHit(15 + playerStats.GetStatLevel(StatType.STR), SkillType.Null);
        }
        if (other.CompareTag("WaterAttack"))
        {
            GetHit(10 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetSkillLevel(SkillType.Water) * 10, SkillType.Water);
        }

        if (other.CompareTag("FireAttack"))
        {
            GetHit(20 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetSkillLevel(SkillType.Fire) * 10, SkillType.Fire);
        }

        if (other.CompareTag("Bomb"))
        {
            GetHit(60 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetStatLevel(StatType.SPR) * 2, SkillType.Bomb);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Poison"))
        {
            poisonHit = MAX_POISON_HIT;
            if (poisonCoroutine == null)
            {
                poisonCoroutine = StartCoroutine(PoisonCoroutine());
                return;
            }
        }
        if (other.CompareTag("WindAttack"))
        {
            //如果沒有這層 會因為不斷設定0.25反而延長無敵時間
            if (invincibleTimer <= 0)
            {
                GetHit(2 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetSkillLevel(SkillType.Wind) * 20, SkillType.Wind);
                invincibleTimer = 0.25f;
            }
        }
        if (other.CompareTag("Firetornado"))
        {
            //如果沒有這層 會因為不斷設定0.25反而延長無敵時間
            if (invincibleTimer <= 0)
            {
                GetHit(5 + playerStats.GetStatLevel(StatType.INT) + playerStats.GetStatLevel(StatType.SPR) * 2 + playerStats.GetSkillLevel(SkillType.Wind) * 20, SkillType.FireTornado);
                //怪物被受擊的間隔時間歸零
                invincibleTimer = 0.25f;
            }
        }
    }
}