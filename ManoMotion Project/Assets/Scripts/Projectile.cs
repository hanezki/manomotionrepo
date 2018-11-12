using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody _rigidbody;
    public float speed;
    public GameObject explosion;
    private GameObject _gameManager;

    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("manager");
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
        Destroy(this.gameObject, 3f);
    }
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Explode();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.1f);
            _gameManager.GetComponent<UIhandler>().RemoveTarget();
        }
    }
}