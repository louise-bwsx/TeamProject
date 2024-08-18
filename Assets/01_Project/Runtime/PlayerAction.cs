using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    public GameObject shadowDestory;
    public Transform spawantransform;
    public Transform SpikeAttackLeftPos;
    public Transform SpikeAttackRightPos;
    public Transform playerRotation;

    private SpriteRenderer spriteRenderer;
    private GameObject spwanSwordCube;
    private PlayerControl playerControl;
    private PlayerSprite playerSprite;
    private ShootDirectionSetter shootDirectionSetter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSprite = GetComponent<PlayerSprite>();
        playerControl = GetComponentInParent<PlayerControl>();
        shootDirectionSetter = GetComponentInParent<ShootDirectionSetter>();
    }

    public void NormalAttackFX()//動畫Event呼叫
    {
        //生成攻擊範圍
        ObjectPool.Inst.SpawnFromPool("SwordAttackCube", spawantransform.position, spawantransform.rotation, duration: 0.3f);
    }

    public void NormalAttackEffect()//動畫Event呼叫
    {
        AudioManager.Inst.PlaySFX("Swing");
        if (playerRotation.localEulerAngles.y > 0 && playerRotation.localEulerAngles.y < 180)
        {
            ObjectPool.Inst.SpawnFromPool("SwingFxL", playerControl.transform.position, Quaternion.identity, duration: 0.3f);
        }
        else
        {
            ObjectPool.Inst.SpawnFromPool("SwingFxR", playerControl.transform.position, Quaternion.identity, duration: 0.3f);
        }
    }

    public void SpikeAttackFX()//動畫Event呼叫
    {
        AudioManager.Inst.PlaySFX("Spike");

        if (spriteRenderer.flipX)
        {
            //攻擊範圍
            ObjectPool.Inst.SpawnFromPool("SwordAttackCube", SpikeAttackRightPos.position, SpikeAttackRightPos.rotation, duration: 0.3f);
            //攻擊特效
            ObjectPool.Inst.SpawnFromPool("SpikeFxR", SpikeAttackRightPos.position, Quaternion.identity, duration: 0.5f);
            return;
        }
        ObjectPool.Inst.SpawnFromPool("SwordAttackCube", SpikeAttackLeftPos.position, SpikeAttackLeftPos.rotation, duration: 0.3f);
        ObjectPool.Inst.SpawnFromPool("SpikeFxL", SpikeAttackLeftPos.position, Quaternion.identity, duration: 0.3f);
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

    public void StartMoving()//動畫Event呼叫
    {
        playerControl.rigidbody.velocity = shootDirectionSetter.GetForward() * playerControl.normalAttackDash;
    }

    public void RollStop()//動畫Event呼叫
    {
        playerControl.rigidbody.velocity = Vector3.zero;
    }

    public void RollCancelAttack()//動畫Event呼叫
    {
        animator.SetBool("IsAttack", false);
        playerControl.isAttack = false;
        playerSprite.isMagicAttack = false;
    }
}