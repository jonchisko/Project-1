using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private int _scoreAmount = 2;

    private float _minY = -6.0f;
    private float _maxY = 8.0f;
    private float _minXRand = -8.0f;
    private float _maxXRand = 8.0f;


    private Animator _animController;
    private BoxCollider2D _boxCol;
    private AudioSource _aSource;
    // Start is called before the first frame update
    void Start()
    {
        _animController = GetComponent<Animator>();
        _boxCol = GetComponent<BoxCollider2D>();
        _aSource = GetComponent<AudioSource>();

        if(_animController == null)
        {
            Debug.LogError("EnemyScript::Start -> _animController is missing.");
        }
        if(_boxCol == null)
        {
            Debug.LogError("EnemyScript::Start -> _boxCol is missing.");
        }
        if(_aSource == null)
        {
            Debug.LogError("EnemyScript::Start() -> _aSource is missing.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();   
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.position = OutOfBounds();
    }

    Vector3 OutOfBounds()
    {
        Vector3 result = transform.position;
        if(transform.position.y <= _minY)
        {
            result.y = _maxY;
            // randomize x
            result.x = Random.Range(_minXRand, _maxXRand);
        }
        return result;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HitPlayer(other);
        HitBullet(other);
    }

    void HitPlayer(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enemy hit " + other.transform.tag);
            IncreaseScore();
            // damage the player
            _animController.SetTrigger("EnemyDestroyed");
            _boxCol.enabled = false;
            _aSource.Play();
            _speed = 0;
            other.GetComponent<PlayerScript>()?.TakeLife();
            Destroy(this.gameObject, 2.8f);
        }
    }

    void HitBullet(Collider2D other)
    {
        if (other.tag == "Bullet1")
        {
            Debug.Log("Enemy hit " + other.transform.tag);
            IncreaseScore();
            _animController.SetTrigger("EnemyDestroyed");
            _boxCol.enabled = false;
            _aSource.Play();
            _speed = 0;
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.8f);
        }
    }

    void IncreaseScore()
    {
        PlayerScript.IncreaseScore(_scoreAmount);
    }

}
