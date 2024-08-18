using UnityEngine;
using UnityEngine.UI;

public class MobileRoll : MonoBehaviour
{
    [SerializeField] private Button dodgeBtn;

    public void Init(PlayerControl playerControl)
    {
        dodgeBtn.onClick.RemoveAllListeners();
        dodgeBtn.onClick.AddListener(playerControl.Roll);
    }
}