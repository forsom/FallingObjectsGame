using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class ObjectFallControler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float minX = -20f;
    [SerializeField] float maxX = 20f;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;
    public GameObject fallingObject;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    public float _wait = 0.8f;


    void Start()
    {
        if (playerMovement != null && fallingObject != null)
        {
            playerMovement.PlayerDied += StopObjectsFall;
            InvokeRepeating("RandomFall", _wait, _wait);
        }
    }
    private void RandomFall()
    {
        GameObject newobject = Instantiate(fallingObject, new Vector3(Random.Range(minX, maxX), 15, 0), Quaternion.identity);
        spawnedObjects.Add(newobject);
    }
    void StopObjectsFall()
    {
        if (playerMovement != null)
        {
            CancelInvoke("RandomFall");
            playerMovement.PlayerDied -= StopObjectsFall;
            HideObjects();
        }
    }
    public void ChangeGameDiffuculty0()
    {
        Debug.Log("0");
        _wait = 0.8f;
        InvokeRepeating("RandomFall", _wait, _wait);
        HideObjects();
    }
    public void ChangeGameDiffuculty1()
    {
        _wait = 0.4f;
        InvokeRepeating("RandomFall", _wait, _wait);
        HideObjects();
    }
    public void ChangeGameDiffuculty2()
    {
        _wait = 0.2f;
        InvokeRepeating("RandomFall", _wait, _wait);
        HideObjects();
    }
    public void HideObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        spawnedObjects.Clear();
    }
}

