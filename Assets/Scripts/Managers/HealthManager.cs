using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    [SerializeField] private GameObject _heart2;
    [SerializeField] private GameObject _heart3;
    [SerializeField] private GameObject _healthBar;
    private int _maxHealth = 3;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        HeartsUpdate();
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
        _heart2.SetActive(_maxHealth >= 2);
        _heart3.SetActive(_maxHealth >= 3);
        _healthBar.SetActive(_maxHealth > 0);
    }
}
