using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFadeGroup : MonoBehaviour
{
    public MeshRenderer[] wallGroup;
    public Material[] colors;
    public Material fadeColor;
    public float recoverTime;
    public bool isFade = false;
    void Start()
    {
        wallGroup = GetComponentsInChildren<MeshRenderer>();
        //不讓輸入material 
        //colors = transform.GetComponentsInChildren<Material>();
        //colors = wallGroup <Material>();
        //foreach (MeshRenderer i in wallGroup)
        //{
        //    colors = i.GetComponent<MeshRenderer>().materials;
        //}
    }
    void Update()
    {
        recoverTime += Time.deltaTime;
        if (recoverTime > 2 && isFade == false)
        {
            AllWallRecover();
        }
    }

    public void AllWallFade()
    {
        foreach (MeshRenderer i in wallGroup)
        {
            i.material = fadeColor;
        }
        recoverTime = 0;
        isFade = true;
    }
    public void AllWallRecover()
    {
        for (int i = 0; i < wallGroup.Length; i++)
        {
            wallGroup[i].material = colors[i];
        }
        //foreach (MeshRenderer i in wallGroup)
        //{
        //    i.material = colors[];
        //}
        isFade = false;
    }
}
