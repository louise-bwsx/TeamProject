using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemSTO", menuName = "ScriptableObject/ItemSTO", order = 1)]

public class ItemSTO : ScriptableObject
{
    // Start is called before the first frame update
    public float Range;
    public GameObject[] ItemObjList;
    public float[] ItemObjRateList;
}
