using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public Transform pivot;

    public float startTimeBtwShots;
    private float timeBtwShots;
    
    void Update()
    {
        if (timeBtwShots <= 0)
        {
          if (Input.GetButton("Fire1"))
          {
            Instantiate(projectile, shotPoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
          }

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
