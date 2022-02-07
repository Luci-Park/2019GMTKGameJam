using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
    public string Name;
    private EnemyChecker checker;
    private GameObject checkerObject;


    private void Start()
    {
        checkerObject = GameObject.FindGameObjectWithTag("EnemyChecker");
        checker = checkerObject.GetComponent<EnemyChecker>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && checker.EveryMobDead == true)
        {
            
            SceneManager.LoadScene(Name);
        }
    }
}
