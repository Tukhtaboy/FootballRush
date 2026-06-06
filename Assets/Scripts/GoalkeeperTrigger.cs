using UnityEngine;

public class GoalkeeperTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().PlayerCaught();
        }
    }
}