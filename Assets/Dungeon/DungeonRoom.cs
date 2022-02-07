using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public Door[] doors;
    public SpriteRenderer background;
    Door nextWay;
    Door entrance;
    bool lastRoom;
    GameObject checkerObject;
    EnemyChecker checker;
    public GameObject SceneChanger;
    bool done;
    bool bossSpawned;
    public GameObject bosscanvas;
    public GameObject bossPrefab;
    public GameObject nextRoom;
    public GameObject Enemy;
    // Start is called before the first frame 
    private void Awake()
    {
        SceneChanger = GameObject.FindGameObjectWithTag("SceneManager");
        bossSpawned = false;
        done = false;
        lastRoom = false;
        checkerObject = GameObject.FindGameObjectWithTag("EnemyChecker");
        checker = checkerObject.GetComponent<EnemyChecker>();
    }
    private void OnEnable()
    {
        SceneChanger.transform.position = transform.position;
        Vector3 pos = FindObjectOfType<PlayerHealthSystem>().transform.position;
        pos.z = -10;
        FindObjectOfType<Camera>().GetComponent<Transform>().position = pos;
        if (lastRoom)
        {
            foreach(Door door in doors)
            {
                door.gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (checker.EveryMobDead)
        {
            OpenAllDoor();
        }
        if (!lastRoom) return;
        if (checker.EveryMobDead&&!bossSpawned)
        {
            bossSpawned = true;
            GameObject boss = Instantiate(bossPrefab);
            boss.GetComponent<Transform>().position = transform.position;
            GameObject canvas = Instantiate(bosscanvas);
            canvas.GetComponent<Transform>().position = transform.position;
        }
    }
    public void DisableEnemies()
    {
        Enemy.SetActive(false);
    }
    public void SetDoor(int next, int entry)
    {
        nextWay = doors[next];
        nextWay.GoodDoor = true;
        entrance = doors[entry];
        print(nextWay);
    }
    public void IsLastRoom(bool isBossRoom)
    {
        lastRoom = isBossRoom;
    }
    public void TurnAllTriggerOff()
    {
        foreach(BoxCollider2D boxCollider in GetComponentsInChildren<BoxCollider2D>())
        {
            boxCollider.enabled = false;
        }
    }
    
    public void SetNextRoom(DungeonRoom nextRoom)
    {
        for(int i = 0; i<4; i++)
        {
            if (doors[i].GoodDoor)
            {
                doors[i].nextRoom = nextRoom.gameObject;
            }
            else
            {
                doors[i].nextRoom = gameObject;
            }
        }
    }
    public void ShowOnlyRightDoor()
    {
        foreach(Door door in doors)
        {
            if(door.GoodDoor)
            {
                continue;
            }
            door.gameObject.SetActive(false);
        }
    }
    void OpenAllDoor()
    {
        //door rumbling sound here
        foreach(Door door in doors)
        {
            if (door.isActiveAndEnabled)
                door.DoorOpen();
        }
    }
    
    void CloseAllDoors()
    {
        foreach (Door door in doors)
        {
            if(door.isActiveAndEnabled)
                door.doorClose();
        }

    }
}
