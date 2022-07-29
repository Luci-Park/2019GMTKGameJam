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
        SetDirections(numberOfRooms);
        SetDungeons();
    }

    void SetDirections(int numberOfRooms)
    {
        exitDirs = new Direction[numberOfRooms];
        exitDirs[0] = Direction.Top;
        for (int i = 1; i < numberOfRooms; i++)
        {
            int dir;
            do
            {
                dir = Random.Range(0, 4);
            } while (Mathf.Abs((int)exitDirs[i - 1] - dir) == 2);
            exitDirs[i] = (Direction)dir;
        }

        foreach(Direction d in exitDirs)
        {
            print(d);
        }
    }


    void SetDungeons()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < numberOfRooms; i++)
        {
            dungeonRooms[i] = CreateDungeon(pos, i);

            switch ((int)exitDirs[i])
            {
                case 0:
                    pos += new Vector3(-dungeonRooms[i].background.bounds.size.x, 0);
                    break;
                case 1:
                    pos += new Vector3(0, dungeonRooms[i].background.bounds.size.y);
                    break;
                case 2:
                    pos += new Vector3(dungeonRooms[i].background.bounds.size.x, 0);
                    break;
                case 3:
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

    DungeonRoom CreateDungeon(Vector3 pos, int idx)
    {
        GameObject newDungeonObject = Instantiate(dungeonRoom);
        newDungeonObject.transform.position = pos;
        
        DungeonRoom newDungeon = newDungeonObject.GetComponent<DungeonRoom>();
        newDungeon.background.sprite = sprites[idx];

        if (idx == 0)
        {
            newDungeon.SetRoomType(DungeonRoom.RoomType.First);
            newDungeon.SetEnterance(-1);
        }
        else if (idx == numberOfRooms - 1)
        {
            newDungeon.SetRoomType(DungeonRoom.RoomType.Last);
            newDungeon.SetEnterance(((int)exitDirs[idx - 1] + 2) % 4);
        }
        else 
        { 
            newDungeon.SetRoomType(DungeonRoom.RoomType.Normal);
            newDungeon.SetEnterance(((int)exitDirs[idx - 1] + 2) % 4);
        }

        return newDungeon;
    }
}
