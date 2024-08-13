using UnityEngine;

public class RemoteSkillPosition : MonoBehaviour
{
    public float skillControlSpeed;
    public Transform playerSprite;
    public Transform mousePosition;
    public RectTransform hadle;
    public RectTransform outerCircle;
    public MeshRenderer meshRenderer;
    public Animator animator;
    public bool isTouch;
    public MobileSkillChoose mobileSkillChoose;

    int floor;
    Vector3 originPosition;

    void Start()
    {
        mobileSkillChoose = FindObjectOfType<MobileSkillChoose>();
        floor = LayerMask.GetMask("Floor");
    }
    void FixedUpdate()
    {
        //範圍鎖定失敗
        if (isTouch /*&& Vector3.Distance(transform.position,playerSprite.position)< maxDistence*/)
        {
            animator.SetBool("IsCheck", !isTouch);
            //meshRenderer.enabled = isTouch;
            //用一個Vector3把搖桿距離中心點的位置給存起來
            Vector3 offset = outerCircle.position - hadle.position;
            //因為搖桿是Vector2所以要把它的Y給Z
            offset.z = offset.y;
            //算距離?
            Vector3 direction = Vector3.ClampMagnitude(offset, 1f);
            //技能施放位置移動
            mousePosition.Translate(direction * -1f * skillControlSpeed * Time.deltaTime);
            RaycastHit hit;
            //技能施放位置朝地板打射線如果碰到地板
            if (Physics.Raycast(mousePosition.position, -mousePosition.up, out hit, 10f, floor))
            {
                //先存起來等不小心跑進地板再拿出來用
                originPosition = mousePosition.position;
                //技能位置等於射線打到的點朝上0.1f
                mousePosition.position = hit.point + mousePosition.up * 0.1f;
            }
            //如果潛入地下就回到還沒回到地下以前的點
            else
            {
                mousePosition.position = originPosition;
            }
        }
    }
    public void SetSkillPosition(bool isTouch)
    {
        if (mobileSkillChoose.skillList[0].CanShoot())
        {
            meshRenderer.enabled = false;
        }
        else
        {
            meshRenderer.enabled = true;
        }
        //為了讓每次按下技能時確保從角色底下出現而不是上一個施放位置
        if (isTouch)
        {
            mousePosition.position = mousePosition.parent.position;
        }
        this.isTouch = isTouch;
    }
}
