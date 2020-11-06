using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUI : MonoBehaviour
{
    public void UIEnable(Transform typeUI)
    {
        typeUI.gameObject.SetActive(!typeUI.gameObject.activeSelf);
    }
}
