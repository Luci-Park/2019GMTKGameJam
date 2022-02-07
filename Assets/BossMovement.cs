using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveTime;
    public float speed;
    public float chargeTime;
    public bool isCharging;
    public float rotateTime;
    public float stopTime;
    private Rigidbody2D rigidbody;
    float timer = 0;
    Transform player;
    Vector3 targetpos;
    float startTime;
    enum State { move, rotate, attack};
    State state;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isCharging = false;
        state = State.move;
        player = FindObjectOfType<PlayerHealthSystem>().transform;
    }

    
    void Update()
    {
        print(state);
        switch (state)
        {
            case State.move:
                Move();
                timer += Time.deltaTime;
                if(timer > moveTime)
                {
                    state = State.rotate;
                    timer = 0;
                }
                break;
            case State.rotate:
                Rotate();
                timer += Time.deltaTime;
                if(timer > rotateTime)
                {
                    startTime = Time.time;
                    state = State.attack;
                    timer = 0;
                    targetpos = player.position;
                    isCharging = true;
                }
                break;
            case State.attack:
                Attack();
                if(!isCharging)
                {
                    timer += Time.deltaTime;
                    if (timer > stopTime)
                    {
                        state = State.move;
                        timer = 0;
                    }
                }
                break;

        }
        
    }

   void Move()
    {
        Rotate();
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == State.attack&&collision.gameObject.tag == "Player")
        {
            isCharging = false;
        }
    }
    void Rotate()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Attack()
    {
        //print(target.position);
        if(transform.position == targetpos)
        {
            print(true);
            isCharging = false;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetpos, (Time.time - startTime) / chargeTime);
    }

    
}
