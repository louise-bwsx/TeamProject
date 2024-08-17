using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    public GameObject swordCube;
    public GameObject swingAttackEffectLeft;
    public GameObject swingAttackEffectRight;
    public GameObject SpikeAttackEffectLeft;
    public GameObject SpikeAttackEffectRight;
    public GameObject shadowDestory;
    public Transform player;
    public Transform spawantransform;
    public Transform SpikeAttackLeftPos;
    public Transform SpikeAttackRightPos;
    public GameMenuController gameMenu;
    public Transform playerRotation;

    private SpriteRenderer spriteRenderer;
    private GameObject spwanSwordCube;
    private PlayerControl playerControl;
    private PlayerSprite playerSprite;
    private float horizontal;
    private float vertical;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
        gameMenu = FindObjectOfType<GameMenuController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSprite = GetComponent<PlayerSprite>();
    }

    private void Update()
    {
        //避免先按住左 再按住右會播放移動動畫
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

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
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
    }

    public void Roll()
    {
        animator.SetTrigger("Roll");
        AudioManager.Inst.PlaySFX("Roll");
    }

    public void NormalAttack()
    {
        //動畫
        animator.SetTrigger("Attack");
    }

    public void NormalAttackFX()//動畫Event呼叫
    {
        //生成攻擊範圍
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        Destroy(spwanSwordCube, 0.3f);
    }

    public void NormalAttackEffect()
    {
        AudioManager.Inst.PlaySFX("Swing");
        //特效
        GameObject FX;
        if (playerRotation.localEulerAngles.y > 0 && playerRotation.localEulerAngles.y < 180)
        {
            FX = Instantiate(swingAttackEffectLeft, player);
            Destroy(FX, 0.3f);
        }
        else
        {
            FX = Instantiate(swingAttackEffectRight, player);
            Destroy(FX, 0.3f);
        }
    }

    public void SpikeAttack()//動畫Event呼叫
    {
        animator.SetTrigger("Attack_Spike");
    }

    public void SpikeAttackFX()//動畫Event呼叫
    {
        AudioManager.Inst.PlaySFX("Spike");
        GameObject FX;
        //攻擊範圍
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        //攻擊特效
        if (spriteRenderer.flipX)
        {
            FX = Instantiate(SpikeAttackEffectLeft, SpikeAttackLeftPos.position, SpikeAttackLeftPos.rotation);
            Destroy(FX, 0.5f);
        }
        else if (!spriteRenderer.flipX == true)
        {
            FX = Instantiate(SpikeAttackEffectRight, SpikeAttackRightPos.position, SpikeAttackRightPos.rotation);
            Destroy(FX, 0.5f);
        }
        Destroy(spwanSwordCube, 0.3f);
    }

    public void DestroySword()//動畫Event呼叫
    {
        //刪除攻擊範圍
        if (spwanSwordCube != null)
        {
            Destroy(spwanSwordCube);
        }
        spawantransform.GetComponent<Collider>().enabled = false;
    }

    public void Die()//動畫Event呼叫
    {
        shadowDestory.SetActive(false);
        AudioManager.Inst.PlayBGM("Dead");
        UIManager.Inst.OpenMenu("DiePanel");
        Time.timeScale = 0;
    }

    public void StartMoving()
    {
        playerControl.rigidbody.velocity = playerSprite.faceDirection.forward * playerControl.normalAttackDash;
    }

    public void StopMoveing()
    {
        playerControl.rigidbody.velocity = Vector3.zero;
    }

    public void RollCancelAttack()
    {
        animator.SetBool("IsAttack", false);
        playerControl.isAttack = false;
        playerSprite.isMagicAttack = false;
    }
}