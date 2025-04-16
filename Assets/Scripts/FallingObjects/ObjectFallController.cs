using System.Collections.Generic;
using UnityEngine;

public class ObjectFallControler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _wait = 0.8f;
    [SerializeField] private float _waitDecrease = 0.05f;
    [SerializeField] private float _increaseDifficultyInterval = 5f;
    public GameObject fallingObject;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private float _timeSinceLastIncreasedDiffuculty;
    private bool _isSpawning = true;


    private void Start()
    {
        StartCoroutine(Spawner());
    }
    void Update()
    {
        IncreaseDifficulty();
    }

    private void RandomDirectionSpawn()
    {
        GameObject newobject = Instantiate(fallingObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedObjects.Add(newobject);
    }
    private System.Collections.IEnumerator Spawner()
    {
        if (playerMovement != null && fallingObject != null)
        {
            playerMovement.PlayerDied += StopObjectsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(_wait);
            }
        }
    }
    private void StopObjectsFall()
    {
        if (playerMovement != null)
        {
            _isSpawning = false;
            playerMovement.PlayerDied -= StopObjectsFall;
            HideObjects();
        }
    }

    private void HideObjects()
    {
        foreach (GameObject obj in _spawnedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        _spawnedObjects.Clear();
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

