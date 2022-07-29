using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public GameObject LoseScreen;
    public Shake shake;

  
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject HurtParticle;
    private Animator anim;

    int maxHeart = 7;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        health = maxHeart;
        if (health > maxHeart)
        {
            health = maxHeart;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            LoseScreen.SetActive(true);
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void GiveHealth(int health)
    {
        this.health += health;
    }

   public void TakeDamage(int damage)
    {
        print(damage);
//        shake.ShakeStart(0.2f, 1, 1f);
        anim.SetTrigger("Hurt");
        Instantiate(HurtParticle, transform.position, Quaternion.identity);
        health -= damage;
    }
}
