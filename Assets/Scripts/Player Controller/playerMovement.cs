using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private float speed = 10f;
    private string VERTICAL_MOVEMENT = "Vertical";
    private string BULLET_TAG = "Bullet";
    private string ENEMY_TAG = "Enemy";

    [SerializeField] private float min_x, max_x;

    [SerializeField] private GameObject player_bullet;
    [SerializeField] private Transform attack_point; // it's a place, that's why it's defined as Transform

    public float attackTimer = 0.35f;
    private float current_attack_timer;
    private bool canAttack;

    private AudioSource laserAudio;

    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        current_attack_timer = attackTimer; // this will only work on start, next bullet will have to add to it a lot of "deltaTimes"
    }

    void Update()
    {
        movePlayer();
        Attack();
    }

    void movePlayer()
    {

        if (Input.GetAxisRaw(VERTICAL_MOVEMENT) > 0f) // if positive (means W button)
        {
            Vector2 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_x)
            {
                temp.y = max_x;
            }

            transform.position = temp;
        }

        else if (Input.GetAxisRaw(VERTICAL_MOVEMENT) < 0f) // if negative (means S button)
        {
                Vector2 temp = transform.position;
                temp.y -= speed * Time.deltaTime;

                if (temp.y < min_x)
                {
                    temp.y = min_x; // means, don't get a value smaller than this ever
                }

                transform.position = temp;
        }

    }

    void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > current_attack_timer) // in the 1st shot, we'll enter here at first check ..
        {
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.K) && canAttack)
        {
            canAttack = false;
            attackTimer = 0f;

            Instantiate(player_bullet, attack_point.position, Quaternion.identity);
            laserAudio.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D target) // if player gets collided with another bullet or enemy => destroy it
    {
        if (target.tag == BULLET_TAG || target.tag == ENEMY_TAG)
        {
            gameObject.SetActive(false);
        }
    }

}
