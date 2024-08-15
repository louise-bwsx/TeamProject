using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    //public UIBarControl uIBarControl;
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
    SpriteRenderer spriteRenderer;
    GameObject spwanSwordCube;
    public GameMenuController gameMenu;
    public Transform playerRotation;

    private PlayerControl playerControl;
    public PlayerSprite playerFaceDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
        gameMenu = FindObjectOfType<GameMenuController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
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

    //TODOError: 施法動畫被分段了 本來Magic 被分成Magic_Prepare Magic_Shoot Callback 也沒設定
    //TODOWarning: Roll動畫多了一個Event RollStop不確定是否正確
    public void SpikeAttack()//動畫Event呼叫
    {
        animator.SetTrigger("Attack_Spike");
        //spawantransform.GetComponent<Collider>().enabled = true;
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
        gameMenu.OpenMenu("DiePanel");
        Time.timeScale = 0;
    }

    public void StartMoving()
    {
        playerControl.rigidbody.velocity = playerFaceDirection.faceDirection.forward * playerControl.normalAttackDash;
    }

    public void StopMoveing()
    {
        playerControl.rigidbody.velocity = Vector3.zero;
    }

    public void RollCancelAttack()
    {
        animator.SetBool("IsAttack", false);
        playerControl.isAttack = false;
        playerFaceDirection.isMagicAttack = false;
    }
}