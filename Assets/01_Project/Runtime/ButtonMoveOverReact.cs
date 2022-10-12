using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoveOverReact : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("ButtonClick");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("ButtonClick");
    }
}
