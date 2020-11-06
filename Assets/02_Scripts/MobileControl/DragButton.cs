using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour , IDragHandler
{
    //背景圖片
    public RectTransform background = null;
    //搖桿主體
    public RectTransform handle = null;
    public Canvas canvas;
    public Camera cam;
    public Vector2 input = Vector2.zero;
    public AxisOptions axisOptions = AxisOptions.Both;
    public float deadZone;
    //public float handle
    public float DeadZone 
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }
    public AxisOptions AxisOption 
    {
        get { return AxisOption; } 
        set { AxisOption = value; } 
    }
    //public float HandleRange
    //{
    //    //get { return han}
    //}
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        DeadZone = deadZone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //搖桿拖曳
    public void OnDrag(PointerEventData pointerEventData)
    {
        //設定相機為Canvas面對的主攝影機
        cam = canvas.worldCamera;
        //宣告一個Vector2的位置為
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (pointerEventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandlerInput(input.magnitude, input.normalized, radius, cam);
        //handle.anchoredPosition = input * radius *
    }
    //當玩家拖曳的時候如果碰到邊界該如何反應
    public void HandlerInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }
    //中間的搖桿實際移動到哪裡
    public void FormatInput()
    {
        if (AxisOption == AxisOptions.Horizontal)
            input = new Vector2(input.x, 0f);
        else if (AxisOption == AxisOptions.Vertical)
            input = new Vector2(0f, input.y);
    }
}
