using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAttach : MonoBehaviour
{
    public Camera camera;
    public Transform player, background, plHpBarMask, plStBarMask;
    public GameManager gameManager;
    public float maxX, maxScale, minX;
    public static float playerHp, playerSt;
    public float maxHp, maxSt;
    public float StaminaRegenRate;

    float lastAdd;

    public Transform clouds;
    GameObject[] parts;
    float delta, previous;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = maxHp;
        playerSt = maxSt;
        previous = player.position.x;
        parts = GameObject.FindGameObjectsWithTag("Cloud");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerHp < 0)
        {
            playerHp = 0;
            FindObjectOfType<GameManager>().EndGame();
        }
        if (playerHp > maxHp)
            playerHp = maxHp;
        
        playerSt+=(Time.deltaTime/StaminaRegenRate);
            

        if (playerSt < 0)
            playerSt = 0;

        if (playerSt > maxSt)
            playerSt = maxSt;

        //Прив'язка камери
        camera.transform.position = player.transform.position + new Vector3(0,0,-10);
        background.transform.position = player.transform.position;

        //Плавні хмари
        delta = previous - player.position.x;

        clouds.position = new Vector3(player.position.x, clouds.position.y, clouds.position.z);
        
        foreach (GameObject ob in parts)
        {
            ob.transform.localPosition += new Vector3(delta / 1.5f, 0, 0);
            if (ob.transform.localPosition.x < -37)
                ob.transform.localPosition += new Vector3(72, 0, 0);
            if (ob.transform.localPosition.x > 35)
                ob.transform.localPosition -= new Vector3(72, 0, 0);
        }
        previous = player.position.x;

        //Шкала життя
        float currentLife = playerHp / maxHp;
        plHpBarMask.localPosition = new Vector3(minX + ((maxX - minX) * currentLife), plHpBarMask.localPosition.y, plHpBarMask.localPosition.z);
        plHpBarMask.localScale = new Vector3(maxScale * currentLife, plHpBarMask.localScale.y, 1);

        float currentStamina = playerSt / maxSt;
        plStBarMask.localPosition = new Vector3(minX + ((maxX - minX) * currentStamina), plStBarMask.localPosition.y, plStBarMask.localPosition.z);
        plStBarMask.localScale = new Vector3(maxScale * currentStamina, plStBarMask.localScale.y, 1);
    }
}

