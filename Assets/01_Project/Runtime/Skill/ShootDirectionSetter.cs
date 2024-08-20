using UnityEngine;
using UnityEngine.EventSystems;

public class ShootDirectionSetter : MonoBehaviour
{
    private int floor;
    private PlayerStats playerStats;
    private RaycastHit hit;
    private float rayDistance = 500f;
    [SerializeField] private Transform shootDirectionTrans;
    [SerializeField] private Transform mouseTransform;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (playerStats.IsDead())
        {
            return;
        }

        //當手機模式時 用左邊搖桿來改變shootDirection
        if (GameStateManager.Inst.IsMobile)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, floor))
        {
            //讓他比Tatami高 不要再Tatami設collider 會抖
            mouseTransform.position = hit.point.With(y: hit.point.y + .2f);
            //取得方向 但不修改Y軸
            Vector3 direction = new Vector3(hit.point.x, shootDirectionTrans.position.y, hit.point.z) - shootDirectionTrans.position;
            shootDirectionTrans.rotation = Quaternion.LookRotation(direction);
        }
    }

    public Vector3 GetForward()
    {
        return shootDirectionTrans.forward;
    }

    public float GetLocalEulerAnglesY()
    {
        return shootDirectionTrans.localEulerAngles.y;
    }

    public void MobileShootDirectionChange(float targetYAngle)
    {
        shootDirectionTrans.rotation = Quaternion.Euler(0, targetYAngle, 0);
    }
}