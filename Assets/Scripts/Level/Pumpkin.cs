using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Enemy
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //"AI"
        if (obj.transform.position.x > player.position.x && cooldown == 0)
        {
            if(ridB.velocity.x-speed>=-speed)
                ridB.velocity += new Vector2(-speed, 0);
        }
        if (obj.transform.position.x < player.position.x && cooldown == 0)
        {
            if(ridB.velocity.x + speed <= speed)
                ridB.velocity += new Vector2(speed, 0);
        }


    }
}
