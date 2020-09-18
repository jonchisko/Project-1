using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _waitFor = 2.0f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;

    private Coroutine _spawnCoroutine;
    private Coroutine _spawnPowerUpCoroutine;

    [SerializeField]
    private float _powerUpSpawnerTimer1 = 3.0f;
    [SerializeField]
    private float _powerUpSpawnerTimer2 = 6.0f;

    private float _maxY = 8.0f;
    private float _minXRand = -8.0f;
    private float _maxXRand = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawn manager SpawnCoroutine started!");
        PlayerScript.playerDiedEvent += StopSpawnCoroutine;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawnCoroutines()
    {
        StartCoroutine("StartAllCoroutines");
    }


    IEnumerator StartAllCoroutines()
    {
        yield return new WaitForSeconds(2.0f);
        _spawnCoroutine = StartCoroutine("SpawnCoroutine");
        _spawnPowerUpCoroutine = StartCoroutine("SpawnPowerUpCoroutine");
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, RandomLocation(), Quaternion.identity);
            // put new enemy into container in hierarchy
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitFor);
        }
    }

    IEnumerator SpawnPowerUpCoroutine()
    {
        while (true)
        {
            GameObject powerUp = Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], RandomLocation(), Quaternion.identity);
            powerUp.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(_powerUpSpawnerTimer1, _powerUpSpawnerTimer2));
        }
    }

    Vector3 RandomLocation()
    {
        return new Vector3(Random.Range(_minXRand, _maxXRand), _maxY, 0);
    }

    void StopSpawnCoroutine()
    {
        Debug.Log("Spawn manager SpawnCoroutine stopped!");
        if(_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
        if(_spawnPowerUpCoroutine != null) StopCoroutine(_spawnPowerUpCoroutine);
    }

    private void OnDisable()
    {
        StopSpawnCoroutine();
        PlayerScript.playerDiedEvent -= StopSpawnCoroutine;
    }

}
