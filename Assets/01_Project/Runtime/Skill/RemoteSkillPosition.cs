using UnityEngine;

public class RemoteSkillPosition : MonoBehaviour
{
    private FixedJoystick joystick;
    private RectTransform handle;
    //不要用handle的position取代 不想要因為按下去後 因為handle的移動 有誤差
    private RectTransform joystickBorder;
    [SerializeField] private MeshRenderer mousePosition;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private float skillControlSpeed;
    [SerializeField] private float maxDistence;
    [SerializeField] private LayerMask floor;
    private bool isClick;
    private RaycastHit hit;
    private Vector3 offset;
    private Vector3 magnitude;
    private float vertical;
    private float horizontal;

    private void Update()
    {
        if (!isClick || handle == null)
        {
            return;
        }
        ChangeShootDirection();
        ChangeCastPosition();
    }

    public void SkillBtnOnClick(FixedJoystick joystick, RectTransform handle, RectTransform joystickBorder, bool isDirectionalTypeSkill)
    {
        mousePosition.transform.position = transform.position;
        this.joystick = joystick;
        this.handle = handle;
        this.joystickBorder = joystickBorder;
        mousePosition.enabled = !isDirectionalTypeSkill;
        shootDirection.gameObject.SetActive(isDirectionalTypeSkill);
        isClick = true;
    }

    public void SkillBtnOnRelease()
    {
        isClick = false;
        mousePosition.enabled = false;
        shootDirection.gameObject.SetActive(false);
    }

    private void ChangeShootDirection()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        float targetYAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg;
        shootDirection.transform.rotation = Quaternion.Euler(0, targetYAngle, 0);
    }

    private void ChangeCastPosition()
    {
        Vector3 offset = joystickBorder.position - handle.position;
        offset.z = offset.y; // 因為搖桿是 Vector2，所以將 Y 軸轉換為 Z 軸
        magnitude = Vector3.ClampMagnitude(offset, 1f);

        Vector3 movement = magnitude * (-1f * skillControlSpeed * Time.deltaTime);

        mousePosition.transform.Translate(movement);

        //有了AI還是沒辦法 限制最大距離 都會卡住 或是超出去
        // 如果射線打到地板，鎖定位置在地表上
        if (Physics.Raycast(mousePosition.transform.position + Vector3.up * 5f, Vector3.down, out hit, 10f, floor))
        {
            mousePosition.transform.position = hit.point + Vector3.up * 0.1f;
        }
    }
}