using System.Collections.Generic;
using UnityEngine;

public class CoinFallController : MonoBehaviour
{
    [SerializeField] private CharacterSelector characterSelector;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _minWait = 5f;
    [SerializeField] private float _maxWait = 10f;
    [SerializeField] private float initialDelay = 10f;
    [SerializeField] private GameObject _coinObject;
    private List<GameObject> _spawnedCoinsList = new List<GameObject>();
    private bool _isSpawning = true;
    private void Start()
    {
        StartCoroutine(CoinSpawner());
    }

    public void RandomDirectionSpawn()
    {
        GameObject newobject = Instantiate(_coinObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedCoinsList.Add(newobject);
    }
    private System.Collections.IEnumerator CoinSpawner()
    {
        if (characterSelector.GetActivePlayerMovement() != null && _coinObject != null)
        {
            yield return new WaitForSeconds(initialDelay);
            characterSelector.GetActivePlayerMovement().PlayerDied += StopCoinsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(Random.Range(_minWait, _maxWait));
            }
        }
    }
    private void StopCoinsFall()
    {
        if (characterSelector.GetActivePlayerMovement() != null)
        {
            _isSpawning = false;
            characterSelector.GetActivePlayerMovement().PlayerDied -= StopCoinsFall;
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
