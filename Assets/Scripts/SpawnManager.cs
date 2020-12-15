using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;

    private bool isPlayerDied = false;
    void Start()
    {
        StartCoroutine(SpawnEnemeyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemeyRoutine()
    {
        while (isPlayerDied == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f,8f), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (isPlayerDied == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerUps[Random.Range(0,3)], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void OnPlayerDead()
    {
        isPlayerDied = true;
    }
}
