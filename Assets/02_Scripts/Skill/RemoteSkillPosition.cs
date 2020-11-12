using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteSkillPosition : MonoBehaviour
{
    public Transform skillPosition;
    public RectTransform hadle;
    public RectTransform outerCircle;
    public float skillControlSpeed;
    public MeshRenderer meshRenderer;


    bool isTouch;
    Vector3 originPosition;
    int floor;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        floor = LayerMask.GetMask("Floor");
    }
    void FixedUpdate()
    {
        if (isTouch)
        {
            meshRenderer.enabled = isTouch;
            //用一個Vector3把搖桿距離中心點的位置給存起來
            Vector3 offset = outerCircle.position - hadle.position;
            //因為搖桿是Vector2所以要把它的Y給Z
            offset.z = offset.y;
            //算距離?
            Vector3 direction = Vector3.ClampMagnitude(offset, 1f);
            //技能施放位置移動
            skillPosition.Translate(direction * -1f * skillControlSpeed * Time.deltaTime);
            RaycastHit hit;
            //技能施放位置朝地板打射線如果碰到地板
            if (Physics.Raycast(skillPosition.position, -skillPosition.up, out hit, 10f, floor))
            {
                //先存起來等不小心跑進地板再拿出來用
                originPosition = skillPosition.position;
                //技能位置等於射線打到的點朝上0.1f
                skillPosition.position = hit.point + skillPosition.up * 0.1f;
            }
            //如果潛入地下就回到還沒回到地下以前的點
            else
            {
                skillPosition.position = originPosition;
            }
        }
        else if (!isTouch)
        {
            meshRenderer.enabled = false;
            skillPosition.position = skillPosition.parent.position;
        }
    }
    public void SetSkillPosition(bool isTouch)
    {
        this.isTouch = isTouch;
    }
}
