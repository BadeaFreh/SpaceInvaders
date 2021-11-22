using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5f;

    public float rotate_speed = 30f;
    
    // not all enemies are the same, some are asteroids, and some are spaceships, so we need some boolean vars here:
    public bool canShoot;
    public bool canRotate;
    private bool canMove = true; // all enemies can move by default
    public float bound_x = -11f; // at this point, enemies will be out of the game frame => so destroy them

    public Transform attack_point;
    public GameObject enemy_bullet;

    private Animator anim;
    
    private AudioSource explosion_sound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosion_sound = GetComponent<AudioSource>();

    }

    void Start()
    {
        // randomizing the rotation:
        if (canRotate)
        {
            if (Random.Range(0, 2) == 0) // 0 or 1
            {
                rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
                rotate_speed *= -1f;
            }

            else // if random.range == 1
            {
                rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
            }
        }

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        if (canMove) // (and all enemies can move in general)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            if (temp.x < bound_x)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Rotate()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World); // only the z axis should rotate
        }
    }

    void StartShooting()
    {
        // Quaternion.identity is 0,0,0 in rotation
        GameObject bullet = Instantiate(enemy_bullet, attack_point.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().is_enemy_bullet = true;

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    
    // unity calls this method when the game object collides with any other game object
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            canMove = false;
            if (canShoot){
                canShoot = false;
                CancelInvoke("StartShooting"); // destroyed ships don't shoot
            }
            
            Invoke("TurnOffGameObject", 3f);

            anim.Play("Destroy"); // this animation is only for enemy ships, asteroid can't access this
            explosion_sound.Play();
        }
    }

}
