using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet1;
    [SerializeField]
    private GameObject _tripleShot;

    // fire rate / CD
    [SerializeField]
    private float _fireCD = 0.2f;
    [SerializeField]
    private GameObject _bulletContainer;

    private float _canFire = -1f;
    [SerializeField]
    private float _tripleShotTimer = 3.0f;
    [SerializeField]
    private bool _tripleShotEnabled = false;

    private Coroutine coroutine;

    // Audio
    private AudioSource _aSource;


    // Start is called before the first frame update
    void Start()
    {
        _aSource = GetComponent<AudioSource>();
        if(_aSource == null)
        {
            Debug.LogError("WeaponScript::Start() -> AudioSource component missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
    }

    void FireWeapon()
    {
        // laser firing
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire)
        {
            Fire();
            _aSource.Play();
        }
    }

    void Fire()
    {
        _canFire = Time.time + _fireCD;
        GameObject projectile;
        if (_tripleShotEnabled)
        {
            projectile = Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            projectile = Instantiate(_bullet1, transform.position, Quaternion.identity); 
        }
        projectile.transform.parent = _bulletContainer.transform;
    }


    public void SetTripleShotOn()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(TripleShotActiveTimer());
    }

    IEnumerator TripleShotActiveTimer()
    {
        _tripleShotEnabled = true;
        yield return new WaitForSeconds(_tripleShotTimer);
        _tripleShotEnabled = false;
    }

}
