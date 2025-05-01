using System.Collections.Generic;
using UnityEngine;

public class BombFallController : MonoBehaviour
{
    [SerializeField] private CharacterSelector characterSelector;
    [SerializeField] private float _minX = -20f;
    [SerializeField] private float _maxX = 20f;
    [SerializeField] private float _minWait = 10f;
    [SerializeField] private float _maxWait = 25f;
    [SerializeField] private float initialDelay = 15f;
    public GameObject heartObject;
    private List<GameObject> _spawnedHeartsList = new List<GameObject>();
    private bool _isSpawning = true;
    private void Start()
    {
        StartCoroutine(HeartsSpawner());
    }

    public void RandomDirectionSpawn()
    {
        GameObject newobject = Instantiate(heartObject, new Vector3(Random.Range(_minX, _maxX), 15, 0), Quaternion.identity);
        _spawnedHeartsList.Add(newobject);
    }
    private System.Collections.IEnumerator HeartsSpawner()
    {
        if (characterSelector.GetActivePlayerMovement() != null && heartObject != null)
        {
            yield return new WaitForSeconds(initialDelay);
            characterSelector.GetActivePlayerMovement().PlayerDied += StopHeartsFall;
            while (_isSpawning)
            {
                RandomDirectionSpawn();
                yield return new WaitForSeconds(Random.Range(_minWait, _maxWait));
            }
        }
    }
    private void StopHeartsFall()
    {
        if (characterSelector.GetActivePlayerMovement() != null)
        {
            _isSpawning = false;
            characterSelector.GetActivePlayerMovement().PlayerDied -= StopHeartsFall;
            HideHearts();
        }
    }
    private void HideHearts()
    {
        foreach (GameObject obj in _spawnedHeartsList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        _spawnedHeartsList.Clear();
    }
}
