using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        if (!Helper.Camera)
        {
            return;
        }
        transform.LookAt(transform.position + Helper.Camera.transform.forward);
    }
}