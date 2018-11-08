using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExampleSpooky : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform projectileSpawn;

    public ManoGestureTrigger shootGesture;
    public ManoGestureTrigger closeHand;

    private bool reloaded;

    // Use this for initialization
    void Start()
    {
        reloaded = false;
        print(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        ManoGestureTrigger someGesture = gesture.mano_gesture_trigger;
        //TrackingInfo tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        //Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        ShootProjectiles(someGesture);
    }

    /// <summary>
    /// Based on the continuous gesture performed (Open hand or Closed Hand) the ghost will change its appearance
    /// </summary>
    /// <param name="gesture">Gesture.</param>
    /// <param name="warning">Warning.</param>
    void ShootProjectiles(ManoGestureTrigger someGesture)
    {

        if (someGesture == closeHand && reloaded == false)
        {
            reloaded = true;
        }
        if (someGesture == shootGesture && reloaded == true)
        {
            var projectile = (GameObject)Instantiate(projectilePrefab,projectileSpawn.position,projectileSpawn.rotation);
            reloaded = false;
        }
    }

    
}
