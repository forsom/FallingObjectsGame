using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin();
            SoundManager.PlaySound(SoundType.COINPICKUP);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Background"))
        {
            Destroy(this.gameObject);
        }
    }
}
