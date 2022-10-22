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
        Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - 10000));
    }
    //Gizmos.DrawLine(transform.position, transform.localPosition.With(y: transform.localPosition.y - distance));


    private Vector3 previousPos = Vector3.zero;
    private bool isGround;
    private void CalculateFallDamage()
    {
        if (previousPos == Vector3.zero)
        {
            return;
        }
        float result = previousPos.y - transform.position.y;
        Debug.Log(result);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10000, LayerMask.GetMask("Ground")))
        {
            Debug.Log("hit.point: " + hit.point);
            Debug.Log("hit.normal: " + hit.normal);
        }
        if (result > 5)
        {
            Debug.Log("承受掉落傷害");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            CalculateFallDamage();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            previousPos = transform.position;
        }
    }
}