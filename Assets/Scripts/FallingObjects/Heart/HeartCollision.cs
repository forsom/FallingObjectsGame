using UnityEngine;

public class HeartCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int currentHealth = PlayerMovement.instance.GetHealth();
            PlayerMovement.instance.SetHealth(currentHealth + 1);
        }
        Destroy(this.gameObject);
    }
}
