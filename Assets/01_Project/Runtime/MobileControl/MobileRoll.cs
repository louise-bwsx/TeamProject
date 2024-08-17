using UnityEngine;

public class MobileRoll : MonoBehaviour
{
    public float rollTimer;
    public float rollCost;
    public float rollCD;
    public float invincibleTime;
    public int rollDistence;
    public bool isRoll;
    public bool isInvincible;
    public LayerMask wall, monster;
    public UIBarControl uIBarControl;
    public HealthBarOnGame healthbarongame;
    public AudioClip rollSFX;
    public Transform rollDirection;
    MobileAttack mobileAttack;
    MobileMove mobileMove;
    LayerMask rayMask;
    Vector3 oldPosition;
    Rigidbody RB;
    Animator animator;
    private PlayerStamina playerStamina;
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerStamina = GetComponent<PlayerStamina>();
        mobileMove = GetComponent<MobileMove>();
        animator = GetComponent<Animator>();
        uIBarControl = FindObjectOfType<UIBarControl>();
        RB = GetComponentInParent<Rigidbody>();
        mobileAttack = FindObjectOfType<MobileAttack>();
    }

    private void Start()
    {
        //初始化耐力值為最大值
        //不可穿牆的layer是wall和monster
        rayMask = wall & monster;
    }
    void Update()
    {
        rollTimer += Time.deltaTime;
        //無敵狀態切換 及 迴避移動停止
        if (rollTimer > invincibleTime && isInvincible)
        {
            //迴避移動停止
            RB.velocity = Vector3.zero;
            //玩家現在不能穿wall 及 monster
            rayMask = wall & monster;
            //取消不能攻擊的限制
            isRoll = false;
            //取消無敵狀態
            isInvincible = false;
        }
    }

    void LateUpdate()
    {
        RaycastHit hit;
        //原點,方向,hit,最大移動距離,可變更的rayMask
        if (Physics.Raycast(oldPosition, (transform.position - oldPosition), out hit, Vector3.Distance(oldPosition, transform.position), rayMask))
        {
            //撞到牆velocity歸零
            RB.velocity = Vector3.zero;
            //將玩家移動到被射線打到的點
            RB.MovePosition(hit.point);
            Debug.Log("穿牆");
        }
        //每一幀的最後都把自己的位置存起來給下一幀使用
        oldPosition = transform.position;
    }

    private void RollStop()//動畫Event
    {
        RB.velocity = Vector3.zero;
    }

    public void Roll()
    {
        if (playerStamina.IsEnough(rollCost) && rollTimer > rollCD && playerStats.hp > 0)
        {
            //播動畫
            animator.SetTrigger("Roll");
            //取消攻擊動畫
            animator.SetBool("IsAttack", false);
            //播音效
            AudioManager.Inst.PlaySFX("Roll");
            //迴避的冷卻時間
            rollTimer = 0;
            //設定不能穿過wall
            rayMask = wall;
            //儲存oldPosition打射線
            oldPosition = transform.position;
            //取消攻擊中不能移動的限制
            mobileAttack.isAttack = false;
            //迴避中不能攻擊
            isRoll = true;
            //歸零動量
            RB.velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            playerStamina.Cost(rollCost);
            //開啟無敵狀態
            isInvincible = true;
            //朝rollDirection移動rollDistence距離
            RB.velocity = rollDirection.forward * rollDistence;
        }
    }
}