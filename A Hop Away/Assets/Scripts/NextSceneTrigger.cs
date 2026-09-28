using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioClip nextLevel;
    [SerializeField] private AudioClip gameWin;
    [SerializeField] private bool playSounds = true;

    // Changes to a scene based on the variable set
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sceneName == "Start")
        {
            GameManager.resetScore();
        }

        if (collision.CompareTag("Player"))
        {   

            if (playSounds)
            {
                if (sceneName != "End")
                {
                    AudioManager.Instance.PlaySound(nextLevel);
                } else
                {
                    AudioManager.Instance.PlaySound(gameWin);
                }
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
