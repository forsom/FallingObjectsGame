using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add CoinPickUp Sound
            // Add Coin Counter 
        }
        Destroy(this.gameObject);
    }
}
