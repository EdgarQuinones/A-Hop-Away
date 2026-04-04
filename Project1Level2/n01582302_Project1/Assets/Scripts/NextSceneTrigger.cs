using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string sceneName;
    // Changes to a scene based on the variable set
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sceneName == "Start")
        {
            GameManager.resetScore();
        }

        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
