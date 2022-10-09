using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFadeGroup : MonoBehaviour
{
    public MeshRenderer[] wallGroup;
    public List<Material> colors = new List<Material>();
    public Material fadeColor;
    public float recoverTime;
    public bool isFade = false;
    void Start()
    {
        wallGroup = GetComponentsInChildren<MeshRenderer>(true);

        foreach (MeshRenderer i in wallGroup)
        {
                colors.Add(i.material);
        }
    }
    void Update()
    {
        recoverTime += Time.deltaTime;
        if (recoverTime > 2 && isFade == true)
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
        int a = 0;
        foreach (MeshRenderer i in wallGroup)
        {
            i.material = colors[a];
            a++;
        }
        isFade = false;
    }
}
