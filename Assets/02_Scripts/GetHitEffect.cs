using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    public float getHitInvincibleTime;
    float getHitInvincible = 1f;
    public int dust = 99999;
    public float maxHp = 100;
    public float playerHealth = 0;
    public HealthBarOnGame healthbarongame;
    public UIBarControl uIBarControl;
    public PlayerControl playerControl;
    public Rigidbody RD;
    public GameObject changeColor;
    public Transform playerRotation;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public int bounceForce = 10000;
    public bool getHit;
    public bool attackBuff;
    public GameObject[] getHitEffect;

    void Start()
    {
        dust = CentralData.GetInst().dust;
        playerHealth = maxHp;
        uIBarControl.SetMaxHealth(maxHp);//UI身上的血條
        healthbarongame.SetMaxHealth(maxHp);//人物身上的血條
        RD = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getHitInvincibleTime > 0f)
        {
            getHitInvincibleTime -= Time.deltaTime;
            spriteRenderer.color = Color.red;
        }
        else if (getHitInvincibleTime <= 0.6f)
        {
            //關閉被打中狀態
            getHit = false;
            //解除移動限制
            playerControl.cantMove = false;
            if (getHitInvincibleTime < 0f)
            {
                getHitInvincibleTime = 0f;
                spriteRenderer.color = Color.white;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gold"))
        {
            dust += 3;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Green"))
        {
            dust += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("White"))
        {
            dust += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Blue"))
        {
            dust += 5;
            Destroy(collision.gameObject);
        }

        //else if (collision.gameObject.CompareTag("Item"))
        //{
        //    collision.gameObject.GetComponent<ItemPickup>().PickUp();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        //放在Stay會重複傷害因為大招不會因為玩家碰到而消失
        if (other.gameObject.CompareTag("BossUlt"))
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible)
            {
                getHitEffect[0] = getHitEffect[1];
                Debug.Log("danger");
                //隨機怪物傷害 左 到 右 如果是int是 左 到 右-1
                playerHealth -= Random.Range(30f, 50f);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uIBarControl.SetHealth(playerHealth);
                //瞬間讓玩家不能動凸顯彈跳效果
                playerControl.cantMove = true;
                RD.AddForce(other.transform.forward * bounceForce);
                //玩家血量歸零時遊戲暫停
                if (playerHealth <= 0)
                {
                    //死掉後玩家不能動
                    playerControl.isAttack = true;
                    animator.SetTrigger("Dead");
                    GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<ItemPickup>().PickUp();
        }
        if (other.CompareTag("Block") || other.CompareTag("Wall"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
        if (other.gameObject.CompareTag("MonsterAttack") && getHitInvincibleTime <= 0f)
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible)
            {
                Debug.Log("danger");
                //隨機怪物傷害
                playerHealth -= Random.Range(10f, 20f);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uIBarControl.SetHealth(playerHealth);
                //瞬間讓玩家不能動凸顯彈跳效果
                playerControl.cantMove = true;
                RD.AddForce(other.transform.forward * bounceForce);
                //玩家血量歸零時遊戲暫停
                if (playerHealth <= 0)
                {
                    //死掉後玩家不能動
                    playerControl.isAttack = true;
                    animator.SetTrigger("Dead");
                    GetComponent<Collider>().enabled = false;
                }
            }
        }
        //當玩家無敵狀態
        else if (playerControl.isInvincible)
        {
            //尚未實作
            Debug.Log("攻擊力*3");
            attackBuff = true;
        } 
    }
}

