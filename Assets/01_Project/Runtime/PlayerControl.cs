using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackType
{
    NormalAttack,
    SpikeAttack
}

//角色Rigidbody的Interpolate的下拉式選單改成interpolate就可以把EquipmentGroup就可以刪掉
//原因是EquipmentGroup裡的物件Rigidbody的Interpolate選項是Extrapolate會影響其他物件
//Interpolate和Extrapolate 最好只讓玩家使用就好

public class PlayerControl : MonoBehaviour
{
    //有關耐力
    [SerializeField] private PlayerStamina stamina;
    //有關移動
    public int moveSpeed = 5;
    public bool isInvincible = false;//無敵狀態
    private float ws;
    private float ad;
    //有關翻滾
    private float rollCostStamina = 10;
    float rollTime = 0f;//被存入的時間
    public float rollTimeLimit = 0.4f;//翻滾的無敵時間
    public int rollDistence;
    private float rollCD;
    [SerializeField] private float rollRate = 1f;
    private Vector3 rollDirection = Vector3.right;
    //有關攻擊
    public bool isAttack = false;
    public float attackTime;
    public float attackSpeed;//同動畫時間
    public float attackSpikeSpeed;//突刺攻擊間隔
    public int spikeAttackDash = 7;
    public int normalAttackDash = 3;
    public LayerMask EnemyLayer;
    public bool isRoll;

