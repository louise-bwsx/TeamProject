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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance, floor))
        {
            mousePosition = hit.point;
            //取得方向 但不修改Y軸
            Vector3 direction = new Vector3(mousePosition.x, shootDirectionTrans.position.y, mousePosition.z) - shootDirectionTrans.position;
            // Set the rotation of shootDirectionTrans to look at the mouse position, only modifying the X and Z axes
            shootDirectionTrans.rotation = Quaternion.LookRotation(direction);
        }
    }
}
