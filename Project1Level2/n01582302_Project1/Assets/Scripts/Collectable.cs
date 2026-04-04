using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value = 1;
    // Player gets points for collecting coins, 
    // with some having different values
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.addScore(value);
            Destroy(gameObject);
        }
    }
}
