using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDirection : MonoBehaviour
{
    public MobileMove mobileMove;
    void Start()
    {
        mobileMove = FindObjectOfType<MobileMove>();    
    }
    void Update()
    {
        if (mobileMove.ws > 0 && mobileMove.ad > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90+45));
        }
        else if (mobileMove.ws > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -90);
        }
        else if (mobileMove.ad > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        if (mobileMove.ws < 0 && mobileMove.ad > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90 + 135));
        }
        else if (mobileMove.ws < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (-90 + 180));
        }
        if (mobileMove.ws < 0 && mobileMove.ad < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (90+45));
        }
        else if (mobileMove.ad < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        if (mobileMove.ws > 0 && mobileMove.ad < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * (90 + 135));
        }
    }
}
