using UnityEngine;


public class StaticTest
{
    public static int a = 1;

    public void aaa()
    {
        a += 2;
    }
}

public class TestStatic : MonoBehaviour
{
    public int b = 0;
    public static int c = 0;

    private void Start()
    {
    }
}