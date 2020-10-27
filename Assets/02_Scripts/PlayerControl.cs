using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    PlayerAction playerAction;
    public PlayerOptions playerOptions;
    public UIBarControl uIBarControl;
    public PlayerFaceDirection playerFaceDirection;
    public GameMenu gameMenu;
    public HealthBarOnGame healthbarongame;
    public EquipmentManager equipmentManager;
    public GetHitEffect getHitEffect;
    public GameObject backPackUI;
    public GameObject skillUI;
    public GameObject miniMap;
    public Transform playerRotation;
    public Transform rollDirection;
    public new Rigidbody rigidbody;
    public new Collider collider;
    //有關耐力
    public float stamina;
    float staminaLimit = 100;
    float staminaRoll = 10;
    //有關移動
    public int moveSpeed = 5;//移動速度
    public bool isInvincible = false;//無敵狀態
    public bool cantMove = false;//移動限制
    float ws;
    float ad;
    Vector3 Movement;
    //有關翻滾
    float rollTime = 0f;//被存入的時間
    public float rollTimeLimit = 0.4f;//翻滾的無敵時間
    public int rollForce;
    public int rollDistence;
    LayerMask rayMask;
    public LayerMask wall, monster;
    Vector3 oldPosition;
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
    public float fireRate=1f;

    void Start()
    {
        lastFireTime = 10f;
        //讓角色一開始可以攻擊
        attackTime = 10;
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        stamina = staminaLimit;
        uIBarControl.SetMaxStamina(staminaLimit);
        playerAction = GetComponentInChildren<PlayerAction>();
        collider = GetComponentInParent<Collider>();
        oldPosition = transform.position;
        rayMask = wall & monster;
        playerOptions = FindObjectOfType<PlayerOptions>();
    }
    private void FixedUpdate()//好用的東東
    {
        ws = Input.GetAxis("Vertical");//世界軸
        //GunAudio.PlayOneShot(walkSFX);
        ad = Input.GetAxis("Horizontal");
        //GunAudio.PlayOneShot(walkSFX);
        if (isAttack)
        {
            Movement.Set(0, 0, 0);
        }
        else if (!isAttack)
        {
            Movement.Set(-ws, 0f, ad);
        }
        //如果有Movement.normalized會延遲很嚴重 因為四捨五入?
        Movement = Movement * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + Movement);
    }

    [System.Obsolete]
    void Update()
    {
        lastFireTime += Time.deltaTime;
        Movement.Set(0, 0, 0);
        //攻速&普攻按鍵
        attackTime += Time.deltaTime;
        if (attackTime >= attackSpeed && 
            !gameMenu.anyWindow[0].activeSelf && !gameMenu.anyWindow[2].activeSelf&& !gameMenu.anyWindow[4].activeSelf && !gameMenu.anyWindow[5].activeSelf && !gameMenu.anyWindow[6].activeSelf &&
            !playerFaceDirection.isMagicAttack && getHitEffect.playerHealth>0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Attack();
                attackTime = 0;//另外一種計時方式
            }
            if (Input.GetMouseButtonDown(1) && attackTime >= attackSpikeSpeed)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Attack();
                attackTime = 0;//另外一種計時方式
            }
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
        if (Input.GetKeyDown(KeyCode.B) && !gameMenu.anyWindow[0].activeSelf)
        {
            backPackUI.SetActive(!backPackUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.K) && !gameMenu.anyWindow[0].activeSelf)
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
            //SaveSystem.SavePlayer(getHitEffect, transform);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("回復存檔");
            PlayerData data = SaveSystem.LoadPlayer();
            //getHitEffect.playerHealth = data.Playerhealth;
            uIBarControl.SetHealth(data.Playerhealth);
            Vector3 SavePosition;
            SavePosition.x = data.position[0];
            SavePosition.y = data.position[1];
            SavePosition.z = data.position[2];
            transform.position = SavePosition;
            healthbarongame.SetHealth(data.Playerhealth);//人物身上的血條
        }
        #endregion
        //翻滾
        if (Input.GetKeyDown(KeyCode.Space) && getHitEffect.playerHealth>0)
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
            cantMove = false;
            rollTime = 0;
            rigidbody.velocity = Vector3.zero;
            collider.isTrigger = false;
            rigidbody.useGravity = true;
            rayMask = wall & monster;
        }

        if (stamina < staminaLimit)
        {
            stamina += Time.deltaTime * 10;
            uIBarControl.SetStamina(stamina);
        }
        else
        {
            stamina = staminaLimit;
            uIBarControl.SetStamina(stamina);
        }

        if (Input.GetKeyDown(KeyCode.N))//傳送到指定地點 &場景重置
        {
            transform.position = new Vector3(-6.31f, 0, 6.08f);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    void LateUpdate()
    {
        RaycastHit hit;
        //原點,方向,hit,最大移動距離,可變更的rayMask
        if (Physics.Raycast(oldPosition, (transform.position - oldPosition), out hit, Vector3.Distance(oldPosition, transform.position), rayMask))
        {
            //撞到牆velocity歸零
            rigidbody.velocity = Vector3.zero;
            //將玩家移動到被射線打到的點
            rigidbody.MovePosition(hit.point);
            Debug.Log("穿牆");
        }
        oldPosition = transform.position;
    }
    public void Attack()
    {
        //false在動畫Event呼叫
        //isAttack = true;
        playerFaceDirection.PlayerSpriteFlip();
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = playerRotation.forward * normalAttackDash;
            playerAction.NormalAttack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rigidbody.velocity = playerRotation.forward * spikeAttackDash;
            playerAction.SpikeAttack();
        }
    }
    void Roll()
    {
        rayMask = wall;
        oldPosition = transform.position;
        isAttack = false;
        playerFaceDirection.isMagicAttack = false;
        if (stamina > staminaRoll)
        {
            //歸零動量
            rigidbody.velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            stamina -= staminaRoll;
            //將損失的耐力顯示在上面
            uIBarControl.SetStamina(stamina);
            playerAction.Roll();
            //開啟無敵狀態
            isInvincible = true;
            //限制翻滾時不能轉向
            cantMove = true;
            rigidbody.velocity = rollDirection.forward * rollDistence;
            //讓玩家可以穿過怪物
            //collider.isTrigger = true;
            //不要讓玩家掉下去
            rigidbody.useGravity = false;
        }
    }
}
