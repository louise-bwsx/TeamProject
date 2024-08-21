using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    [SerializeField] private Transform SpikeAttackLeftPos;
    [SerializeField] private Transform SpikeAttackRightPos;
    [SerializeField] private Collider attackRange;
    private Animator animator;
    private PlayerControl playerControl;
    private PlayerSprite playerSprite;
    private ShootDirectionSetter shootDirectionSetter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<PlayerSprite>();
        playerControl = GetComponentInParent<PlayerControl>();
        shootDirectionSetter = GetComponentInParent<ShootDirectionSetter>();
    }

    public void NormalAttackFX()//動畫Event呼叫
    {
        attackRange.enabled = true;
    }

    public void NormalAttackEffect()//動畫Event呼叫
    {
        AudioManager.Inst.PlaySFX("Swing");
        if (playerSprite.FlipX())
        {
            ObjectPool.Inst.SpawnFromPool("SwingFxR", playerControl.transform.position, Quaternion.identity, null, 0.3f);
            return;
        }
        ObjectPool.Inst.SpawnFromPool("SwingFxL", playerControl.transform.position, Quaternion.identity, null, 0.3f);
    }

    public void SpikeAttackFX()//動畫Event呼叫
    {
        AudioManager.Inst.PlaySFX("Spike");

        attackRange.enabled = true;
        if (playerSprite.FlipX())
        {
            //攻擊特效
            ObjectPool.Inst.SpawnFromPool("SpikeFxR", SpikeAttackRightPos.position, Quaternion.identity, null, 0.5f);
            return;
        }
        ObjectPool.Inst.SpawnFromPool("SpikeFxL", SpikeAttackLeftPos.position, Quaternion.identity, null, 0.3f);
    }

    public void DestroySword()//動畫Event呼叫
    {
        //刪除攻擊範圍
        attackRange.enabled = false;
    }

    public void Die()//動畫Event呼叫
    {
        shadow.SetActive(false);
        AudioManager.Inst.PlayBGM("Dead");
        UIManager.Inst.OpenMenu("DiePanel");

        Time.timeScale = 0;
    }

    public void StartMoving()//動畫Event呼叫
    {
        playerControl.rigidbody.velocity = shootDirectionSetter.GetForward() * playerControl.normalAttackDash;
    }

    public void ResetVelocity()//動畫Event呼叫
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