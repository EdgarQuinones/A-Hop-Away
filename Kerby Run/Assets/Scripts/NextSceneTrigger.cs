using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioClip nextLevel;
    // Changes to a scene based on the variable set
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sceneName == "Start")
        {
            GameManager.resetScore();
        }

        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(nextLevel);
            SceneManager.LoadScene(sceneName);
        }
    }
}
