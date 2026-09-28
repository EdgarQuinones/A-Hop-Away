using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int value = 1;
    // Player gets points for collecting coins, 
    // with some having different values

    [SerializeField] AudioClip pickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(pickup);
            GameManager.addScore(value);
            Destroy(gameObject);
        }
    }
}
