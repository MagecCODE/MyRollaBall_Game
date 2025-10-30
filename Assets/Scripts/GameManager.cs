using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;


    
    public void CompleteLevel()
    { 
        Debug.Log("LEVEL COMPLETE!");
        completeLevelUI.SetActive(true);
    }

    public void EngGame()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
            Invoke("RestartGame", restartDelay);
        }

    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
