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
    //有關移動
    public int moveSpeed = 5;//移動速度
    public bool isInvincible = false;//無敵狀態
    //有關攻擊
    public bool isAttack = false;
    public float attackRange = 0.4f;
    public float attackTime;
    public float attackSpeed;//同動畫時間
    public float attackSpikeSpeed;//突刺攻擊間隔
    public int attackDamage = 2;
    //public int spikeAttackDash = 7;
    //public int normalAttackDash = 3;
    public LayerMask EnemyLayer;
    public float lastFireTime;
    public float fireRate=1f;
    public Animator animator;
    public bool isRoll;
    public FixedJoystick leftJoyStick;

    void Start()
    {
        //讓角色一開始可以攻擊
        attackTime = 10;
        //Invoke("Roll", 5);開始遊戲後五秒施放翻滾
        playerAction = GetComponentInChildren<PlayerAction>();
        collider = GetComponentInParent<Collider>();
        playerOptions = FindObjectOfType<PlayerOptions>();
    }

    [System.Obsolete]
    void Update()
    {
        //moveMent.Set(0, 0, 0);
        //攻速&普攻按鍵
        attackTime += Time.deltaTime;
        if (attackTime >= attackSpeed && 
            !gameMenu.anyWindow[0].activeSelf && !gameMenu.anyWindow[2].activeSelf&& !gameMenu.anyWindow[4].activeSelf && !gameMenu.anyWindow[5].activeSelf && !gameMenu.anyWindow[6].activeSelf &&
            !playerFaceDirection.isMagicAttack && getHitEffect.playerHealth>0 && !isRoll)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                //Attack();
                //attackTime = 0;//另外一種計時方式
            }
            if (Input.GetMouseButtonDown(1) && attackTime >= attackSpikeSpeed)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                animator.SetTrigger("Attack_Spike");
                //Attack();
                //attackTime = 0;//另外一種計時方式
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
            Debug.Log(SavePosition);
            healthbarongame.SetHealth(data.Playerhealth);//人物身上的血條
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.N))//傳送到指定地點 &場景重置
        {
            transform.position = new Vector3(-6.31f, 0, 6.08f);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    //public void Attack()
    //{
    //    //false在動畫Event呼叫
    //    isAttack = true;
    //    //讓人物轉向 測試人物轉向暫時關閉
    //    playerFaceDirection.PlayerSpriteFlip();
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //playerOptions.GetComponent<Transform>().position += playerRotation.forward;
    //        //rigidbody.velocity = playerRotation.forward * normalAttackDash;
    //        //playerAction.NormalAttack();
    //    }
    //    else if (Input.GetMouseButtonDown(1))
    //    {
    //        rigidbody.velocity = playerRotation.forward * spikeAttackDash;
    //        //playerAction.SpikeAttack();
    //    }
    //}
}
