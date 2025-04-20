using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin();
        }
        Destroy(this.gameObject);
    }
}
