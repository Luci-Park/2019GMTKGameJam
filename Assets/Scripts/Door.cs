using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator SceneChangerAnim;
    public Transform spawnPoint;

    public bool isGood{get; private set;}

    private GameObject thisRoom;
    private GameObject nextRoom;
    private Transform Player;
    private Animator anim;

    private bool isClosed;
    private float damageCooldown = 1;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject SceneChanger = GameObject.FindGameObjectWithTag("SceneManager");
        SceneChangerAnim = SceneChanger.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isClosed)
        {
            if (!isGood)
            {
                GiveDamage();
            }
            else
            {
                StartCoroutine(ChangeRoom());
            }
        }

    }
    public void SetRoute(DungeonRoom thisDun, DungeonRoom nextDun)
    {
        Debug.Log("Door from " + thisDun.name + " isGood is " + isGood);
        thisRoom = thisDun.gameObject;
        nextRoom = nextDun.gameObject;

        if (thisRoom != nextRoom) isGood = true;
        else isGood = false;
    }
    public void DoorClose()
    {
        FindObjectOfType<AudioManager>().Play("Door Close");
        anim.SetBool("Door Open", false);
        isClosed = true;
    }

    public void DoorOpen()
    {
        FindObjectOfType<AudioManager>().Play("Door Open");
        anim.SetBool("Door Open", true);
        isClosed = false;
    }

    void GiveDamage()
    {
        if (damageCooldown <= 0)
        {
            damageCooldown = 1;
            Player.GetComponent<PlayerHealthSystem>().health--;
            SceneChangerAnim.SetTrigger("end");
        }
    }

    IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(0.2f);
        Player.GetComponent<PlayerHealthSystem>().GiveHealth(7);

        Player.position = nextRoom.GetComponent<DungeonRoom>().GetSpawn();
        FindObjectOfType<Camera>().GetComponent<Transform>().position = Player.position;

        thisRoom.SetActive(false);

        nextRoom.SetActive(true);
        DungeonController.SetCurrentRoom(nextRoom.GetComponent<DungeonRoom>());


    }
}
