using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int damagePoint;
    public float pushForce;

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    //Animation
    public Animator Animweapon;
    public Animator Animplayer;

    //Swing
    public float cooldown = 0.5f;
    public float lastSwing;

    float lastHit;
    public float hitCoolDown = 1f;
    public float attackCost;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    bool WasLastFrameHit=false;
    protected override void Update()
    {
        base.Update();
        

        if (Input.GetMouseButtonDown(0))//Leftclick
        {
            if (Time.time - lastSwing > cooldown&&CameraAttach.playerSt>=attackCost)
            {
                lastSwing = Time.time;
                //Swing();
                WasLastFrameHit = true;
                if(!Animweapon.GetCurrentAnimatorStateInfo(0).IsName("Wylamovement"))
                    CameraAttach.playerSt -= attackCost;
            }
            else
            {
                WasLastFrameHit = false;
            }


        }
        else
        {
            if(Input.GetMouseButton(0)==false)
            WasLastFrameHit = false;
        }
        if (WasLastFrameHit)
        {
            Animweapon.SetBool("ishit", true);
            Animplayer.SetBool("ishit", true);
        }
        else
        {
            Animweapon.SetBool("ishit", false);
            Animplayer.SetBool("ishit", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D coll = collision.collider;
        if (coll.tag == "Pumpkin" && Animweapon.GetCurrentAnimatorStateInfo(0).IsName("Wylamovement") && (Time.time-lastHit >= hitCoolDown))
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };
            Pumpkin pumpkin = coll.GetComponent<Pumpkin>();
            pumpkin.hp -= dmg.damageAmount;
            if (pumpkin.obj.position.x > pumpkin.player.position.x) { 
                pumpkin.ridB.velocity += new Vector2(dmg.pushForce, 0);}
            if (pumpkin.obj.position.x <= pumpkin.player.position.x)
                pumpkin.ridB.velocity += new Vector2(-dmg.pushForce , 0);
            pumpkin.cooldown = 2.5f;
            //coll.SendMessage("ReceiveDamage", dmg);
            lastHit = Time.time;

        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        //base.OnCollide(coll);
        //if (coll.tag == "Pumpkin" /*&& Input.GetMouseButtonDown(0)*/)
        /*{
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce=pushForce
            };
            coll.GetComponent<Pumpkin>().pumpkinHp -= dmg.damageAmount;
            //coll.SendMessage("ReceiveDamage", dmg);

            
        }*/


    }
    private void Swing()
    {
        Debug.Log("Swing");
        //OnCollide();
    }

}
