using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation = 1.0f;

    [SerializeField]
    private GameObject _explosion;

    private SpawnManagerScript _spawnManager;




    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();

        if(_spawnManager == null)
        {
            Debug.LogError("AsteroidScript::Start() -> _spawnManager not found.");
        }

        if(_explosion == null)
        {
            Debug.LogError("AsteroidScript::Start() -> explosion prefab is missing.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateAroundZ();
    }

    void RotateAroundZ()
    {
        transform.Rotate(Vector3.forward, _speedRotation * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet1")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.StartSpawnCoroutines();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
