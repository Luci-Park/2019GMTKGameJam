using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject thisRoom;
    public GameObject nextRoom;
    private EnemyChecker checker;
    private GameObject checkerObject;
    private Animator anim;
    public Animator SceneChangerAnim;
    public bool GoodDoor;
    public int direction;
    public Transform spawnPoint;
    private GameObject RightDoor;
    private GameObject LeftDoor;
    private GameObject TopDoor;
    private GameObject BottomDoor;
    private Transform Player;
    private float timeBtwDamage = 1;

    public void SetRoute(DungeonRoom dungeonRoom)
    {
        nextRoom = dungeonRoom.gameObject;
    }
    private void Start()
    {
        RightDoor = GameObject.FindGameObjectWithTag("Right Door");
        LeftDoor = GameObject.FindGameObjectWithTag("Left Door");
        BottomDoor = GameObject.FindGameObjectWithTag("Bottom Door");
        TopDoor = GameObject.FindGameObjectWithTag("Top Door"); 
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        GameObject SceneChanger = GameObject.FindGameObjectWithTag("SceneManager");
        SceneChangerAnim = SceneChanger.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
        checkerObject = GameObject.FindGameObjectWithTag("EnemyChecker");
        checker = checkerObject.GetComponent<EnemyChecker>();
    }

    private void Update()
    {
        timeBtwDamage -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && checker.EveryMobDead == true)
        {
            Debug.Log(GoodDoor);
            if (!GoodDoor )
            {
                if (timeBtwDamage <= 0)
                {
                    timeBtwDamage = 1;
                    Player.GetComponent<PlayerHealthSystem>().health--;
                    SceneChangerAnim.SetTrigger("end");
                }
            }
            else
            {
                StartCoroutine(ChangeRoom());
            }
            /*
            if (GoodDoor)
            {
                StartCoroutine(ChangeRoom());
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
           */
        }
        
    }
    
    public void doorClose()
    {
        FindObjectOfType<AudioManager>().Play("Door Close");
        anim.SetBool("Door Open", false);
    }

    public void DoorOpen()
    {
        FindObjectOfType<AudioManager>().Play("Door Open");
        anim.SetBool("Door Open", true);
    }



    IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(0.2f);
        Player.GetComponent<PlayerHealthSystem>().GiveHealth(7);
        Player.position = nextRoom.GetComponent<DungeonRoom>().doors[(direction + 2) % 4].spawnPoint.position;
        FindObjectOfType<Camera>().GetComponent<Transform>().position = Player.position;

        thisRoom.SetActive(false);

        nextRoom.SetActive(true);


    }
}
