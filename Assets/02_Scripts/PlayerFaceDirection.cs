using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    void Update()
    {
        if (playerControl.cantMove == false)
        { 
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
