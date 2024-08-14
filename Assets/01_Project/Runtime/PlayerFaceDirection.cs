using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public Transform playerRotation;
    public PlayerControl playerControl;
    public SkillSelect skillControl;
    public bool isMagicAttack;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        skillControl = FindObjectOfType<SkillSelect>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (isMagicAttack)
        {
            PlayerSpriteFlip();
        }
        if (playerControl.isAttack == false &&
            !animator.GetBool("IsAttack") &&
            inputDir.x != 0)
        {
            float y = inputDir.x > 0 ? 90 : -90;
            playerRotation.localEulerAngles = playerRotation.localEulerAngles.With(y: y);
            spriteRenderer.flipX = inputDir.x > 0;
        }
    }

    public void PlayerSpriteFlip()
    {
        if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
        {
            //Debug.Log("面向右邊");
            playerRotation.localEulerAngles = playerRotation.localEulerAngles.With(y: 90);
            spriteRenderer.flipX = true;
        }
        else if (playerRotation.localEulerAngles.y < 360 && playerRotation.localEulerAngles.y > 180)
        {
            playerRotation.localEulerAngles = playerRotation.localEulerAngles.With(y: -90);
            spriteRenderer.flipX = false;
        }
    }

    void IsAttackFalse()//動畫Event控制
    {
        //為了攻擊中不能移動
        playerControl.isAttack = false;
    }

    void IsMagicAtttack()//動畫Event控制
    {
        //為了攻擊中不能移動
        //不能放在一開頭 不然會讀不到很詭異
        playerControl.isAttack = true;
        isMagicAttack = true;
    }

    void SkillShoot()//動畫Event控制
    {
        skillControl.SkillShoot();
        playerControl.isAttack = false;
        isMagicAttack = false;
    }
}
