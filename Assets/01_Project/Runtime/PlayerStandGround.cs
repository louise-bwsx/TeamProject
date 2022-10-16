using UnityEngine;

public class PlayerStandGround : MonoBehaviour
{
    [SerializeField] private LayerMask floor;
    [SerializeField] private float distance;
    [SerializeField] private float lift;

    private void LateUpdate()
    {
        RaycastHit hit;
        //沒有transform.down 負數的選項
        if (Physics.Raycast(transform.position, -transform.up, out hit, distance, floor))
        {
            //還是卡 但沒有卡到完全動不了
            Vector3 hitLift = hit.point + transform.up * lift;
            Vector3 originPos = transform.position;
            originPos.y = hitLift.y;
            transform.position = originPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));
    }
}