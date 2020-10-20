using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    float getHitInvincibleTime;
    float getHitInvincible = 1f;
    public int dust = 99999;
    public float maxHp = 100;
    public float playerHealth = 0;
    public HealthBarOnGame healthbarongame;
    public UIBarControl uIBarControl;
    public PlayerControl playerControl;
    public Rigidbody RD;
    public GameMenu gameMenu;
    public GameObject changeColor;
    public Transform playerRotation;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip dieSFX;
    public int bounceForce = 10000;
    public bool getHit;
    public bool attackBuff;

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
                //GetComponent<Rigidbody>().velocity = Vector3.zero;
                //並且朝反方向彈跳一下
                //rigidbody.velocity = other.transform.forward * bounceForce;
                //方向對但是值會因為角色與怪物的距離越遠彈得越遠
                RD.AddForce((transform.position-other.transform.position) * bounceForce);

                RD.AddForce(other.transform.forward * bounceForce);
                //玩家血量歸零時遊戲暫停
                if (playerHealth <= 0)
                {
                    Die();
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
    public void Die()
    {
        audioSource.clip = dieSFX;
        audioSource.PlayOneShot(dieSFX);
        gameMenu.anyWindow[6].SetActive(true);
        Time.timeScale = 0;
    }
}
