using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float lifetime;

    private void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Enemy"))
        {
            DestroyProjectile();     
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if(collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHealthAndDamage>().TakeDamage(damage);
            DestroyProjectile();
        }
       
       
    }


    void DestroyProjectile()
    {
        FindObjectOfType<AudioManager>().Play("Destroy Bullet");
        Destroy(gameObject);
    }
}
