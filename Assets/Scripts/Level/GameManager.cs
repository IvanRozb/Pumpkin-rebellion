using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameIsEnded = false;
    public float restartDelay = 1f;
    public GameObject restartMenuUI;
    public void EndGame()
    {
        if (gameIsEnded == false)
        {
            gameIsEnded = true;
            Debug.Log("wdadwadwadwa");
            Invoke("Restart", restartDelay);
        }
    }
    public void SetMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SetLevelScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
    public void Restart()
    {
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        FindObjectOfType<AudioManager>().PlaySound("PlayerDeath");
        restartMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
 