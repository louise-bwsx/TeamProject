using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemSTO", menuName = "ScriptableObject/ItemSTO", order = 1)]

public class ItemSTO : ScriptableObject
{
    public float Range;
    public GameObject[] ItemObjList;
    public float[] ItemObjRateList;
}
