using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    public LookAtCursor lookAtCursor;
    void Update()
    {
        if (playerControl.cantMove == false)
        { 
            if (lookAtCursor.rotationangle.y > 0)
            {
                transform.rotation = Quaternion.Euler(-30, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(30, 0, 0);
            }        
        }
    }
}
