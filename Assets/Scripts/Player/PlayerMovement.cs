using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action PlayerDied;
    public static PlayerMovement instance;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private HealthManager healthManager;
    // [SerializeField] private GameObject heart2;
    // [SerializeField] private GameObject heart3;
    // [SerializeField] private GameObject healthBar;

    private Rigidbody2D rb;
    private Animator anim;
    private float _horizontalMovement;
    // private int _maxHealth = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
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
            // _maxHealth--;
            int currentHealth = healthManager.GetHealth();
            currentHealth--;
            healthManager.SetHealth(currentHealth);
            SoundManager.PlaySound(SoundType.HIT);
            anim.SetTrigger("damage");
            // HeartsUpdate();
            // if (_maxHealth == 0)
            if (currentHealth == 0)
            {
                anim.ResetTrigger("damage");
                PlayerDied.Invoke();
                Destroy(this.gameObject);
            }
            
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            // HealthManager.HeartsUpdate();
            int currentHealth = healthManager.GetHealth();
            currentHealth++;
            healthManager.SetHealth(currentHealth);
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }
    // public void SetHealth(int newHealth)
    // {
    //     _maxHealth = Mathf.Clamp(newHealth, 0, 3);
    //     HeartsUpdate();
    // }
    // public int GetHealth()
    // {
    //     return _maxHealth;
    // }

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
    // private void HeartsUpdate()
    // {
    //     heart2.SetActive(_maxHealth >= 2);
    //     heart3.SetActive(_maxHealth >= 3);
    //     healthBar.SetActive(_maxHealth > 0);
    // }
}

