using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private GameObject _gameManager;
    private Transform _camera;
	// Use this for initialization
	void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        _gameManager = GameObject.FindGameObjectWithTag("manager");
        _gameManager.GetComponent<UIhandler>().AddTarget(this.gameObject);
	}

    private void Update()
    {
        transform.LookAt(_camera);
    }
}
