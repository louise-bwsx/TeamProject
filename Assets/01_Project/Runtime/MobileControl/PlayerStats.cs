﻿using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float invincibleLimit;
    public int dust = 99999;
    public HealthBarOnGame healthbarongame;
    public GameObject[] getHitEffect;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float invincibleTimer;

    private CharacterBase characterBase;
    private MobileRoll mobileRoll;
    private MobileAttack mobileAttack;

    public UnityEvent<float> OnHealthChange = new UnityEvent<float>();

    private void Start()
    {
        hp = maxHp;
        OnHealthChange?.Invoke(1);
        characterBase = FindObjectOfType<CharacterBase>();
        mobileRoll = GetComponentInChildren<MobileRoll>();
        mobileAttack = FindObjectOfType<MobileAttack>();
    }

    private void Update()
    {
        if (invincibleTimer > 0)
        {
            mobileRoll.isInvincible = true;
            invincibleTimer -= Time.deltaTime;
        }
        else if (invincibleTimer < 0 && mobileRoll.isInvincible)
        {
            invincibleTimer = 0;
            mobileRoll.isInvincible = false;
            spriteRenderer.color = Color.white;
        }
        if (hp <= 0 && GetComponent<Collider>().enabled)
        {
            mobileAttack.isAttack = true;
            animator.SetTrigger("Dead");
            GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //放在Stay會重複傷害因為大招不會因為玩家碰到而消失
        if (hp > 0 && !mobileRoll.isInvincible)
        {
            if (other.gameObject.CompareTag("BossUlt") && characterBase.charaterStats[(int)CharacterStats.DEF] - 20 < 0)
            {
                getHitEffect[0] = getHitEffect[1];
                //絕對值(人物的防禦值-20)<0
                hp -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF] - 20);
                //玩家設定為無敵狀態
                mobileRoll.isInvincible = true;
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
            else if (other.gameObject.CompareTag("MonsterAttack") && characterBase.charaterStats[(int)CharacterStats.DEF] - 10 < 0)
            {
                Debug.Log("danger");
                //絕對值(人物的防禦值-10)<0
                hp -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF] - 10);
                //玩家設定為無敵狀態
                mobileRoll.isInvincible = true;
                //怪打到玩家時把無敵時間輸入進去
                invincibleTimer = invincibleLimit;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(hp);
                //將血量輸入到畫面上的UI
                OnHealthChange?.Invoke(hp / maxHp);
                //玩家貼圖變紅
                spriteRenderer.color = Color.red;
                Debug.Log(mobileRoll.isInvincible);
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
        if (hp > 0)
        {
            if (collision.gameObject.CompareTag("Gold") ||
               collision.gameObject.CompareTag("Green") ||
               collision.gameObject.CompareTag("White") ||
               collision.gameObject.CompareTag("Blue"))
            {
                dust += 5;
                hp += 5;
                Destroy(collision.gameObject);
                OnHealthChange?.Invoke(hp / maxHp);
                healthbarongame.SetHealth(hp);
            }
        }
    }
}