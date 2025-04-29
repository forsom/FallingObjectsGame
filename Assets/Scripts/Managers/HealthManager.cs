using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject healthBar;
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
    private void HeartsUpdate()
    {
        heart2.SetActive(_maxHealth >= 2);
        heart3.SetActive(_maxHealth >= 3);
        healthBar.SetActive(_maxHealth > 0);
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
}
