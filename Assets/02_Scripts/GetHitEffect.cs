﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    float getHitInvincibleTime;
    float getHitInvincible = 1f;
    public int pickGold = 10000;
    public float maxHp = 100;
    public float playerHealth = 0;
    public HealthBarOnGame healthbarongame;
    public UIBarControl uIBarControl;
    PlayerControl playerControl;
    public GameMenu gameMenu;
    public GameObject changeColor;
    public Transform playerRotation;
    public SpriteRenderer spriteRenderer;
    public int bounceForce = 10000;
    public bool getHit;
    public bool attackBuff;

    void Start()
    {
        playerHealth = maxHp;
        uIBarControl.SetMaxHealth(maxHp);//UI身上的血條
        healthbarongame.SetMaxHealth(maxHp);//人物身上的血條
        playerControl = GetComponent<PlayerControl>();
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
            pickGold += 3;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Green"))
        {
            pickGold += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("White"))
        {
            pickGold += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Blue"))
        {
            pickGold += 5;
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
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                //並且朝反方向彈跳一下
                gameObject.GetComponent<Rigidbody>().AddForce(-playerRotation.forward * bounceForce);
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
        gameMenu.anyWindow[6].SetActive(true);
        Time.timeScale = 0;
    }
}
