using UnityEngine;

public class BombCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Background")
        {
            Destroy(this.gameObject);
        }
    }
}
