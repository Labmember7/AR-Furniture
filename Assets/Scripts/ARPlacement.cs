using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARPlacement : MonoBehaviour
{

    public GameObject toSpawnObject;
    public GameObject indicator;

    private Pose placementPose;
    private ARRaycastManager aRRaycast;
    private bool placementPoseIsValid = false;


    void Start()
    {
        aRRaycast = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if(placementPoseIsValid && Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SpawnARObject();
        }
        UpdateIndicatorPose();
        UpdateIndicator();
    }
    void UpdateIndicatorPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycast.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }
    void UpdateIndicator()
    {
        if(placementPoseIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            indicator.SetActive(false);

        }
    }
    void SpawnARObject()
    {
        Instantiate(toSpawnObject, placementPose.position, placementPose.rotation);
    }



}
