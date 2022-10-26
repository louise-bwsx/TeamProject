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
    private GameObject endRoom;

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

        //xOffset = 0;
        //yOffset = 0;
        //Debug.Log(xOffset);
        //Debug.Log(yOffset);
        Reborn();

        //rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        //endRoom = rooms[0].gameObject;
        //找到最後房間
        foreach (var room in rooms)
        {
            //if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            //{
            //    endRoom = room.gameObject;
            //}

            SetupRoom(room, room.transform.position);
        }
        //endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.anyKeyDown){
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
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
        //direction = (Direction)Random.Range(0, 4);
        //switch (direction)
        //{
        //    case Direction.up:
        //        generatorPoint.position += new Vector3(0, yOffset, 0);
        //        break;
        //    case Direction.down:
        //        generatorPoint.position += new Vector3(0, -yOffset, 0);
        //        break;
        //    case Direction.left:
        //        generatorPoint.position += new Vector3(-xOffset, 0, 0);
        //        break;
        //    case Direction.right:
        //        generatorPoint.position += new Vector3(xOffset, 0, 0);
        //        break;
        //}
        //// 11時偶爾近來有些重疊
        //// 00時一定進來而且都在原點
        //if (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer))
        //{
        //    Debug.Log("if");
        //    generatorPoint.position += new Vector3(0, yOffset, 0);
        //}

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
}
