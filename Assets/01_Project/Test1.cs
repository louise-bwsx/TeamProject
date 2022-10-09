using UnityEngine;
using UnityEngine.EventSystems;

public class Test1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string aaa;
    public string bbb;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointExit");
    }
}