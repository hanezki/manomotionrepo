using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public GameObject target;

    public GameObject holdAnimation;

    public ManoGestureTrigger shootGesture;
    public ManoGestureTrigger closeHand;

    private List<GameObject> targetSpawns = new List<GameObject>();

    private bool reloaded;
    private bool reloading;
    private bool openHand;

    // Use this for initialization
    void Start()
    {
        openHand = false;
        var projectile = (GameObject)Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        reloading = false;
        reloaded = false;
        targetSpawns.AddRange(GameObject.FindGameObjectsWithTag("Spawn"));
        
        for(int i=0; i < targetSpawns.Count; i++)
        {
            Instantiate(target, targetSpawns[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TrackingInfo trackingInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        ManoGestureTrigger someGesture = gesture.mano_gesture_trigger;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;
        ContinuousGesture(gesture, trackingInfo, warning);
        ShootProjectiles(someGesture, warning);
    }

    void ContinuousGesture(GestureInfo gesture, TrackingInfo tracking, Warning warning)
    {
        if (warning != Warning.WARNING_HAND_NOT_FOUND)
        {
            //found hand
            if (gesture.mano_gesture_continuous == ManoGestureContinuous.CLOSED_HAND_GESTURE)
            {
                openHand = false;
                if (reloading == false)
                {
                    reloading = true;
                    Invoke("Reload", 1f);
                }
                Vector3 boundingBoxCenter = tracking.bounding_box_center;
                Vector3 projectileSpawnPoint = boundingBoxCenter;

                Vector3 holdAnimationPoint = boundingBoxCenter;
                holdAnimationPoint.z = 0.5f;

                projectileSpawnPoint.z = 5f;
                boundingBoxCenter.z = 3f;
                projectileSpawn.transform.position = Camera.main.ViewportToWorldPoint(projectileSpawnPoint);
                holdAnimation.transform.position = Camera.main.ViewportToWorldPoint(holdAnimationPoint);
            }
        }

        else
        {
            reloading = false;
            reloaded = false;
            openHand = false;
            holdAnimation.SetActive(false);
        }
    }

    /// <summary>
    /// Based on the continuous gesture performed (Open hand or Closed Hand) the ghost will change its appearance
    /// </summary>
    /// <param name="gesture">Gesture.</param>
    /// <param name="warning">Warning.</param>
    void ShootProjectiles(ManoGestureTrigger someGesture, Warning warning)
    {
        if(someGesture == shootGesture)
        {
            openHand = true;
            if (reloaded == true)
            {
                holdAnimation.SetActive(false);
                var projectile = (GameObject)Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
                reloaded = false;
                reloading = false;
            }
        }
       
    }
    
    void Reload()
    {
        if(!reloading == false && !openHand == true)
        {
            holdAnimation.SetActive(true);
            reloaded = true;
        }
        else
        {
            reloading = false;
        }
    }
}
