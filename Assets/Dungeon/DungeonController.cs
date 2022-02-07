using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonController : MonoBehaviour
{
    public int numberOfRooms;
    public enum Direction { Left, Top, Right, Bottom};
    public GameObject dungeonRoom;
    Direction[] directions;
    DungeonRoom[] dungeonRooms;
    public Sprite[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
        dungeonRooms = new DungeonRoom[numberOfRooms];
        directions = new Direction[numberOfRooms + 1];
        directions[0] = Direction.Top;
        for(int i = 1; i<=numberOfRooms; i++)
        {
            int dir; 
            do
            {
                dir = Random.Range(0, 4);
            } while (Mathf.Abs((int)directions[i-1] - dir) == 2);
            directions[i] = (Direction)dir;
        }
    }
    void Start()
    {
        Vector3 pos = Vector3.zero;
        for(int i = 1; i<=numberOfRooms; i++)
        {
            GameObject newDungeon = Instantiate(dungeonRoom);
            newDungeon.GetComponent<DungeonRoom>().background.sprite = sprites[i - 1];
            newDungeon.transform.position = pos;
            newDungeon.GetComponent<DungeonRoom>().SetDoor((int)directions[i], ((int)directions[i - 1]+2)% 4);
            dungeonRooms[i - 1] = newDungeon.GetComponent<DungeonRoom>();
            print("entrance " + ((int)directions[i - 1] + 2) % 4 + " exit" + (int)directions[i]);
            switch ((int)directions[i])
            {
                case 0:
                    pos += new Vector3(-newDungeon.GetComponent<DungeonRoom>().background.bounds.size.x, 0);
                    break;
                case 1:
                    pos += new Vector3(0, newDungeon.GetComponent<DungeonRoom>().background.bounds.size.y);
                    break;
                case 2:
                    pos += new Vector3(newDungeon.GetComponent<DungeonRoom>().background.bounds.size.x, 0);
                    break;
                case 3:
                    pos += new Vector3(0, -newDungeon.GetComponent<DungeonRoom>().background.bounds.size.y);
                    break;
            }
            if (i != 1)
            {
                dungeonRooms[i - 1].gameObject.SetActive(false);
                dungeonRooms[i - 2].SetNextRoom(dungeonRooms[i - 1]);
                if (i == numberOfRooms)
                {
                    dungeonRooms[i - 1].IsLastRoom(true);
                }
            }
        }
        dungeonRooms[0].ShowOnlyRightDoor();
        dungeonRooms[0].DisableEnemies();
        dungeonRooms[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
