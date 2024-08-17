using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    //TODOError: 這裡之後取消被PlayerStats替代
    public float getHitInvincibleTime;
    float getHitInvincible = 1f;
    public float maxHealth = 100;
    public float playerHealth = 0;
    public HealthBarOnGame healthbarongame;
    private UIBarControl uiBarControl;
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
    CharacterBase characterBase;


    private void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        playerHealth = maxHealth;
        healthbarongame.SetMaxHealth(maxHealth);//人物身上的血條
        RD = GetComponent<Rigidbody>();
    }

    private void Update()
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
            if (getHitInvincibleTime < 0f)
            {
                getHitInvincibleTime = 0f;
                spriteRenderer.color = Color.white;
            }
        }
        if (playerHealth <= 0 && GetComponent<Collider>().enabled == true)
        {
            //死掉後玩家不能動
            playerControl.isAttack = true;
            animator.SetTrigger("Dead");
            GetComponent<Collider>().enabled = false;
            AudioManager.Inst.PlayBGM("Dead");
            GameStateManager.Inst.ChangState(GameState.PlayerDead);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //放在Stay會重複傷害因為大招不會因為玩家碰到而消失
        if (other.gameObject.CompareTag("BossUlt") && playerHealth > 0 && characterBase.charaterStats[(int)CharacterStats.DEF] - 20 < 0)
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible)
            {
                getHitEffect[0] = getHitEffect[1];
                Debug.Log("danger");
                //絕對值(人物的防禦值-20)<0
                playerHealth -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF] - 20);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //生出被大招打中的特效
                GameObject bossUltHitFX = Instantiate(getHitEffect[1], transform.position, transform.rotation);
                Destroy(bossUltHitFX, 1f);
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uiBarControl.SetHealth(playerHealth / maxHealth);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("Item"))
        //{
        //    other.GetComponent<ItemPickup>().PickUp();
        //}
        if (other.CompareTag("Wall"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
        if (other.gameObject.CompareTag("MonsterAttack") && getHitInvincibleTime <= 0f)
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible && playerHealth > 0 && characterBase.charaterStats[(int)CharacterStats.DEF] - 10 < 0)
            {
                Debug.Log("danger");
                //絕對值(人物的防禦值-10)<0
                playerHealth -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF] - 10);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uiBarControl.SetHealth(playerHealth / maxHealth);
            }
        }
        //當玩家無敵狀態
        else if (playerControl.isInvincible)
        {
            //尚未實作
            //Debug.Log("攻擊力*3");
            attackBuff = true;
        }
    }
}

