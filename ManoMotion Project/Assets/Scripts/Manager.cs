using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public Transform testSpawn;

    public GameObject holdAnimation;

    public ManoGestureTrigger shootGesture;
    public ManoGestureTrigger closeHand;

    private bool reloaded;
    private bool reloading;

    // Use this for initialization
    void Start()
    {
        var projectile = (GameObject)Instantiate(projectilePrefab, testSpawn.position, testSpawn.rotation);
        reloading = false;
        reloaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        TrackingInfo trackingInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        ManoGestureTrigger someGesture = gesture.mano_gesture_trigger;
        //Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;
        ContinuousGesture(gesture, trackingInfo);
        ShootProjectiles(someGesture);

        if(reloading == true)
        {
        }
    }

    void ContinuousGesture(GestureInfo gesture, TrackingInfo tracking)
    {
        if (gesture.mano_gesture_continuous == ManoGestureContinuous.CLOSED_HAND_GESTURE)
        {
            if(reloading == false)
            {
                reloading = true;
                Invoke("Reload", 1f);
            }
            //Handheld.Vibrate();
            Vector3 boundingBoxCenter = tracking.bounding_box_center;
            boundingBoxCenter.z = 10f;
            projectileSpawn.transform.position = Camera.main.ViewportToWorldPoint(boundingBoxCenter);
            holdAnimation.transform.position = Camera.main.ViewportToWorldPoint(boundingBoxCenter);
        }
    }

    void ShootProjectiles(ManoGestureTrigger someGesture)
    {
        if (someGesture == shootGesture && reloaded == true)
        {
            holdAnimation.SetActive(false);
            var projectile = (GameObject)Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            reloaded = false;
            reloading = false;
        }
    }
    
    void Reload()
    {
        holdAnimation.SetActive(true);
        reloaded = true;
    }

}
