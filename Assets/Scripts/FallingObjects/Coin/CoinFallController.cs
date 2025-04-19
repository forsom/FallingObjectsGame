using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CoinFallController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _minWait = 5f;
    [SerializeField] private float _maxWait = 10f;
    [SerializeField] private float initialDelay = 10f;
    private float _timeSinceGameStart;
    public GameObject coinObject;
    private List<GameObject> _spawnedCoinsList = new List<GameObject>();
    private bool _isSpawning = true;
    private void Start()
    {
        StartCoroutine(CoinSpawner());
    }

    public void RandomDirectionSpawn()
    {
        GameObject newobject = Instantiate(coinObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedCoinsList.Add(newobject);
    }
    private System.Collections.IEnumerator CoinSpawner()
    {
        if (playerMovement != null && coinObject != null)
        {
            yield return new WaitForSeconds(initialDelay);
            playerMovement.PlayerDied += StopCoinsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(Random.Range(_minWait, _maxWait));
            }
        }
    }
    private void StopCoinsFall()
    {
        if (playerMovement != null)
        {
            _isSpawning = false;
            playerMovement.PlayerDied -= StopCoinsFall;
            HideCoins();
        }
    }
    private void HideCoins()
    {
        foreach (GameObject obj in _spawnedCoinsList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        _spawnedCoinsList.Clear();
    }
}
