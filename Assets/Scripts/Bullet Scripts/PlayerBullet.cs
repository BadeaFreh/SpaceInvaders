using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 5f;
    public float deactivate_timer = 4.5f;

    [HideInInspector] public bool is_enemy_bullet = false;

    void Start()
    {
        if (is_enemy_bullet)
            speed *= -1f;

        // invoke it every 5 seconds (note that we call it from the Start(), which means it gets called only once for each bullet)
        Invoke("deactivateGameObject", deactivate_timer);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void deactivateGameObject() // called by Invoke
    {
        gameObject.SetActive(false); // this great method "destroys" the object from hierarchy
    }

    void OnTriggerEnter2D(Collider2D target) // if bullet get collided with another bullet or enemy => destroy it
    {
        if (target.tag == "Bullet" || target.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
