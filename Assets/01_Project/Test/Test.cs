using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LayerMask floor;
    [SerializeField] private float distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;

    private void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();

        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position - transform.up * distance, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, distance, floor))
        {
            //還是卡 但沒有卡到完全動不了
            Vector3 hitLift = hit.point + transform.up * 0.5f;
            //Debug.Log(hitLift);
            Vector3 originPos = transform.position;
            originPos.y = hitLift.y;
            transform.position = originPos;
            //transform.position = hit.point + transform.up * 0.5f;
            Debug.Log("回到地板上");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));
        //Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));
    }
}