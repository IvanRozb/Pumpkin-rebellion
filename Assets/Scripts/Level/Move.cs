using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D playerRd;
    public float maxSpeed, speed, jumpSpeed;
    public Collider2D groundCol,playerCol;
    bool rightMove = false, leftMove = false;
    public Transform player;
    public float healRate, staminaRate;
    public Animator anim;
    float lastDamaged, lastCollide;
    public float pickUpCooldown;
    public static float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        lastDamaged = 0;
    }

    // Update is called once per frame
    void Update()
    {

        cooldown -= Time.deltaTime;
        if (cooldown < 0)
            cooldown = 0;

        //–ух гравц€
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d"))
        {
            rightMove = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp("d"))
        {
            rightMove = false;
        }

        if (rightMove)
        {
            player.rotation = new Quaternion(0, 0, 0, 0);
            if (playerRd.velocity.x <= maxSpeed - speed)
                playerRd.velocity += new Vector2(speed, 0);
            else if (playerRd.velocity.x > maxSpeed - speed)
                playerRd.velocity = new Vector2(maxSpeed, playerRd.velocity.y);

        }



        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a"))
        {
            leftMove = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp("a"))
        {
            leftMove = false;
        }

        if (leftMove)
        {
            player.rotation = new Quaternion(0, 180, 0, 0);
            if (playerRd.velocity.x >= -maxSpeed + speed)
                playerRd.velocity -= new Vector2(speed, 0);
            else if (playerRd.velocity.x < -maxSpeed + speed)
                playerRd.velocity = new Vector2(-maxSpeed, playerRd.velocity.y);

        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))&&Physics2D.IsTouching(groundCol,playerCol))
        {
            playerRd.velocity = new Vector2(playerRd.velocity.x, jumpSpeed);
        }
        anim.SetFloat("speed", (int) Mathf.Abs(playerRd.velocity.x));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D coll = collision.collider;
        if (coll.tag == "Pumpkin" && (Time.time - lastDamaged >= cooldown) && Physics2D.IsTouching(playerCol, collision.collider))
        {
            Pumpkin pumpkin = collision.gameObject.GetComponent<Pumpkin>();
            CameraAttach.playerHp -= pumpkin.damage;
            if (collision.gameObject.transform.position.x > player.position.x)
                playerRd.velocity += new Vector2(-pumpkin.pushForce, 0.5f);
            if (collision.gameObject.transform.position.x <= player.position.x)
                playerRd.velocity += new Vector2(pumpkin.pushForce, 0.5f);
            lastDamaged = Time.time;
        } else if (cooldown==0) {
            if (coll.tag == "Coin")
            {
                GameObject ParticleSys = collision.gameObject.transform.parent.GetChild(1).gameObject;
                ParticleSys.transform.position = collision.transform.position;
                ParticleSys.SetActive(true);
                Destroy(collision.gameObject);
                GameObject parent = ParticleSys.transform.parent.gameObject;
                parent.AddComponent<DestroyChecker>();
                DestroyChecker destroy = parent.GetComponent<DestroyChecker>();
                destroy.ps = ParticleSys.GetComponent<ParticleSystem>();
                destroy.duration = 1f;
                //destroy.childCount = 1;
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                cooldown=pickUpCooldown;
            }
            else if (coll.tag == "Heal")
            {
                GameObject ParticleSys = collision.gameObject.transform.parent.GetChild(1).gameObject;
                ParticleSys.transform.position = player.position;
                GameObject parent = ParticleSys.transform.parent.gameObject;
                ParticleSys.transform.parent = player;
                ParticleSys.transform.localScale = new Vector3(1, 1, 1);
                ParticleSys.SetActive(true);
                Destroy(collision.gameObject);
                parent.AddComponent<DestroyChecker>();
                DestroyChecker destroy = parent.GetComponent<DestroyChecker>();
                destroy.ps = ParticleSys.GetComponent<ParticleSystem>();
                destroy.duration = 1f;
                //destroy.childCount = 3;
                CameraAttach.playerHp += healRate;
                cooldown = pickUpCooldown;
            }
            else if (coll.tag == "Stamina")
            {
                GameObject ParticleSys = collision.gameObject.transform.parent.GetChild(1).gameObject;
                ParticleSys.transform.position = player.position;
                GameObject parent = ParticleSys.transform.parent.gameObject;
                ParticleSys.transform.parent = player;
                ParticleSys.transform.localScale = new Vector3(1, 1, 1);
                ParticleSys.SetActive(true);
                Destroy(collision.gameObject);
                parent.AddComponent<DestroyChecker>();
                DestroyChecker destroy = parent.GetComponent<DestroyChecker>();
                destroy.ps = ParticleSys.GetComponent<ParticleSystem>();
                destroy.duration = 1f;
                //destroy.childCount = 3;
                CameraAttach.playerSt += staminaRate;
                cooldown = pickUpCooldown;
            }
        }
    }

}
