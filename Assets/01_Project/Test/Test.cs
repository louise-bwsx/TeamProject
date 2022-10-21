using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LayerMask floor;
    [SerializeField] private float distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;

    //不會因為Destroy(null)報錯
    //[SerializeField] private GameObject aa;
    //private void Start()
    //{
    //    StartCoroutine(Testing());
    //    Destroy(aa);
    //}
    //private IEnumerator Testing()
    //{
    //    yield return new WaitForSeconds(2);
    //    Destroy(aa);
    //    Debug.Log(11);
    //}

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
            Vector3 hitLift = hit.point + transform.up * 0.5f;
            Vector3 originPos = transform.position;
            originPos.y = hitLift.y;
            transform.position = originPos;
            //Debug.Log("回到地板上");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));
        //Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));
    }
}