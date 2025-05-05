using System.Collections.Generic;
using UnityEngine;

public class BobmFallControler : MonoBehaviour
{
    [SerializeField] private CharacterSelector characterSelector;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _wait = 0.8f;
    [SerializeField] private float _waitDecrease = 0.05f;
    [SerializeField] private float _increaseDifficultyInterval = 5f;
    [SerializeField] private GameObject _bombObject;
    private List<GameObject> _spawnedBombsList = new List<GameObject>();
    private float _timeSinceLastIncreasedDiffuculty;
    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(BombSpawner());
        
    }
    void Update()
    {
        IncreaseDifficulty();
    }

    public void RandomDirectionSpawn()
    {
        GameObject newobject = Instantiate(_bombObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedBombsList.Add(newobject);
    }
    private System.Collections.IEnumerator BombSpawner()
    {
        if (characterSelector.GetActivePlayerMovement() != null && _bombObject != null)
        {
            characterSelector.GetActivePlayerMovement().PlayerDied += StopBombsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(_wait);
            }
        }
    }
    private void StopBombsFall()
    {
        if (characterSelector.GetActivePlayerMovement() != null)
        {
            _isSpawning = false;
            characterSelector.GetActivePlayerMovement().PlayerDied -= StopBombsFall;
            HideBombs();
        }
    }

    private void HideBombs()
    {
        foreach (GameObject obj in _spawnedBombsList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        _spawnedBombsList.Clear();
    }
    private void IncreaseDifficulty()
    {
        _timeSinceLastIncreasedDiffuculty += Time.deltaTime;
        if (_timeSinceLastIncreasedDiffuculty >= _increaseDifficultyInterval)
        {
            _wait = Mathf.Max(_waitDecrease,_wait - _waitDecrease);
            _timeSinceLastIncreasedDiffuculty = 0f;
        }
    }
}

