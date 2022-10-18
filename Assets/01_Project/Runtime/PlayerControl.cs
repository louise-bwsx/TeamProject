using UnityEngine;
using UnityEngine.EventSystems;

public enum AttackType
{
    NormalAttack,
    SpikeAttack
}

public class PlayerControl : MonoBehaviour
{
    PlayerAction playerAction;
    CharacterBase characterBase;
    public PlayerOptions playerOptions;
    public UIBarControl uIBarControl;
    public PlayerFaceDirection playerFaceDirection;
    public GameMenuController gameMenu;
    public HealthBarOnGame healthbarongame;
    public EquipmentManager equipmentManager;
    public GetHitEffect getHitEffect;
    public GameObject backPackUI;
    public GameObject skillUI;
    public GameObject miniMap;
    public Transform playerRotation;
    public Transform rollDirection;
    public new Collider collider;
    //有關耐力
    public float stamina;
    float staminaLimit = 100;
    float staminaRoll = 10;
    //有關移動
    public int moveSpeed = 5;//移動速度
    public bool isInvincible = false;//無敵狀態
    float ws;
    float ad;
    //有關翻滾
    float rollTime = 0f;//被存入的時間
    public float rollTimeLimit = 0.4f;//翻滾的無敵時間
    public int rollForce;
    public int rollDistence;
    //有關攻擊
    public bool isAttack = false;
    public float attackRange = 0.4f;
    public float attackTime;
    public float attackSpeed;//同動畫時間
    public float attackSpikeSpeed;//突刺攻擊間隔
    public int attackDamage = 2;
    public int spikeAttackDash = 7;
    public int normalAttackDash = 3;
    public LayerMask EnemyLayer;
    public float lastFireTime;
    public float fireRate = 1f;
    Vector3 position;
    public Animator animator;
    public bool isRoll;

