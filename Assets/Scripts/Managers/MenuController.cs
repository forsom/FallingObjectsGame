using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _characterShop;

    // public void ShopButtonCick()
    // {
    //     _mainMenu.SetActive(false);
    //     _characterShop.SetActive(true);
    // }
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
