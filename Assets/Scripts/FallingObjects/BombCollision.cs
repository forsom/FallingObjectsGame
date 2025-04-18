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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Background")
        {
            _cameraAnim.SetTrigger("shake");
            Destroy(this.gameObject);
        }
    }
}