    [field: SerializeField] public Rigidbody rigidbody { get; private set; }
    [SerializeField] private LayerMask rollLayer;
    [SerializeField, ReadOnly] private Vector3 previousPos;
    private Vector3 moveDirection;
    private PlayerOptions playerOptions;
    private PlayerSprite playerSprite;
    private Collider collider;
    private Animator animator;
    private PlayerStats playerStats;
    private ShootDirectionSetter shootDirectionSetter;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerOptions = GetComponent<PlayerOptions>();
        playerSprite = GetComponentInChildren<PlayerSprite>();
        shootDirectionSetter = GetComponent<ShootDirectionSetter>();
    }

    private void Start()
    {
        //讓角色一開始可以攻擊
        attackTime = 10;
        previousPos = transform.position;
    }

    private void Update()
    {
        rollCD -= Time.deltaTime;
        if (isInvincible)
        {
            rollTime += Time.deltaTime;
        }
        //無敵時間超過上限取消無敵狀態
        if (rollTime > rollTimeLimit)
        {
            isInvincible = false;
            rollTime = 0;
            rigidbody.velocity = Vector3.zero;
            collider.isTrigger = false;
            isRoll = false;
        }

        if (!GameStateManager.Inst.IsGaming())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //避免在死掉時按儲存
            if (GameStateManager.Inst.IsGaming())
            {
                SaveManager.Inst.Save();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.Inst.LoadLevel("GameScene");
        }

        //不檢查的話 這邊會覆蓋掉mobile mobile會沒辦法播放移動動畫
        if (GameStateManager.Inst.IsMobile)
        {
            return;
        }

        ws = Input.GetAxisRaw("Vertical");
        ad = Input.GetAxisRaw("Horizontal");
        SetMoveDirection(ws, ad);

        //攻速&普攻按鍵
        attackTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && attackTime >= attackSpeed)
        {
            Attack(AttackType.NormalAttack);
            return;
        }
        if (Input.GetMouseButtonDown(1) && attackTime >= attackSpikeSpeed)
        {
            Attack(AttackType.SpikeAttack);
            return;
        }

        //翻滾
        if (Input.GetKeyDown(KeyCode.Space) /* && !animator.GetBool("IsAttack")*/)
        {
            Roll();
            return;
        }

        //開關玩家血條
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerOptions.SetPlayerHealthActive();
        }
        //開關怪物血條
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerOptions.SetMonsterHealthActive();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (UIManager.Inst.IsUIOpen("StatsWindow"))
            {
                UIManager.Inst.CloseMenu("StatsWindow");
            }
            UIManager.Inst.OpenMenu("StatsWindow");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (UIManager.Inst.IsUIOpen("SkillWindow"))
            {
                UIManager.Inst.CloseMenu("SkillWindow");
            }
            UIManager.Inst.OpenMenu("SkillWindow");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIManager.Inst.IsUIOpen("MiniMap"))
            {
                UIManager.Inst.CloseMenu("MiniMap");
            }
            UIManager.Inst.OpenMenu("MiniMap");
        }
    }

    private void LateUpdate()
    {
        CheckMoveThroughWall();
        PlayerStandGround();
    }

    public void SetMoveDirection(float vertical, float horizontal)
    {
        if (isAttack || animator.GetBool("IsAttack") || animator.GetBool("Magic_Prepare"))
        {
            moveDirection.Set(0, 0, 0);
            return;
        }

        //避免先按住左 再按住右會播放移動動畫
        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) ||
           (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            horizontal = 0f;
        }
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) ||
            (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)))
        {
            vertical = 0f;
        }

        moveDirection.Set(-vertical, 0f, horizontal);
        moveDirection.Normalize();
        if (moveDirection != Vector3.zero)
        {
            rollDirection = moveDirection;
        }
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);

        if (moveDirection != Vector3.zero)
        {
            //Debug.Log(vertical);
            //Debug.Log(horizontal);
            playerSprite.SpriteFlipByInputDirection(horizontal);
            rigidbody.position += moveDirection * ((moveSpeed + playerStats.GetStatLevel(StatType.AGI)) * Time.deltaTime);
        }
    }

    public void Attack(AttackType type)
    {
        //不要利用 EventSystem.current.IsPointerOverGameObject() 會因為點到血條沒辦法攻擊
        if (UIManager.Inst.IsUIOpen("StatsWindow") ||
            UIManager.Inst.IsUIOpen("SkillWindow"))
        {
            return;
        }

        if (playerSprite.isMagicAttack || playerStats.IsDead() || isRoll)
        {
            Debug.Log("玩家在一個無法攻擊的狀態");
            return;
        }
        //false在動畫Event呼叫
        isAttack = true;
        //讓人物轉向滑鼠點擊方向 為了不讓手機模式 改變方向
        if (!GameStateManager.Inst.IsMobile)
        {
            playerSprite.SpriteFlipByMousePosition();
        }
        switch (type)
        {
            case AttackType.NormalAttack:
                NormalAttack();
                break;
            case AttackType.SpikeAttack:
                SpikeAttack();
                break;
        }
        attackTime = 0;
    }

    private void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    private void SpikeAttack()
    {
        animator.SetTrigger("Attack_Spike");
        if (GameStateManager.Inst.IsMobile)
        {
            Vector3 direction = playerSprite.FlipX() ? Vector3.forward : Vector3.back;
            Debug.Log(direction);
            rigidbody.velocity = direction * spikeAttackDash;
            return;
        }
        rigidbody.velocity = shootDirectionSetter.GetForward() * spikeAttackDash;
    }

    private void CheckMoveThroughWall()
    {
        Vector3 raycastDir = rigidbody.position - previousPos;
        float distance = Vector3.Distance(previousPos, rigidbody.position);
        RaycastHit hit;
        if (Physics.Raycast(previousPos, raycastDir, out hit, distance, rollLayer))
        {
            //Debug.Log("穿牆");
            //撞到牆velocity歸零
            rigidbody.velocity = Vector3.zero;
            //將玩家移動到被射線打到的點
            rigidbody.position = hit.point;
        }
        previousPos = rigidbody.position;
    }

    private void PlayerStandGround()
    {
        //不要讓 rigidbody.useGravity = true; 會讓玩家變成不斷墜落的狀態 因為collider懸空 為了走樓梯不卡住
        LayerMask floor = LayerMask.GetMask("Floor");
        float distance = 5;
        float lift = 0.4f;
        RaycastHit hit;
        //沒有transform.down 負數的選項
        if (Physics.Raycast(rigidbody.position, -rigidbody.transform.up, out hit, distance, floor))
        {
            Debug.Log("PlayerStandGround");
            Vector3 hitLift = hit.point + rigidbody.transform.up * lift;
            Vector3 originPos = rigidbody.position;
            originPos.y = hitLift.y;
            rigidbody.position = originPos;
        }
    }

    public void Roll()
    {
        if (rollCD > 0 ||
            !GameStateManager.Inst.IsGaming() ||
            !stamina.IsEnough(rollCostStamina) ||
            playerStats.IsDead())
        {
            return;
        }

        animator.SetTrigger("Roll");
        animator.SetBool("IsAttack", false);
        AudioManager.Inst.PlaySFX("Roll");
        rollCD = rollRate;
        previousPos = rigidbody.position;
        isAttack = false;
        isRoll = true;
        playerSprite.isMagicAttack = false;
        stamina.Cost(rollCostStamina);
        //開啟無敵狀態
        isInvincible = true;
        rigidbody.velocity = rollDirection * rollDistence;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(rigidbody.position, rigidbody.transform.localPosition.With(y: rigidbody.transform.localPosition.y - 1));
    }
}