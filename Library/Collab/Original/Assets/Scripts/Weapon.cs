using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;


    //Swing
    public float cooldown = 0.5f;
    public float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        /*spriteRenderer = GetComponent<SpriteRenderer>();*/

        if (Input.GetMouseButtonDown(0))//Leftclick
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        //base.OnCollide(coll);
        if (coll.tag == "Pumkin" && Input.GetMouseButtonDown(0))
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce=pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);

            Pumpkin.pumpkinHp -= damagePoint;
        }


    }
    private void Swing()
    {
        Debug.Log("Swing");
        //OnCollide();
    }

}
