using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnReward
    {
        public static void SpawnSmth(float chance, Vector3 pos, GameObject obj)
        {
            System.Random rand = new System.Random();
            float buf = rand.Next(0,100);
            if (buf <= chance)
            {
                GameObject smth = obj;
                smth.transform.position = pos;
                smth.name = smth.name.Replace("(Clone)", "");
                GameObject.Instantiate(smth);
            }
        }

       
    }
}
