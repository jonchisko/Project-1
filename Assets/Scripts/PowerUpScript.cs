using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    enum PowerUpType
    {
        Triple,
        Speed,
        Shield,
    }



    [SerializeField]
    private float _speed = 4.0f;
    private float _minY = -7.0f;

    [SerializeField]
    private PowerUpType _powerUpType = PowerUpType.Triple;

    [SerializeField]
    private AudioClip _aClip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }


    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= _minY) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_aClip, transform.position);
            switch (_powerUpType)
            {
                case PowerUpType.Shield: collision.transform.GetComponent<PlayerScript>().SetShieldsOn(); break;
                case PowerUpType.Triple: collision.transform.Find("Gun")?.GetComponent<WeaponScript>()?.SetTripleShotOn(); break;
                case PowerUpType.Speed: collision.transform.GetComponent<PlayerScript>()?.IncreaseSpeedPowerUp(); break;
            }
            Destroy(this.gameObject);
        }
    }

}
