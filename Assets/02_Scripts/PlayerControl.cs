using UnityEngine.EventSystems;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    PlayerOptions playerOptions;
    GetHitEffect getHitEffect;
    public UIBarControl uIBarControl;//刪掉了懶得復原
    public HealthBarOnGame healthbarongame;
    public Animator PlayerCube;
    public GameObject[] toolall = new GameObject[6];//每個欄位都要塞東西 不然會報錯不給你用
    public GameObject BackPackUI;
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
    float rollTime = 0f;//被存入的時間
    public float rollTimeLimit = 0.4f;//翻滾的無敵時間
    //有關攻擊
    public Transform AttackPoint;
    public float attackRange = 0.4f;
    public float attackSpeed = 2;
    public int attackDamage = 20;
    float attackTime = 0;
    public LayerMask EnemyLayer;

    void Start()
    {
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        stamina = staminaLimit;
        uIBarControl.SetMaxStamina(staminaLimit);
        playerOptions = GetComponent<PlayerOptions>();
        getHitEffect = GetComponent<GetHitEffect>();
    }
    private void FixedUpdate()//好用的東東
    {
        ws = Input.GetAxis("Vertical");
        ad = Input.GetAxis("Horizontal");
        Movement.Set(ad, 0f, ws);
        //如果有Movement.normalized會延遲很嚴重 因為四捨五入?
        Movement = Movement * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + Movement);
    }

    [System.Obsolete]
    void Update()
    {
        #region 數字鍵一到五
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Toolchandge(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Toolchandge(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Toolchandge(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Toolchandge(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Toolchandge(5);
        }
        #endregion
        //攻速
        if (Time.time >= attackTime)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && !PlayerCube.GetBool("IsAttack"))
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            BackPackUI.SetActive(!BackPackUI.activeSelf);
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
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        //翻滾
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
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
    void Toolchandge(int toolnum)//只能取自己身上的gameobject 不能取prefab
    {
        if (toolall[toolnum].activeSelf == false)
        {
            for (int i = 0; i < toolall.Length; i++)
            {
                toolall[i].SetActive(false);
            }
            toolall[toolnum].SetActive(true);
        }
        else
        {
            toolall[toolnum].SetActive(false);
        }
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && toolall[4] == true)
        {
            PlayerCube.SetTrigger("SwordAttack");
            //他只會抓這個方法啟動瞬間的範圍
            Collider[] hitEnemy = Physics.OverlapSphere(AttackPoint.position, attackRange, EnemyLayer);
            foreach (Collider enemy in hitEnemy)
            {
                Debug.Log(enemy.name);
                enemy.GetComponent<MonsterHealth>()?.GetHit(attackDamage);
            }
        }
        if (Input.GetMouseButtonDown(1) && toolall[4] == true)
        {
            PlayerCube.SetTrigger("SwordAttackSpike");
        }
    }
    void OnDrawGizmosSelected()
    {
        //畫一條中心線是AttackPoint的圓半徑是attackRange
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
    void Roll()
    {
        if (getHitEffect.getHit == false && rollTime <= 0 && stamina >= staminaRoll)
        {
            //歸零動量
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //耐力條 -= 損失耐力
            stamina -= staminaRoll;
            uIBarControl.SetStamina(stamina);
            //開啟無敵狀態
            rollInvincible = true;
            //限制翻滾時不能轉向
            cantMove = true;
            GetComponent<Rigidbody>().velocity = playerRotation.forward * rollDistence;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
        //PlayerCube.transform.Rotate(Vector3.right * 200);//瞬間轉到x.200度
    }

    //testall += test1失敗
}
