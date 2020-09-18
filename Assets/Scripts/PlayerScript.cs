using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // event
    public delegate void OnDeath();
    public static OnDeath playerDiedEvent;
    public delegate void OnScoreChange(int score);
    public static OnScoreChange playerScoreEvent;
    public static OnScoreChange playerLifeEvent;

    // speed variable
    [SerializeField]
    private float _speedVariable = 1.0f;
    [SerializeField]
    private float _bonusSpeed = 5.0f;
    private float _tmpSpeed;
    [SerializeField]
    private float _durationBonusSpeed = 3.0f;
    private Coroutine _speedCoroutine;

    // shields
    private bool _shieldActive = false;

    [SerializeField]
    private GameObject _shieldVisual;

    // engine damage
    private GameObject _rightEngineDamage;
    private GameObject _leftEngineDamage;

    // variables to limit player's movement
    private float _maxY = 6.0f;
    private float _minY = -4.0f;
    private float _maxX = 11.0f;
    private float _minX = -11.0f;

    // number of lives
    [SerializeField]
    private int _lives = 3;

    // Explosion
    [SerializeField]
    private GameObject _explosion;



    // you could set up this in start tho
    private static int _playerScore = 0;




    // Start is called before the first frame update
    void Start()
    {
        // set player at 0, 0, 0 at the beginning!  
        transform.position = new Vector3(0, 0, 0);
        _tmpSpeed = _speedVariable;
        _shieldVisual.SetActive(false);
        
        _rightEngineDamage = transform.Find("RightEngineDamage").gameObject;
        _leftEngineDamage = transform.Find("LeftEngineDamage").gameObject;
        if(_explosion == null)
        {
            Debug.LogError("Explosion is null in player script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }




    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = (Vector3.right * horizontalInput + Vector3.up * verticalInput).normalized;
        transform.Translate(movementDirection * _speedVariable * Time.deltaTime);
        transform.position = RestrictMovement(transform.position);
    }

    Vector3 RestrictMovement(Vector3 currentPosition)
    {
        Vector3 result = currentPosition;
        if(currentPosition.y >= _maxY)
        {
            result.y = _maxY;
        }
        else if(currentPosition.y <= _minY)
        {
            result.y = _minY;
        }

        if(currentPosition.x >= _maxX)
        {
            result.x = _minX + 1.0f;
        }
        else if(currentPosition.x <= _minX)
        {
            result.x = _maxX - 1.0f;
        }

        return result;

    }

    public void IncreaseSpeedPowerUp()
    {
        if (_speedCoroutine != null) StopCoroutine(_speedCoroutine);
        _speedCoroutine = StartCoroutine("SpeedPowerUpCoroutine");
    }

    public void SetShieldsOn()
    {
        _shieldActive = true;
        _shieldVisual.SetActive(true);
    }

    IEnumerator SpeedPowerUpCoroutine()
    {
        _speedVariable = _bonusSpeed;
        yield return new WaitForSeconds(_durationBonusSpeed);
        _speedVariable = _tmpSpeed;
    }

    public static void IncreaseScore(int amount)
    {
        _playerScore += amount;
        playerScoreEvent?.Invoke(_playerScore);
    }

    public void TakeLife()
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shieldVisual.SetActive(false);
        }
        else
        {
            _lives--;
            
            if (_lives == 2) _rightEngineDamage.SetActive(true);
            if (_lives == 1) _leftEngineDamage.SetActive(true);

            playerLifeEvent?.Invoke(_lives);
            if (_lives < 1)
            {
                playerDiedEvent?.Invoke();
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

}
