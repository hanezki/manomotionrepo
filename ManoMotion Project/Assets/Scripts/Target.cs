using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private GameObject _gameManager;

	// Use this for initialization
	void Start () {
        _gameManager = GameObject.FindGameObjectWithTag("manager");
        _gameManager.GetComponent<UIhandler>().AddTarget(this.gameObject);
	}
}
