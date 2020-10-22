using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    PlayerOptions playerOptions;
    PlayerAction playerAction;
    public UIBarControl uIBarControl;
    public PlayerFaceDirection playerFaceDirection;
    public GameMenu gameMenu;
    public HealthBarOnGame healthbarongame;
    public EquipmentManager equipmentManager;
    public GameObject backPackUI;
    public GameObject skillUI;
    public Transform playerRotation;
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
    public int rollForce;//翻滾移動的強度
    public int rollDistence;
    public LayerMask wall;
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

    void Start()
    {
        //讓角色一開始可以攻擊
        attackTime = 10;
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        stamina = staminaLimit;
        uIBarControl.SetMaxStamina(staminaLimit);
        playerOptions = GetComponent<PlayerOptions>();
        playerAction = GetComponentInChildren<PlayerAction>();
        oldPosition = transform.position;
    }
    private void FixedUpdate()//好用的東東
    {
        if (isInvincible)
        {
           return;
        }
        ws = Input.GetAxis("Vertical");//世界軸
        //GunAudio.PlayOneShot(walkSFX);
        ad = Input.GetAxis("Horizontal");
        //GunAudio.PlayOneShot(walkSFX);
        if (isAttack == true)
        {
            Movement.Set(0, 0, 0);
        }
        else if (isAttack == false)
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
        Movement.Set(0, 0, 0);
        //攻速&普攻按鍵
        attackTime += Time.deltaTime;
        if (attackTime >= attackSpeed && 
            !gameMenu.anyWindow[0].activeSelf && !gameMenu.anyWindow[2].activeSelf&& !gameMenu.anyWindow[4].activeSelf && !gameMenu.anyWindow[5].activeSelf && !gameMenu.anyWindow[6].activeSelf)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
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
            oldPosition = transform.position;
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
        if (isInvincible)
        {
            if (Physics.Raycast(oldPosition, (transform.position-oldPosition), Vector3.Distance(oldPosition ,transform.position), wall))
            {
               //transform.position = oldPosition;
                Debug.Log("穿牆");
            }
        }
        else
        {
            if (rigidbody.velocity.magnitude > moveSpeed)
            {
                rigidbody.velocity = Vector3.zero;
            }
            
        }

        if (rigidbody.velocity.magnitude > 20)
        {
            Debug.Log(rigidbody.velocity + " " + rigidbody.velocity.magnitude);
        }
    }
    public void Attack()
    {
        //false在動畫Event呼叫
        isAttack = true;
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
        oldPosition = transform.position;
        if (stamina > staminaRoll)
        {
            //歸零動量
            rigidbody.velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            stamina -= staminaRoll;
            uIBarControl.SetStamina(stamina);
            playerAction.Roll();
            //開啟無敵狀態
            isInvincible = true;
            //限制翻滾時不能轉向
            cantMove = true;
            
            //rigidbody.AddForce(-playerRotation.forward * rollForce);//不知道該決定用哪個好
            
            rigidbody.velocity = -playerRotation.forward * rollDistence;
            collider.isTrigger = true;
            rigidbody.useGravity = false;
            //PlayerCube.transform.Rotate(Vector3.right * 200);//瞬間轉到x.200度
        }
    }
    //testall += test1失敗
}
