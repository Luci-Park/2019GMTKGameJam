using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealthAndDamage : MonoBehaviour
{
    [Header("Variabales")]
    public int damage;
    public int health;
    public int maxHealth;
  

    

    

    [Header("Components")]

    public float timeBetweenAttack;
    private float startTimeBetweenAttack;

    public GameObject HurtParticle;
    public Slider Slider;
    public GameObject WinScreen;

    private Transform target;
    public Rigidbody2D rb;
    public bool testHurt;
    private Animator anim;

    
    



    public void Start()
    {
        
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        Slider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        

       

        Slider.maxValue = maxHealth;

        Slider.value = health;

        timeBetweenAttack -= Time.deltaTime;

       


        if (health < 0)
        {
            Die();
        }
        if (testHurt)
        {
            testHurt = false;
            TakeDamage(damage);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damage);

        }
    }

    public void Die()
    {

        SceneManager.LoadScene("Win Scene");
        FindObjectOfType<AudioManager>().Play("Enemy Hit");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Enemy Hit");
        Instantiate(HurtParticle, transform.position, Quaternion.identity);
        health -= damage;
        anim.SetTrigger("Hurt");
    }
}
