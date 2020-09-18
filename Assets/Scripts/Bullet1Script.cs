using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Script : MonoBehaviour
{
    // bullet 1 speed
    [SerializeField]
    private float _bulletSpeed = 5.0f;

    // max y bullet can reach before it is destroyed
    private float _maxY = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        DestroyBullet();
    }

    void CalculateMovement()
    {
        Vector3 direction = Vector3.up;
        transform.Translate(direction * _bulletSpeed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        // clean the bullet if out of game world
        if(transform.position.y >= _maxY)
        {
            if (this.transform.parent != null && this.transform.parent.name == "TripleShot(Clone)") Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

}
