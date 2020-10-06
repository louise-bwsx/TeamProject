using UnityEngine.EventSystems;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    PlayerOptions playerOptions;
    PlayerAction playerAction;
    public UIBarControl uIBarControl;
    public GameMenu gameMenu;
    public HealthBarOnGame healthbarongame;
    public GameObject backPackUI;
    public GameObject skillUI;
    public Transform playerRotation;
    public EquipmentManager equipmentManager;
    //有關耐力
    float stamina;
    float staminaLimit = 100;
    float staminaRoll = 10;
    //有關移動
    public int moveSpeed = 5;//移動速度
    public int rollDistence = 10;//翻滾移動距離
    public bool rollInvincible = false;//無敵狀態
    public bool cantMove = false;//移動限制
    float ws;
    float ad;
    Vector3 Movement;
    //有關翻滾
    public float rollTime = 0f;//被存入的時間
    public float rollTimeLimit = 0.4f;//翻滾的無敵時間
    //有關攻擊
    public float attackRange = 0.4f;
    public float attackSpeed = 2;
    public int attackDamage = 20;
    float attackTime = 0;
    public Transform AttackPoint;
    public LayerMask EnemyLayer;

    public AudioSource GunAudio;//音樂放置
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效

    void Start()
    {
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        stamina = staminaLimit;
        uIBarControl.SetMaxStamina(staminaLimit);
        playerOptions = GetComponent<PlayerOptions>();
        playerAction = GetComponentInChildren<PlayerAction>();
    }
    private void FixedUpdate()//好用的東東
    {
        ws = Input.GetAxis("Vertical");
        //GunAudio.PlayOneShot(walkSFX);
        ad = Input.GetAxis("Horizontal");
        //GunAudio.PlayOneShot(walkSFX);
        Movement.Set(ad, 0f, ws);
        //如果有Movement.normalized會延遲很嚴重 因為四捨五入?
        Movement = Movement * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + Movement);
    }

    [System.Obsolete]
    void Update()
    {
        //攻速&普攻按鍵
        if (Time.time >= attackTime && 
            !gameMenu.anyWindow[0].activeSelf && !gameMenu.anyWindow[1].activeSelf&& !gameMenu.anyWindow[2].activeSelf)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)/* && !PlayerCube.GetBool("IsAttack")*/)
            {
                Attack();
                attackTime = Time.time + 1f / attackSpeed;//另外一種計時方式
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

        //好像沒有任何作用
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //翻滾
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
            GunAudio.PlayOneShot(TurnOverSFX);
        }
        if (rollInvincible)
        {
            rollTime += Time.deltaTime;
        }
        //無敵時間超過上限取消無敵狀態
        if (rollTime > rollTimeLimit)
        {
            rollInvincible = false;
            cantMove = false;
            rollTime = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;
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
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GunAudio.PlayOneShot(SpikeSFX);
            playerAction.NormalAttack();
            //他只會抓這個方法啟動瞬間的範圍
            Collider[] hitEnemy = Physics.OverlapSphere(AttackPoint.position, attackRange, EnemyLayer);
            foreach (Collider enemy in hitEnemy)
            {
                Debug.Log(enemy.name);
                enemy.GetComponent<MonsterHealth>()?.GetHit(attackDamage);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GunAudio.PlayOneShot(SwingSFX);
            playerAction.SpikeAttack();
        }
    }
    void OnDrawGizmosSelected()
    {
        //畫一條中心線是AttackPoint的圓半徑是attackRange
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
    void Roll()
    {
        if (stamina > staminaRoll)
        { 
            //歸零動量
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            stamina -= staminaRoll;
            uIBarControl.SetStamina(stamina);
            playerAction.Roll();
            //開啟無敵狀態
            rollInvincible = true;
            //限制翻滾時不能轉向
            cantMove = true;
            GetComponent<Rigidbody>().velocity = -playerRotation.forward * rollDistence;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
            //PlayerCube.transform.Rotate(Vector3.right * 200);//瞬間轉到x.200度
        }
    }
    //testall += test1失敗
}
