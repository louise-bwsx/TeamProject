using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public float staminaCost;
    public float destroyTime = 3F;
    public GameObject skillObject;
    public Transform skillPos;
    public Transform skillRotation;
    public MobileStats mobileStats;
    public float skillForce = 200f;
    public float skillTimer;//最後射擊時間
    public float skillCD;//射擊間隔
    public Image fillImage;
    public Image[] skillActiveImages;
    public MeshRenderer meshRenderer;
    protected MobileAttack mobileAttack;
    void Start()
    {
        skillTimer = 10f;//確保一開始都能按技能
        mobileStats = FindObjectOfType<MobileStats>();
        mobileAttack = FindObjectOfType<MobileAttack>();
    }
    void Update()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer < skillCD)
        {
            fillImage.fillAmount = (skillCD - skillTimer) / skillCD;
            foreach (Image i in skillActiveImages)
            {
                i.enabled = false;
            }
        }
        else if (skillTimer >= skillCD)
        {
            fillImage.fillAmount = 0;
        }
    }
    public virtual void Shoot()
    {
        //生成攻擊範圍
        GameObject bulletObj = Instantiate(skillObject);
        if (bulletObj != null)
        {
            bulletObj.transform.position = skillPos.position;
            bulletObj.transform.rotation = skillRotation.rotation;
            Rigidbody BulletObjRigidbody_ = bulletObj.GetComponent<Rigidbody>();
            if (BulletObjRigidbody_ != null)
            {
                //指向性技能朝指定Z軸方向射擊
                BulletObjRigidbody_.AddForce(bulletObj.transform.forward * skillForce);
            }
            //在一定秒數摧毀攻擊範圍
            Destroy(bulletObj, destroyTime);
            //扣能量
            mobileStats.stamina -= staminaCost;
        }
        //開始冷卻時間
        skillTimer = 0;
        //使玩家移動限制解除
        mobileAttack.isAttack = false;
        //指向性技能範圍提示關閉
        meshRenderer.enabled = false;
        foreach (Image i in skillActiveImages)
        {
            i.enabled = false;
        }
    }
}
