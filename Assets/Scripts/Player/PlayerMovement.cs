using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public event Action PlayerDied;

    [SerializeField] private float _moveSpeed = 10f;
    private float _horizontalMovement;
    private int _maxHealth = 3;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject healthBar;
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
        if (collision.gameObject.CompareTag("Object"))
        {
            _maxHealth--;
            SoundManager.PlaySound(SoundType.HIT);
            HeartsUpdate();
            if (_maxHealth > 0)
            {
                anim.ResetTrigger("damage");
                anim.SetTrigger("damage");
            }
            else if (_maxHealth == 0)
            {

                Destroy(this.gameObject);
                PlayerDied.Invoke();
            }
        }
    }
    private void HeartsUpdate()
    {
        if (_maxHealth == 2)
        {
            heart3.SetActive(false); // Вимикаємо heart3, залишаємо 2 серця
        }
        else if (_maxHealth == 1)
        {
            heart1.SetActive(false); // Вимикаємо heart2, залишаємо 1 серце
        }
        else if (_maxHealth == 0)
        {
            healthBar.SetActive(false); // Вимикаємо HealthBar закінчились серця
        }
    }
}
