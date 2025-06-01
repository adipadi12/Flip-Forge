using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnMoneyCollected(); // Notify the GameManager that money has been collected
            Destroy(gameObject); // Destroy the collectible object
        }
    }
}
