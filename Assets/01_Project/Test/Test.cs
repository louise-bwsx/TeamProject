using UnityEngine;

public class Test : MonoBehaviour
{
    public float ran = Random.Range(10f, 50f);
    public Vector3 v;
    public float rot_speed = 100f;

    private void Update()
    {
        v = new Vector3(ran, ran, ran);
        transform.Rotate(v * rot_speed * Time.deltaTime);
    }
}
