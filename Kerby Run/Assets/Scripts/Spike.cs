using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player hits a spike, they get sent back to level 1
        // and their score resets back to 0
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.resetScore();
            SceneManager.LoadScene("Start");
        }
    }
}
