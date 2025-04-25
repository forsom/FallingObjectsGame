using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public event Action PlayerDied;
    public static PlayerMovement instance;

    [SerializeField] private float _moveSpeed = 10f;
    private float _horizontalMovement;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject healthBar;
    private int _maxHealth = 3;
    void Awake()
    {

        instance = this;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontalMovement * _moveSpeed, rb.velocity.y);
        anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        UpdateSpriteDirection();

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            _maxHealth--;
            SoundManager.PlaySound(SoundType.HIT);
            HeartsUpdate();
            if (_maxHealth > 0)
            {
                anim.ResetTrigger("damage");
                anim.SetTrigger("damage");

                Destroy(this.gameObject);
                PlayerDied.Invoke();
            }
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            HeartsUpdate();
        }
    }
    public void SetHealth(int newHealth)
    {
        _maxHealth = Mathf.Clamp(newHealth, 0, 3);
        HeartsUpdate();

    }

    public int GetHealth()
    {
        return _maxHealth;
    }

    private void HeartsUpdate()
    {
        heart2.SetActive(_maxHealth >= 2);
        heart3.SetActive(_maxHealth >= 3);
        healthBar.SetActive(_maxHealth > 0);
    }
}

