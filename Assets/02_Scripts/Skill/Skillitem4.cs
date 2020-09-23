using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skillitem4 : MonoBehaviour
{
    public float ColdTime = 2;//技能CD時間 
    public float Timer = 0;//技能CD時間初始值
    public Image FilledImage;
    public bool IsStartTimer;//是否開始計算時間
    // Start is called before the first frame update
    void Start()
    {
        FilledImage = transform.Find("FilledSkillFive").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            IsStartTimer = true;
     
        }
        if (IsStartTimer)
        {
            Timer += Time.deltaTime;
            FilledImage.fillAmount = (ColdTime - Timer) / ColdTime;
        }
        if (Timer >= ColdTime)
        {
            FilledImage.fillAmount = 0;
            Timer = 0;
            IsStartTimer = false;
        }
    }
    public void OnClick()
    {
        IsStartTimer = true;
    }
}
