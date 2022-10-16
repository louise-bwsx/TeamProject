using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// https://www.youtube.com/watch?v=JOABOQMurZo&ab_channel=Tarodev
public static class Helper
{
    private static Camera camera;
    /// <summary>
    /// Camera.main以前比較耗資源 所以要儲存起來<para></para>
    /// </summary>
    public static Camera Camera
    {
        get
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            return camera;
        }
    }

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    /// <summary>
    /// Non-allocating WaitForSeconds <para></para>
    /// 如果for迴圈裡面用WaitForSeconds 並且 給的時間相同 <para></para>
    /// 就不用一直new出來比較省資源
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait))
        {
            return wait;
        }
        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    private static PointerEventData eventDataCurrentPostition;
    private static List<RaycastResult> results;
    /// <summary>
    /// 目前游標是否在UI之上<para></para>
    /// 好像跟EventSystem.current.IsPointerOverGameObject()的功能一樣
    /// </summary>
    /// <returns></returns>
    public static bool IsOverUI()
    {
        eventDataCurrentPostition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPostition, results);
        return results.Count > 0;
    }

    /// <summary>
    /// Find world point of canvas element<para></para>
    /// 指定的UI物件座標轉換成2D世界座標 
    /// </summary>
    /// <param name="element">指定的UI物件</param>
    /// <returns></returns>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var worldPoint);
        return worldPoint;
    }

    /// <summary>
    /// 修改ScrollRect的Content高度<para></para>
    /// 使Content在滑動的時候能完美契合<para></para>
    /// 適合不常更新的如容器UI
    /// </summary>
    /// <param name="content">增加的Content</param>
    /// <param name="cellSize">GridLayoutGroup的CellSize</param>
    /// <param name="spaceingY">GridLayoutGroup的SpacingY</param>
    /// <param name="addThreshold">每超過多少的倍數就需要增加一行</param>

    public static void ChangeScrollRectContentSize(RectTransform content, float cellSize, float spaceingY, int addThreshold = 0)
    {
        Vector2 currSize = Vector2.zero;
        int childCount = content.childCount;
        int quotient = childCount / addThreshold; //17/4 = 4, 3/4 = 0
        int remainder = childCount % addThreshold;//17%4 = 1, 3%4 = 3
        if ((quotient > 0 && remainder > 0) ||
            remainder > 0)
        {
            quotient++;
        }
        for (int i = 0; i < quotient; i++)
        {
            currSize.y += cellSize;
            currSize.y += spaceingY;
        }
        content.sizeDelta = currSize;
    }
    /// <summary>
    /// 修改ScrollRect的Content高度<para></para>
    /// 使Content在滑動的時候能完美契合<para></para>
    /// 適合常常更新的如房間人數
    /// </summary>
    /// <param name="isAdd"></param>
    /// <param name="content"></param>
    public static void ChangeScrollRectContentSize(bool isAdd, RectTransform content, int cellSize, int spaceingY)
    {
        Vector2 currSize = content.sizeDelta;
        if (isAdd)
        {
            currSize.y += cellSize;//Cell Size
            currSize.y += spaceingY;//Spacing Y
        }
        else
        {
            currSize.y -= cellSize;//Cell Size
            currSize.y -= spaceingY;//Spacing Y
        }
        content.sizeDelta = currSize;
    }
}