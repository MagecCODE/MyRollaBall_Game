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
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    /*
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */
}
