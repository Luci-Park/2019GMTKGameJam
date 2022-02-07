using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    public float bulletSpeed;
    public float startTimeBtwShots;
    private float timeBtwShots;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Shoot();

    }
    void Shoot()
    {

        Vector3 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10)) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (timeBtwShots <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                FindObjectOfType<AudioManager>().Play("Fire");
                direction.Normalize();
                GameObject bullet = Instantiate(projectile, shotPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                timeBtwShots = startTimeBtwShots;
            }

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
