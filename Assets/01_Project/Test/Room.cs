using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject LeftDoor, RightDoor, UpDoor, DownDoor;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart;
    public Text text;
    void Start()
    {
        //LeftDoor.SetActive(roomLeft);
        //RightDoor.SetActive(roomRight);
        //UpDoor.SetActive(roomUp);
        //DownDoor.SetActive(roomDown);
    }

    public void UpdateRoom()
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));

        text.text = stepToStart.ToString();
    }
}
