using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonController : MonoBehaviour
{
    public enum Direction { Left, Top, Right, Bottom };

    public static DungeonRoom currentDungeon;
    public int numberOfRooms;
    public GameObject dungeonRoom;
    Direction[] exitDirs;
    DungeonRoom[] dungeonRooms;
    public Sprite[] sprites;

    public static void SetCurrentRoom(DungeonRoom room)
    {
        currentDungeon = room;
    }

    void Start()
    {
        dungeonRooms = new DungeonRoom[numberOfRooms];
        SetDirectionsForAllRoom(numberOfRooms);
        SetDungeons();
    }

    void SetDirectionsForAllRoom(int numberOfRooms)
    {
        exitDirs = new Direction[numberOfRooms];
        exitDirs[0] = Direction.Top;
        for (int i = 1; i < numberOfRooms; i++)
        {
            Direction dir;
            do
            {
                dir = (Direction) Random.Range(0, 4);
            } while (Mathf.Abs(exitDirs[i - 1] - dir) == 2);//before exit - 2 = this room's entrance;
            exitDirs[i] = dir;
        }
    }

    void SetDungeons()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < numberOfRooms; i++)
        {
            dungeonRooms[i] = CreateDungeon(pos, i);

            switch (exitDirs[i])
            {
                case Direction.Left:
                    pos += new Vector3(-dungeonRooms[i].background.bounds.size.x, 0);
                    break;
                case Direction.Top:
                    pos += new Vector3(0, dungeonRooms[i].background.bounds.size.y);
                    break;
                case Direction.Right:
                    pos += new Vector3(dungeonRooms[i].background.bounds.size.x, 0);
                    break;
                case Direction.Bottom:
                    pos += new Vector3(0, -dungeonRooms[i].background.bounds.size.y);
                    break;
            }
            if (i > 0)
            {
                int beforeRoomExit = (int)exitDirs[i - 1];

                dungeonRooms[i - 1].SetNextRoom(dungeonRooms[i], beforeRoomExit);
            }
            dungeonRooms[i].gameObject.SetActive(false);
        }
        dungeonRooms[0].gameObject.SetActive(true);
        currentDungeon = dungeonRooms[0];
    }

    DungeonRoom CreateDungeon(Vector3 pos, int roomIdx)
    {
        GameObject newDungeonObject = Instantiate(dungeonRoom);
        newDungeonObject.transform.position = pos;
        
        DungeonRoom newDungeon = newDungeonObject.GetComponent<DungeonRoom>();
        newDungeon.background.sprite = sprites[roomIdx];

        if (roomIdx == 0)
        {
            newDungeon.SetRoomType(DungeonRoom.RoomType.First);
            newDungeon.SetEnterance(-1);
        }
        else if (roomIdx == numberOfRooms - 1)
        {
            newDungeon.SetRoomType(DungeonRoom.RoomType.Last);
            newDungeon.SetEnterance(((int)exitDirs[roomIdx - 1] + 2) % 4);
        }
        else 
        { 
            newDungeon.SetRoomType(DungeonRoom.RoomType.Normal);
            newDungeon.SetEnterance(((int)exitDirs[roomIdx - 1] + 2) % 4);
        }

        return newDungeon;
    }
}
