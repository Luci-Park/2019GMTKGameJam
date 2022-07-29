﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChecker : MonoBehaviour
{
    public GameObject[] enemys;
    private Text enemysLeft; 

    void Start()
    {
        enemysLeft = GameObject.FindGameObjectWithTag("Enemys Left").GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
            enemysLeft.text = enemys.Length.ToString();
        
        if (enemys.Length <= 0)
        {
            SendMobDeadMesg();
        }
    }
    void SendMobDeadMesg()
    {
        DungeonController.currentDungeon.EveryMobDead();
    }
}


