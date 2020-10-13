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
    public HealthBarOnGame healthBarOnGame;
    public int numHeldItemMin = 1;//裝備生成最小數
    public int numHeldItemMax = 3;//裝備生成最大數
    public ItemSTO itemRate;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject healthBar;
    public GameObject getHitEffect;

    public AudioSource GunAudio;//音樂放置
    public AudioClip SwordHitSFX;//突擊受擊音效
    public AudioClip PoisonHitSFX;//毒受擊音效
    public AudioClip FirMagicHitSFX;//火受擊音效
    public AudioClip tornadoHitSFX;//風受擊音效
    public AudioClip AirAttackHitSFX;//水受擊音效
    public AudioClip FiretornadoHitSFX;//龍捲風受擊音效
    public AudioClip BombHitSFX;//爆炸受擊音效

    void Update()
    {
        if (animator != null)
        {
            if (animator.GetBool("IsDead"))
            {
                Destroy(gameObject);
            }
        }
        else if(animator == null && Hp<=0)
        {
            Destroy(gameObject);
        }
    }
    public void GetHit(float Damage)
    {
        GameObject FX = Instantiate(getHitEffect, transform);
        Destroy(FX, 1);
        Hp -= Damage;
        healthBarOnGame.SetHealth(Hp);
        if (Hp <= 0)
        {
            MonsterDead();
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
            GunAudio.PlayOneShot(SwordHitSFX);
            GetHit(15);
        }
        if (other.CompareTag("Skill"))
        {
            GetHit(0);
        }
        if (other.CompareTag("AirAttack"))
        {
            GunAudio.PlayOneShot(AirAttackHitSFX);
            GetHit(10);
        }
        if (other.CompareTag("FireAttack"))
        {
            GunAudio.PlayOneShot(FirMagicHitSFX);
            GetHit(10);
        }
        if (other.CompareTag("Tornado"))
        {
            GunAudio.PlayOneShot(tornadoHitSFX);
            GetHit(15);
        }
        if (other.CompareTag("Poison"))
        {
            GunAudio.PlayOneShot(PoisonHitSFX);
            GetHit(15);
        }
        if (other.CompareTag("Firetornado"))
        {
            GunAudio.PlayOneShot(FiretornadoHitSFX);
            GetHit(30);
        }
        if (other.CompareTag("Bomb"))
        {
            GunAudio.PlayOneShot(BombHitSFX);
            GetHit(30);
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
            animator.SetBool("Dead",true);
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
