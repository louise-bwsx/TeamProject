using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float Hp =0;
    public float maxHp = 50;
    public HealthBarOnGame HealthBarOnGame;
    public int numHeldItemMin = 1;//裝備生成最小數
    public int numHeldItemMax = 3;//裝備生成最大數
    //public GameObject gold;//黃裝
    //public GameObject white;//白裝
    //public GameObject blue;//綠裝
    ////public GameObject GreenHelmet;
    //public GameObject GreenHead; //綠裝/頭盔
    //public GameObject GreenBreastplate;//綠裝/胸甲
    //public GameObject GreenLeg;//綠裝/腿甲
    //public GameObject SkillBookWhite;
    //public GameObject SkillBookGold;
    //public GameObject SkillBookBlue;
    public ItemSTO itemRate;
    public Animator animator;
    NavMeshAgent navMeshAgent;
    LongRangeEnemyController longRangeEnemyController;
    public GameObject healthBar;

    public AudioSource GunAudio;//音樂放置
    public AudioClip PoisonHitSFX;//毒受擊音效
    public AudioClip FireMagicHitSFX;//火受擊音效
    public AudioClip tornadoHitSFX;//風受擊音效
    public AudioClip AirAttackHitSFX;//水受擊音效
   
    



    void Start()
    {
        Hp = maxHp;
        HealthBarOnGame.SetMaxHealth(maxHp);
        navMeshAgent = GetComponent<NavMeshAgent>();
        longRangeEnemyController = GetComponent<LongRangeEnemyController>();
    }
    void Update()
    {
        if (animator != null)
        { 
            if (animator.GetBool("IsDead"))
            {
                Destroy(gameObject);
            }
        }
    }
    public void GetHit(float Damage)
    {
        Hp -= Damage;
        HealthBarOnGame.SetHealth(Hp);
        if (Hp <= 0)
        {
            MonsterDead();
        }
    }
    private void OnTriggerEnter(Collider other)
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
            GetHit(15);
        }
        if (other.CompareTag("Skill"))
        {
            GetHit(0);
        }
        if (other.CompareTag("AirAttack"))
        {
            GetHit(10);
        }
        if (other.CompareTag("FirMagic"))
        {
            GetHit(10);
        }
        if (other.CompareTag("tornado"))
        {
            GetHit(15);
        }
        if (other.CompareTag("Poison"))
        {
            GetHit(15);
        }
    }
    void MonsterDead()
    {
        animator.SetBool("Dead",true);
        longRangeEnemyController.enabled = false;
        navMeshAgent.enabled = false;
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
