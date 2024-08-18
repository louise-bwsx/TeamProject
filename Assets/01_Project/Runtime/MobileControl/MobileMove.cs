using UnityEngine;

public class MobileMove : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    private float vertical;
    private float horizontal;
    private PlayerControl playerControl;
    private ShootDirectionSetter shootDirectionSetter;

    public void Init(PlayerControl playerControl, ShootDirectionSetter shootDirectionSetter)
    {
        this.playerControl = playerControl;
        this.shootDirectionSetter = shootDirectionSetter;
    }

    private void Update()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        playerControl.SetMoveDirection(vertical, horizontal);
        if (vertical == 0 && horizontal == 0)
        {
            return;
        }
        float targetYAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg;
        shootDirectionSetter.MobileShootDirectionChange(targetYAngle);
    }
}