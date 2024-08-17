using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Transform faceDirection;
    public PlayerControl playerControl;
    public bool isMagicAttack;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SkillShooter skillShooter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillShooter = GetComponentInParent<SkillShooter>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //if (isMagicAttack)
        //{
        //    PlayerSpriteFlip();
        //}
        if (playerControl.isAttack == false &&
            !animator.GetBool("IsAttack") &&
            inputDir.x != 0)
        {
            float y = inputDir.x > 0 ? 90 : -90;
            faceDirection.localEulerAngles = faceDirection.localEulerAngles.With(y: y);
            spriteRenderer.flipX = inputDir.x > 0;
        }
    }

    public void PlayerSpriteFlip()
    {
        if (faceDirection.localEulerAngles.y < 180 && faceDirection.localEulerAngles.y > 0)
        {
            //Debug.Log("面向右邊");
            faceDirection.localEulerAngles = faceDirection.localEulerAngles.With(y: 90);
            spriteRenderer.flipX = true;
        }
        else if (faceDirection.localEulerAngles.y < 360 && faceDirection.localEulerAngles.y > 180)
        {
            faceDirection.localEulerAngles = faceDirection.localEulerAngles.With(y: -90);
            spriteRenderer.flipX = false;
        }
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
        //animator.SetTrigger("Magic");
        playerControl.isAttack = false;
        isMagicAttack = false;
    }
}