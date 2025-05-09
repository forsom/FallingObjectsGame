using UnityEngine;

public class BombCollision : MonoBehaviour
{
    private Animator _cameraAnim;

    private void Start()
    {
        _cameraAnim = Camera.main.GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))
        {
            _cameraAnim.SetTrigger("shake");
            SoundManager.PlaySound(SoundType.BOMBEXPLOSION);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
