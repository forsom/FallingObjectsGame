using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action PlayerDied;

    public int moveSpeed = 10;

    private Rigidbody2D _rb;
    private Animator _anim;
    private float _horizontalMovement;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMovement * moveSpeed, _rb.velocity.y);
        _anim.SetFloat("xVelocity", Math.Abs(_rb.velocity.x));
        UpdateSpriteDirection();

    }
    public void MoveLeft()
    {
        _horizontalMovement = -1f;
    }

    public void MoveRight()
    {
        _horizontalMovement = 1f;
    }
    public void StopMove()
    {
        _horizontalMovement = 0f;
    }
    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
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
                _anim.SetTrigger("damage");
                if (currentHealth == 0)
                {
                    _anim.ResetTrigger("damage");
                    PlayerDied?.Invoke();
                    Destroy(this.gameObject);
                }
            }
        }
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

