using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
    public float delay = 2f; // tiempo de espera en segundos

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelWithDelay());
    }

    private IEnumerator LoadNextLevelWithDelay()
    {
        // Using yield to wait for the specified delay and coroutine method
        yield return new WaitForSeconds(delay);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int maxScenesIndex = SceneManager.sceneCountInBuildSettings -1;

        if (nextSceneIndex <= maxScenesIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        // Load main menu if last level is completed
        if(SceneManager.GetActiveScene().buildIndex == maxScenesIndex)
        {
            Debug.Log("FINAL LEVEL!");
            SceneManager.LoadScene("Main Menu"); 
        }
    }
    
}
