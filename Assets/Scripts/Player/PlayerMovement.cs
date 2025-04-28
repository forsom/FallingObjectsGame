using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action PlayerDied;

    [SerializeField] private float _moveSpeed = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private float _horizontalMovement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontalMovement * _moveSpeed, rb.velocity.y);
        anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        UpdateSpriteDirection();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (HealthManager.instance != null)
            {
                int currentHealth = HealthManager.instance.GetHealth();
                currentHealth--;
                HealthManager.instance.SetHealth(currentHealth);
                SoundManager.PlaySound(SoundType.HIT);
                anim.SetTrigger("damage");
                if (currentHealth == 0)
                {
                    anim.ResetTrigger("damage");
                    PlayerDied?.Invoke();
                    Destroy(this.gameObject);
                }
            }
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            int currentHealth = HealthManager.instance.GetHealth();
            currentHealth++;
            HealthManager.instance.SetHealth(currentHealth);
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    private void UpdateSpriteDirection()
    {
        // Якщо рухаємося вправо (_horizontalMovement > 0)
        if (_horizontalMovement > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Нормальний масштаб
        }
        // Якщо рухаємося вліво (_horizontalMovement < 0)
        else if (_horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Дзеркальний масштаб по X
        }
        // Якщо _horizontalMovement == 0, залишаємо поточний напрямок
    }

}

