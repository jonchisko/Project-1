using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireScript : MonoBehaviour
{
    [SerializeField]
    private float fireCD = 1.0f;
    [SerializeField]
    private GameObject _laser;

    private bool _canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        if(_laser == null)
        {
            Debug.LogError("EnemyFireScript::Start() -> _laser is missing.");
        }
        StartCoroutine("FireLaserCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FireLaserCoroutine()
    {
        while (_canFire)
        {
            FireGun();
            yield return new WaitForSeconds(fireCD);
        }
    }


    void FireGun()
    {
        GameObject a = Instantiate(_laser, transform.position, transform.rotation) as GameObject;
        if (a == null) Debug.LogError("EnemyFireScript::FireGun() -> _laser was not instantiated correctly.");
        a.transform.Rotate(Vector3.forward, 180);
    }

    public void SetCanFire(bool value)
    {
        _canFire = value;
    }

}
