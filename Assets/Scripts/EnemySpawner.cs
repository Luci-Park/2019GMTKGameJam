using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject theEnemy;
    private float xPos;
    private float yPos;
    private int enemyCount;
    public int enemyMaxSpawn;
    public Transform spawnValuesX;
    public Transform spawnValuesY;
    public float radius;




    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    IEnumerator EnemySpawn()
    {
        //Preventing enemies spawning too close to the player
        
        while (enemyCount < enemyMaxSpawn)
        {
            Vector2 spawnPosition = new Vector2();
          float dist = (thePlayer.transform.position - spawnPosition).magnitude;

            if (dist > radius)
            {
                Instantiate(theEnemy, spawnPosition, Quaternion.identity);
            }
            else
            {
             spawnPosition = new Vector2(Random.Range(-spawnValuesX.x, spawnValuesX.x), Random.Range(-spawnValuesY.y, spawnValuesY.y));
            }
        

            //Spawn 5 random enemies
            xPos = Random.Range(-17, 17);
            yPos = Random.Range(-7, 7);
            Instantiate(theEnemy, new Vector2(xPos, yPos), Quaternion.identity);

            yield return new WaitForSeconds(0.01f);

            enemyCount += 1;
            */
        }

    

