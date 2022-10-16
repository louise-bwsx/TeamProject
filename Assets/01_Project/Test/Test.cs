using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LayerMask floor;
    [SerializeField] private float distance;

    private void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position - transform.up * distance, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, distance, floor))
        {
            transform.position = hit.point + transform.up * 0.5f;
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