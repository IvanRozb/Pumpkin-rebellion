using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float maxX, maxScale, minX;
    public Transform obj, hpBar, hpBarMask;
    public Transform player;
    public float speed;
    public Rigidbody2D ridB;
    public float hp;
    public float maxHp, damage, pushForce;

    public GameObject coin, heal, stamina;
    public float coins, heals, staminas;//Chance in percents
    public float coinCount, healCount, staminaCount;//Count of tries to spawn

    public float cooldown = 0;
    protected virtual void Start()
    {
        hp = maxHp;
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        if (cooldown < 0)
            cooldown = 0;



        //Hp display
        if (hp <= 0)
        {          
            ParticleSystem ps = this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ps.transform.position = this.transform.position;
            ps.gameObject.SetActive(true);
            this.transform.parent.gameObject.AddComponent<DestroyChecker>();
            DestroyChecker destroy = this.transform.parent.gameObject.GetComponent<DestroyChecker>();
            destroy.ps = ps;
            destroy.duration = 1f;
            destroy.childCount = 2;
            SpawnRewards();
            Destroy(this.transform.parent.GetChild(2).gameObject);
            Destroy(this.gameObject);
        }
        if (hp < maxHp)
        {
            hpBar.parent.gameObject.SetActive(true);
        }
        float currentLife = hp / maxHp;
        hpBar.position = this.transform.position + new Vector3(0, 1.5f, 0);
        hpBarMask.localPosition = new Vector3(minX + ((maxX - minX) * currentLife), hpBarMask.localPosition.y, hpBarMask.localPosition.z);
        hpBarMask.localScale = new Vector3(maxScale * currentLife, hpBarMask.localScale.y, 1);
    
    }

    protected virtual void SpawnRewards()
    {
        for (int i = 0; i < coinCount; i++)
        {
            if (i < 3)
            {
                SpawnReward.SpawnSmth(100f, this.transform.position, coin);
            }
            else
            {
                SpawnReward.SpawnSmth(coins, this.transform.position, coin);
            }
        }
        for (int i = 0; i < healCount; i++)
            SpawnReward.SpawnSmth(heals, this.transform.position, heal); 
        for (int i = 0; i < staminaCount; i++)
            SpawnReward.SpawnSmth(staminas, this.transform.position, stamina);
        Move.cooldown = 0.1f;

    }

}
