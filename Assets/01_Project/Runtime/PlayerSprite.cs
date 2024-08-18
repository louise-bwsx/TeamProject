using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    //TODOWarning: 利用IsMagicAttack去判斷能不能移動 會比用animator.GetBool來的準確 
    public bool isMagicAttack;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerControl playerControl;
    private SkillShooter skillShooter;
    private ShootDirectionSetter shootDirectionSetter;
    private void Awake()
    {
        playerControl = GetComponentInParent<PlayerControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillShooter = GetComponentInParent<SkillShooter>();
        shootDirectionSetter = GetComponentInParent<ShootDirectionSetter>();
    }

    public void SpriteFlipByInputDirection(float horizontal)
    {
        //flipX == true 時是面向右邊
        spriteRenderer.flipX = horizontal > 0;
    }

    public void SpriteFlipByMousePosition()
    {
        bool flipX = shootDirectionSetter.GetLocalEulerAnglesY() > 0 && shootDirectionSetter.GetLocalEulerAnglesY() < 180;
        spriteRenderer.flipX = flipX;
    }

    public bool FlipX()
    {
        return spriteRenderer.flipX;
    }

    void IsAttackFalse()//動畫Event控制
    {
        //為了攻擊中不能移動
        playerControl.isAttack = false;
    }

    private void MagicAtttack()//動畫Event控制
    {
        //為了攻擊中不能移動
        //不能將Event放在一開頭 不然會讀不到很詭異
        playerControl.isAttack = true;
        isMagicAttack = true;
        skillShooter.Cast();//本來想改由Cast當callBack但因為 animator在子物件 所以暫時先這樣
    }

    void SkillShoot()//動畫Event控制
    {
        //Debug.Log("SkillShoot");
        playerControl.isAttack = false;
        isMagicAttack = false;
    }
}