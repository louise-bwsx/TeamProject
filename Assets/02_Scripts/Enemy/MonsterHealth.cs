using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class MonsterHealth : MonoBehaviour
{
    public float Hp = 0;
    public float maxHp = 50;
    public int numHeldItemMin = 1;//裝備生成最小數
    public int numHeldItemMax = 3;//裝備生成最大數
    public float beAttackTime;
    public float attackTime;

    public HealthBarOnGame healthBarOnGame;
    public ItemSTO itemRate;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject healthBar;
    public GameObject[] getHitEffect;
    public CharacterBase characterBase;
    public SkillBase skillBase;

    public AudioSource audioSource;//音效在子類別調整音量大小
    public AudioClip SwordHitSFX;//突擊受擊音效
    public AudioClip PoisonHitSFX;//毒受擊音效
    public AudioClip FirMagicHitSFX;//火受擊音效
    public AudioClip tornadoHitSFX;//風受擊音效
    public AudioClip AirAttackHitSFX;//水受擊音效
    public AudioClip FiretornadoHitSFX;//龍捲風受擊音效
    public AudioClip BombHitSFX;//爆炸受擊音效

    public Transform monsterMove;
    public float beAttackMin = 0;//被打的次數
    public float beAttackMax = 0;//被打的最大次數
    public float gethit;
    public float gethitlimit = 0.3F;//間格秒數
    public float windColdTime = 5;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        characterBase = FindObjectOfType<CharacterBase>();//FindObjectOfType抓取整個場景有這個物件的方法
        skillBase = FindObjectOfType<SkillBase>();

        //讀不到
        //audioSource.volume = playerAction.audioSource.volume;

        beAttackMax = windColdTime / gethitlimit;


        Hp = maxHp;
        healthBarOnGame.SetMaxHealth(maxHp);
    }

    void Update()
    {
        if (animator != null)
        {
            if (animator.GetBool("IsDead"))
            {
                Destroy(gameObject);
            }
            else if (Hp <= 0)
            {
                Destroy(gameObject);//給Boss用的
            }
        }
        beAttackTime += Time.deltaTime;

        //風持續傷害
        gethit += Time.deltaTime;

        if (beAttackMin >= 1)
        {
            if (gethit > gethitlimit)
            {
                gethit = 0;
                GetHit(2);
                beAttackMin--;
            }
        }
        if (monsterMove == null)
        {
            beAttackMin = 0;
        }




        //else if(animator == null && Hp<=0)
        //{
        //    Destroy(gameObject);
        //}




    }
    public void GetHit(float Damage)
    {
        if (beAttackTime > attackTime)
        {
            //Debug.Log(transform.name);
            GameObject FX = Instantiate(getHitEffect[0], new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), transform.rotation);
            Destroy(FX, 1);
            Hp -= Damage;
            healthBarOnGame.SetHealth(Hp);
            if (Hp <= 0)
            {
                MonsterDead();
            }
            beAttackTime = 0;
        }

    }
    protected void OnTriggerEnter(Collider other)
    {
        //暫時用不到
        //if (other.CompareTag("Arrow"))
        //{
        //    //打出的傷害數值 失敗
        //    //text = Instantiate(text, new Vector3(x, 0.7f, z), transform.rotation);
        //    GetHit(10);
        //    other.gameObject.tag = "Broken";
        //}
        if (other.CompareTag("Sword"))
        {
            getHitEffect[0] = getHitEffect[1];
            //audioSource.PlayOneShot(SwordHitSFX);
            GetHit(15 + characterBase.STR);
        }
        if (other.CompareTag("Skill"))
        {
            getHitEffect[0] = getHitEffect[1];
            GetHit(0);
        }
        if (other.CompareTag("AirAttack"))
        {
            getHitEffect[0] = getHitEffect[1];
            audioSource.PlayOneShot(AirAttackHitSFX);
            GetHit(10 + characterBase.INT + skillBase.waterSkill);
        }
        if (other.CompareTag("FireAttack"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(FirMagicHitSFX);
            GetHit(10 + characterBase.INT + skillBase.fireSkill);
        }
        if (other.CompareTag("Tornado") && monsterMove != other.transform)
        {
            getHitEffect[0] = getHitEffect[3];
            beAttackMin = beAttackMax;//最大被打的次數
            monsterMove = other.transform;
            audioSource.PlayOneShot(tornadoHitSFX);
            GetHit(2 + characterBase.INT + skillBase.windSkill);
        }
        if (other.CompareTag("Poison") && monsterMove != other.transform)
        {
            getHitEffect[0] = getHitEffect[4];
            beAttackMin = 20;//最大被打的次數
            monsterMove = other.transform;
            audioSource.PlayOneShot(PoisonHitSFX);
            GetHit(1 + characterBase.INT + skillBase.poisonSkill);
        }
        if (other.CompareTag("Firetornado"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(FiretornadoHitSFX);
            GetHit(30 + characterBase.INT);
        }
        if (other.CompareTag("Bomb"))
        {
            audioSource.PlayOneShot(BombHitSFX);
            GetHit(30 + characterBase.INT);
        }

    }

    public virtual void MonsterDead()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
        if (animator != null)
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
