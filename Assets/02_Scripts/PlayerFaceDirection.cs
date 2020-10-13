using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    public Transform playerRotation;
    void Update()
    {
        if (playerControl.cantMove == false && Time.timeScale != 0)
        {
            if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
            {
                transform.rotation = Quaternion.Euler(-30, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(30, 270, 0);
            }
        }
    }
}
