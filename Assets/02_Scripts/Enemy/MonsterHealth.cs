using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float Hp = 0;
    public float maxHp = 50;
    public int numHeldItemMin = 1;//裝備生成最小數
    public int numHeldItemMax = 3;//裝備生成最大數
    public float beAttackTime;
    public float attackTime;
    public int bounceForce;

    public HealthBarOnGame healthBarOnGame;
    public GameObject healthBar;
    public ItemSTO itemRate;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject[] getHitEffect;
    public CharacterBase characterBase;
    public SkillBase skillBase;
    public EnemyController enemyController;
    public new Rigidbody rigidbody;
    public Transform faceDirection;

    public AudioSource audioSource;//音效在子類別調整音量大小
    public AudioClip swordHitSFX;//突擊受擊音效
    public AudioClip poisonHitSFX;//毒受擊音效
    public AudioClip fireHitSFX;//火受擊音效
    public AudioClip windHitSFX;//風受擊音效
    public AudioClip waterHitSFX;//水受擊音效
    public AudioClip tornadoHitSFX;//龍捲風受擊音效
    public AudioClip bombHitSFX;//爆炸受擊音效

    public Transform hitByTransform;
    public float beAttackMin = 0;//被打的次數
    public float beAttackMax = 0;//被打的最大次數
    public float getHitTime;
    public float gethitlimit = 0.3F;//間格秒數
    public float windColdTime = 5;
    public EnumAttack enumAttack;
    public new Collider collider;

    public virtual void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        skillBase = FindObjectOfType<SkillBase>();
        
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        if (audioSource == null)
        { 
            audioSource = GetComponent<AudioSource>();
        }
        healthBarOnGame = GetComponentInChildren<HealthBarOnGame>();
        beAttackMax = windColdTime / gethitlimit;

        Hp = maxHp;
        healthBarOnGame.SetMaxHealth(maxHp);
    }

    public virtual void Update()
    {
        beAttackTime += Time.deltaTime;
        //風持續傷害
        getHitTime += Time.deltaTime;

        if (beAttackMin >= 1)
        {
            if (getHitTime > gethitlimit)
            {
                getHitTime = 0;
                GetHit(2);
                beAttackMin--;
                if (enumAttack != EnumAttack.count)
                {
                    switch (enumAttack)
                    {
                        case EnumAttack.wind:
                            {
                                audioSource.PlayOneShot(windHitSFX);
                                break;
                            }
                        case EnumAttack.poison:
                            {
                                audioSource.PlayOneShot(poisonHitSFX);
                                break;
                            }
                        case EnumAttack.fireTornado:
                            {
                                audioSource.PlayOneShot(tornadoHitSFX);
                                break;
                            }
                    }
                }
            }
        }

        if (hitByTransform == null)
        {
            enumAttack = EnumAttack.count;
            beAttackMin = 0;
        }
    }
    public virtual void GetHit(float Damage)
    {
        if (beAttackTime > attackTime)
        {
            if (enemyController != null)
            { 
                enemyController.attackCD = 0;
            }
            animator.SetTrigger("GetHit");
            if (faceDirection != null)
            { 
                rigidbody.velocity = -faceDirection.forward * bounceForce;
            }
            Debug.Log(transform.name);
            GameObject FX = Instantiate(getHitEffect[0], transform.position + Vector3.up * 0.8f, transform.rotation);
            Destroy(FX, 1);
            Hp -= Damage;
            healthBarOnGame.SetHealth(Hp);
            if (Hp <= 0)
            {
                MonsterDead();
                //避免一直跳進來
                beAttackMin = 0;
            }
            beAttackTime = 0;
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        //打出的傷害數值 失敗
        //text = Instantiate(text, new Vector3(x, 0.7f, z), transform.rotation);
        //為了讓MonsterDead只執行一次
        if (other.CompareTag("Sword"))
        {
            getHitEffect[0] = getHitEffect[1];
            //audioSource.PlayOneShot(SwordHitSFX);
            if (characterBase != null)
            { 
                GetHit(15 + characterBase.STR);
            }
        }
        if (other.CompareTag("Skill"))
        {
            getHitEffect[0] = getHitEffect[5];
            GetHit(0);
        }
        if (other.CompareTag("WaterAttack"))
        {
            getHitEffect[0] = getHitEffect[6];
            audioSource.PlayOneShot(waterHitSFX);
            GetHit(5 + characterBase.INT + skillBase.waterSkill);
        }
        if (other.CompareTag("FireAttack"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(fireHitSFX);
            GetHit(10 + characterBase.INT + skillBase.fireSkill);
            Debug.Log(10 + " " + characterBase.INT + " " + skillBase.fireSkill);
        }
        if (other.CompareTag("WindAttack") /*&& hitByTransform != other.transform*/)
        {
            getHitEffect[0] = getHitEffect[3];
            beAttackMin = beAttackMax;//最大被打的次數
            //hitByTransform = other.transform;
            GetHit(2 + characterBase.INT + skillBase.windSkill);
            enumAttack = EnumAttack.wind;
        }
        if (other.CompareTag("Poison") && hitByTransform != other.transform)
        {
            getHitEffect[0] = getHitEffect[4];
            audioSource.PlayOneShot(poisonHitSFX);
            beAttackMin = 20;//最大被打的次數
            hitByTransform = other.transform;
            GetHit(1 + characterBase.INT + skillBase.poisonSkill);
            Debug.Log(characterBase.INT + " " + skillBase.poisonSkill);
        }
        if (other.CompareTag("Firetornado") && hitByTransform != other.transform)
        {
            getHitEffect[0] = getHitEffect[2];
            beAttackMin = beAttackMax;//最大被打的次數
            hitByTransform = other.transform;
            GetHit(5 + characterBase.INT);
            enumAttack = EnumAttack.fireTornado;
        }
        if (other.CompareTag("Bomb"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(bombHitSFX);
            GetHit(30 + characterBase.INT);
        }
        if (Hp <= 0)
        {
            collider.enabled = false;
        }
    }

    public virtual void MonsterDead()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
        if (animator != null && animator.GetBool("Dead") == false)
        {
            animator.SetBool("Dead", true);
        }
        healthBar.SetActive(false);

        Vector3 itemLocation = this.transform.position;//獲得當前怪物的地點
        int rewardItems = Random.Range(numHeldItemMin, numHeldItemMax);//隨機裝備產生值
        for (int i = 0; i < rewardItems; i++)
        {
            //Instantiate(gold, transform.position, transform.rotation);
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-1, 1), 0.2f, Random.Range(-1, 1));//在死亡地點周圍隨機分布
            float RateCnt_ = 0;//物品產生的最小值
            float ItemRandom_ = Random.Range(0, 100) / 100f;//隨機裝備機率值
            for (int j = 0; j < itemRate.ItemObjList.Length; j++)
            {
                RateCnt_ += itemRate.ItemObjRateList[j];
                if (ItemRandom_ < RateCnt_)
                {
                    Instantiate(itemRate.ItemObjList[j], randomItemLocation, itemRate.ItemObjList[j].transform.rotation);
                    break;
                }
            }
        }
    }
}
