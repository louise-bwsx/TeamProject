using UnityEngine;

public class RollDirection : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90 + 45));
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -90);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90 + 135));
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90 + 180));
        }
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (90 + 45));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (90 + 135));
        }
    }
}
