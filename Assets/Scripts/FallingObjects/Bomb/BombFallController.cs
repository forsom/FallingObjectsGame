using System.Collections.Generic;
using UnityEngine;

public class BobmFallControler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _wait = 0.8f;
    [SerializeField] private float _waitDecrease = 0.05f;
    [SerializeField] private float _increaseDifficultyInterval = 5f;
    public GameObject bombObject;
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
        GameObject newobject = Instantiate(bombObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedBombsList.Add(newobject);
    }
    private System.Collections.IEnumerator BombSpawner()
    {
        if (playerMovement != null && bombObject != null)
        {
            playerMovement.PlayerDied += StopBombsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(_wait);
            }
        }
    }
    private void StopBombsFall()
    {
        if (playerMovement != null)
        {
            _isSpawning = false;
            playerMovement.PlayerDied -= StopBombsFall;
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

