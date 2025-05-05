using UnityEngine;

public class HeartCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int currentHealth = HealthManager.instance.GetHealth();
            HealthManager.instance.SetHealth(currentHealth++);
            SoundManager.PlaySound(SoundType.HEARTPICKUP);
        }
        Destroy(this.gameObject);
    }
}
