using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDIsplay : MonoBehaviour
{
    public Text thousands, hundreds, tens, ones;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        thousands.text =  (coins / 1000).ToString();
        hundreds.text = ((coins / 100)%10).ToString();
        tens.text = ((coins / 10) % 10).ToString();
        ones.text = (coins % 10).ToString();
    }
}
