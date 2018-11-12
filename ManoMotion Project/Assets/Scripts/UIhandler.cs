using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour {

    float timer;
    public Text time;
    public Text targetsText;
    public List<GameObject> targets = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        targetsText.text = "5";
        StartCoroutine(Example());
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void AddTarget(GameObject target)
    {
        targets.Add(target);
    }
    
    public void RemoveTarget()
    {
        targets.RemoveAt(targets.Count-1);
        //targetsText.text = "1";
        targetsText.text = targets.Count.ToString();
    }

    IEnumerator Example()
    {
        while(true)
        {
            if(targets.Count != 0)
            {
                print(Time.time);
                time.text = Time.time.ToString("0");
                print(Time.time);
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield break;
            }
        }

    }
}
