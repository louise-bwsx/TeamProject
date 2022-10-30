using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房間資訊")]
    public GameObject roomRrefab;
    /// <summary>
    /// 房間數量
    /// </summary>
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject startRoom, endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public static float xOffset;
    public static float yOffset;
    public LayerMask roomLayer;

    public List<Room> rooms = new List<Room>();

    void Start()
    {
        xOffset = 1;
        yOffset = 1;

        Reborn();

        rooms[0].GetComponent<SpriteRenderer>().color = Color.yellow;
        startRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            SetupRoom(room, room.transform.position);
        }
        FindEndRoom();
        endRoom = rooms[rooms.Count - 1].gameObject;
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    /// <summary>
    /// 生成房間
    /// </summary>
    public void Reborn()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomRrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改變Point位置
            ChangePointPos(rooms[rooms.Count - 1]);
        }
    }

    public void ChangePointPos(Room newRoom)
    {
        // 11時一定會進來不會很分散
        // 00時Unity直接卡住
        do
        {
            Debug.Log("random");
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        //newRoom.UpdateRoom();
    }

    public void FindEndRoom()
    {
        Room farRoom = null;
        int index = 0;
        for (var i = 0; i < rooms.Count; i++)
        {
            // if (rooms[i].doorNumber > 1)
            {
                continue;
            }
            if (!farRoom)
            {
                farRoom = rooms[i];
                index = i;
                continue;
            }
            float distance = Vector3.Distance(startRoom.transform.position, rooms[i].transform.position);
            float checkDistance = Vector3.Distance(startRoom.transform.position, farRoom.transform.position);
            if (distance > checkDistance)
            {
                farRoom = rooms[i];
                index = i;
                continue;
            }
        }
        Vector3 farestPosition = farRoom.transform.position;
        Destroy(rooms[index].gameObject);
        // rooms[index] = Instantiate(roomRrefab[bossRoom], farestPosition, Quaternion.identity).GetComponent<Room>();
        farRoom.GetComponent<SpriteRenderer>().color = Color.red;
        //最大數值
        // for (int i = 0; i < rooms.Count; i++)
        // {
        //     if (rooms[i].stepToStart > maxStep)
        //     {
        //         maxStep = rooms[i].stepToStart;
        //     }
        // }

        // //獲得最大值的房間和次大值
        // foreach (var room in rooms)
        // {
        //     if (room.stepToStart == maxStep)
        //         farRoom.Add(room.gameObject);
        //     if (room.stepToStart == maxStep - 1)
        //         lessFarRooms.Add(room.gameObject);
        // }

        // for (int i = 0; i < farRoom.Count; i++)
        // {
        //     if (farRoom[i].GetComponent<Room>().doorNumber == 1)
        //         oneWayRooms.Add(farRoom[i]);
        // }

        // for (int i = 0; i < lessFarRooms.Count; i++)
        // {
        //     if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
        //         oneWayRooms.Add(lessFarRooms[i]);
        // }

        // if (oneWayRooms.Count != 0)
        // {
        //     endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        // }
        // else
        // {
        //     endRoom = farRoom[Random.Range(0, farRoom.Count)];
        // }
    }
}
