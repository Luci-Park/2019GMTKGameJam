using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public enum RoomType {First, Last, Normal };

    [Tooltip("left, top, right, bottom, in that order")] public Door[] doors;
    public SpriteRenderer background;
    public GameObject bosscanvas;
    public GameObject bossPrefab;
    public GameObject Enemy;

    GameObject SceneChanger;

    RoomType roomType;
    int enterance;
    
    // Start is called before the first frame 
    private void Awake()
    {
        SceneChanger = GameObject.FindGameObjectWithTag("SceneManager");
    }
    private void OnEnable()
    {
        SetUI();
        CloseAllDoors();
        Debug.Log("RoomType = " + roomType);
    }
   
    public void EveryMobDead()
    {
        if (roomType == RoomType.Last) SpawnBoss();
        else
        {
            OpenAllDoors();
        }
    }

    public Vector3 GetSpawn()
    {
        return doors[enterance].spawnPoint.position;
    }

    public void SetRoomType(RoomType type)
    {
        roomType = type;
    }
    
    public void SetEnterance(int enterance)
    {
        this.enterance = enterance;
    }

    public void SetNextRoom(DungeonRoom nextRoom, int exitDir)
    {
        for (int i = 0; i<4; i++)
        {
            if (i == exitDir)
            {
                doors[i].SetRoute(this, nextRoom);
            }
            else
            {
                doors[i].SetRoute(this, this);
            }
        }
    }

    void SetUI()
    {
        SceneChanger.transform.position = transform.position;
        Vector3 pos = FindObjectOfType<PlayerHealthSystem>().transform.position;
        pos.z = -10;
        FindObjectOfType<Camera>().GetComponent<Transform>().position = pos;
        if (roomType == RoomType.Last) SetLastRoom();
        else if (roomType == RoomType.First) SetFirstRoom();
        else SetNormalRoom();
    }
    void SetNormalRoom()
    {
        foreach(Door door in doors)
        {
            door.gameObject.SetActive(true);
        }
        EnableEnemies();
    }
    void SetLastRoom()
    {
        foreach (Door door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }
    void SetFirstRoom()
    {
        Debug.Log("setfirstRoom Called");
        foreach (Door door in doors)
        {
            if (door.isGood) door.gameObject.SetActive(true);
            else door.gameObject.SetActive(false);
        }
        DisableEnemies();
    }
    void DisableEnemies()
    {
        Enemy.SetActive(false);
    }
    void EnableEnemies()
    {
        Enemy.SetActive(true);
    }

    void OpenAllDoors()
    {
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
            if (door.isActiveAndEnabled)
                door.DoorClose();
        }
    }
    
    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab);
        boss.GetComponent<Transform>().position = transform.position;
        GameObject canvas = Instantiate(bosscanvas);
        canvas.GetComponent<Transform>().position = transform.position;
    }
}
