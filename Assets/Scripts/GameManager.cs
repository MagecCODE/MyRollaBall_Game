using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public float restartDelay = 2f;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    
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
            gameOverUI.SetActive(true);
            Debug.Log("GAME OVER");
            Invoke("RestartGame", restartDelay);
        }
    }

    void RestartGame()
    {
        StartCoroutine(LoadNextLevelWithDelay());        
    }

    private IEnumerator LoadNextLevelWithDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
