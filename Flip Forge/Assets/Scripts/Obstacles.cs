using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnDamageTaken(); // Notify the GameManager that money has been collected
        }
    }
}
