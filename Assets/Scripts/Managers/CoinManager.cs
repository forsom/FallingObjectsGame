using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager instance;
    private const string CoinsAmount = "UserCoins";
    public int coinsCount;
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
        Debug.Log(coinsCount);
    }
    private void CoinsSave()
    {
        PlayerPrefs.SetInt(CoinsAmount, coinsCount);
        PlayerPrefs.Save();
    }
}