    [field: SerializeField] public Rigidbody rigidbody { get; private set; }
    [SerializeField] private LayerMask wall;
    [SerializeField] private LayerMask monster;
    private Vector3 previousPos;
    private LayerMask layerMask;
    private Vector3 moveDirection;

    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        playerAction = GetComponentInChildren<PlayerAction>();
        collider = GetComponentInParent<Collider>();
        playerOptions = FindObjectOfType<PlayerOptions>();
        characterBase = FindObjectOfType<CharacterBase>();
    }

    private void Start()
    {
        lastFireTime = 10f;
        //讓角色一開始可以攻擊
        attackTime = 10;
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        stamina = staminaLimit;
        uIBarControl.SetMaxStamina();
        previousPos = transform.position;
        layerMask = wall & monster;
    }

    private void Update()
    {
        //不能用transform因為這個Script跟rigidbody的位置不一樣所以在rigidbody沒有freezePos.y.enable的情況會往上飛
        //rigidbody.MovePosition(transform.position + Movement);
        ws = Input.GetAxisRaw("Vertical");//世界軸
        ad = Input.GetAxisRaw("Horizontal");
        if (isAttack && animator.GetBool("IsAttack"))
        {
            moveDirection.Set(0, 0, 0);
        }
        else if (!isAttack && !animator.GetBool("IsAttack"))
        {
            moveDirection.Set(-ws, 0f, ad);
        }
        moveDirection.Normalize();
        if (moveDirection != Vector3.zero)
        {
            rigidbody.position += moveDirection * (moveSpeed + characterBase.charaterStats[(int)CharacterStats.AGI]) * Time.deltaTime; ;
        }

        lastFireTime += Time.deltaTime;
        //不懂為什麼這邊原本要Set(0, 0, 0)
        //Movement.Set(0, 0, 0);
        //攻速&普攻按鍵
        attackTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && attackTime >= attackSpeed)
        {
            Attack(AttackType.NormalAttack);
        }
        if (Input.GetMouseButtonDown(1) && attackTime >= attackSpikeSpeed)
        {
            Attack(AttackType.SpikeAttack);
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
        //開關背包介面
        if (Input.GetKeyDown(KeyCode.B) && !gameMenu.IsMenuActive("Menu"))
        {
            backPackUI.SetActive(!backPackUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.K) && GameStateManager.Inst.CurrentState == GameState.Gaming)
        {
            skillUI.SetActive(!skillUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            miniMap.SetActive(!miniMap.activeSelf);
        }

        //卸下所有裝備欄裡的裝備
        if (Input.GetKeyDown(KeyCode.U))
        {
            equipmentManager.UnequipAll();
        }
        #region 存檔功能
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("存檔中");
            SaveSystem.SavePlayer(getHitEffect, transform);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("回復存檔");
            PlayerData data = SaveSystem.LoadPlayer();
            getHitEffect.playerHealth = data.Playerhealth;
            uIBarControl.SetHealth(data.Playerhealth / data.maxHealth);
            Vector3 SavePosition;
            SavePosition.x = data.position[0];
            SavePosition.y = data.position[1];
            SavePosition.z = data.position[2];
            transform.position = SavePosition;
            Debug.Log(SavePosition);
            healthbarongame.SetHealth(data.Playerhealth);//人物身上的血條
        }
        #endregion
        //翻滾
        if (Input.GetKeyDown(KeyCode.Space) && getHitEffect.playerHealth > 0/* && !animator.GetBool("IsAttack")*/)
        {
            if (lastFireTime > fireRate)
            {
                Roll();
                lastFireTime = 0;
            }

        }
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
            rigidbody.useGravity = true;
            layerMask = wall & monster;
            isRoll = false;
        }

        if (stamina < staminaLimit)
        {
            stamina += Time.deltaTime * 10;
            uIBarControl.SetStamina(stamina / staminaLimit);
        }
        else
        {
            stamina = staminaLimit;
            uIBarControl.SetStamina(stamina / staminaLimit);
        }

        if (Input.GetKeyDown(KeyCode.N))//傳送到指定地點 &場景重置
        {
            SceneManager.Inst.LoadLevel("GameScene");
        }
    }

    private void LateUpdate()
    {
        CheckMoveThroughWall();
        PlayerStandGround();
    }

    private void CheckMoveThroughWall()
    {
        Vector3 raycastDir = rigidbody.position - previousPos;
        float distance = Vector3.Distance(previousPos, rigidbody.position);
        RaycastHit hit;
        if (Physics.Raycast(previousPos, raycastDir, out hit, distance, layerMask))
        {
            //撞到牆velocity歸零
            rigidbody.velocity = Vector3.zero;
            //將玩家移動到被射線打到的點
            rigidbody.position = hit.point;
            Debug.Log("穿牆");
        }
        previousPos = rigidbody.position;
    }

    private void PlayerStandGround()
    {
        LayerMask floor = LayerMask.GetMask("Floor");
        float distance = 5;
        float lift = 0.5f;
        RaycastHit hit;
        //沒有transform.down 負數的選項
        if (Physics.Raycast(rigidbody.position, -rigidbody.transform.up, out hit, distance, floor))
        {
            Vector3 hitLift = hit.point + rigidbody.transform.up * lift;
            Vector3 originPos = rigidbody.position;
            originPos.y = hitLift.y;
            rigidbody.position = originPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(rigidbody.position, rigidbody.transform.localPosition.With(y: rigidbody.transform.localPosition.y - 1));
    }

    private void Attack(AttackType type)
    {
        //TODO: 會因為點到血條沒辦法攻擊 做個script放在指定UI來判斷
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("點到UI無法攻擊");
            return;
        }
        //TODO: 這裡應該可以用GameState來判斷 currentGameState != GameState.Gaming return;
        if (gameMenu.IsMenuActive("Menu") || gameMenu.IsMenuActive("Load") || gameMenu.IsMenuActive("Save") || gameMenu.IsMenuActive("Settings") || gameMenu.IsMenuActive("DiePanel"))
        {
            Debug.Log("其中一個Menu開啟無法攻擊");
            return;
        }
        //TODO: 這裡應該可以用GameState來判斷 currentGameState != GameState.Gaming return;
        if (playerFaceDirection.isMagicAttack || getHitEffect.playerHealth < 0 || isRoll)
        {
            Debug.Log("玩家在一個無法攻擊的狀態");
            return;
        }
        //false在動畫Event呼叫
        isAttack = true;
        //讓人物轉向
        playerFaceDirection.PlayerSpriteFlip();
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
        playerAction.NormalAttack();
    }

    private void SpikeAttack()
    {
        rigidbody.velocity = playerRotation.forward * spikeAttackDash;
        playerAction.SpikeAttack();
    }

    void Roll()
    {
        layerMask = wall;
        previousPos = rigidbody.position;
        isAttack = false;
        isRoll = true;
        playerFaceDirection.isMagicAttack = false;
        if (stamina > staminaRoll)
        {
            //歸零動量
            rigidbody.velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            stamina -= staminaRoll;
            //將損失的耐力顯示在上面
            uIBarControl.SetStamina(stamina / staminaLimit);
            playerAction.Roll();
            //開啟無敵狀態
            isInvincible = true;
            rigidbody.velocity = rollDirection.forward * rollDistence;
            //讓玩家可以穿過怪物
            //collider.isTrigger = true;
            //不要讓玩家掉下去
            rigidbody.useGravity = false;
        }
    }
}
