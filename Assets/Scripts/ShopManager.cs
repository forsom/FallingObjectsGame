using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    [SerializeField] public int selectedCharacterIndex = 0; // зберігати цю змінну через player prefs 
    [SerializeField] private TMP_Text Money;
    public static ShopManager instance;
    void Start()
    {
        // CharacterSelection();
    }
    void Update()
    {
        Money.text = "Coins: " + PlayerPrefs.GetInt("UserCoins", 0);
    }
    // цей метод добавити на іншу сцену де будем змінювати префаб за станом індексу playerprefs get
    // private void CharacterSelection()
    // {
    //     foreach (GameObject character in characterPrefabs)
    //     {
    //         character.SetActive(false);
    //     }
    //     characterPrefabs[selectedCharacterIndex].SetActive(true);
    // }
}
