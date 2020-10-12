using System.Collections;
using UnityEngine;

public class PoisonDestroyEffect : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,3F);
    }
}
