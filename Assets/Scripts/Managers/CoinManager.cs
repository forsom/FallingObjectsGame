using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinsCount;
    public static CoinManager instance;
    private const string CoinsAmount = "UserCoins";
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            coinsCount = PlayerPrefs.GetInt(CoinsAmount, 0);
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCoin()
    {
        coinsCount++;
        CoinsSave();
    }
    private void CoinsSave()
    {
        PlayerPrefs.SetInt(CoinsAmount, coinsCount);
        PlayerPrefs.Save();
    }
}
