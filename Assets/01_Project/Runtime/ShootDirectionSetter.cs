using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootDirectionSetter : MonoBehaviour
{
    private int floor;
    private PlayerStats playerStats;
    private RaycastHit hit;
    private float rayDistance = 500f;
    [SerializeField, ReadOnly] private Vector3 mousePosition;
    [SerializeField] private Transform shootDirectionTrans;

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
        if (playerStats.hp <= 0)
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
            mousePosition = hit.point;
            //取得方向 但不修改Y軸
            Vector3 direction = new Vector3(mousePosition.x, shootDirectionTrans.position.y, mousePosition.z) - shootDirectionTrans.position;
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