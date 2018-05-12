using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectTracking : MonoBehaviour {

    public TrackedAssets trackedAssets;
    public TrackedHistory trackedAssetHistory;
    public GameObject[] TrackedItem;


	void Start () {
	}

    void Update()
    {
        //Update the stored value
        SetCurrentTracked();

    }

    void SetCurrentTracked()
    {
        for (int i = 0 ; i < trackedAssets.assets.Length; i++){
            trackedAssets.assets[i].locations.location = TrackedItem[i].transform.position;
            trackedAssets.assets[i].locations.scale = TrackedItem[i].transform.localScale;
            trackedAssets.assets[i].locations.rotation = TrackedItem[i].transform.eulerAngles;
            trackedAssets.assets[i].name = TrackedItem[i].name;
        }
    }

	// Set current transform to TrackedAsset
	void SetTrackedAssetTransform(int i)
    {
        trackedAssets.assets[i].locations.location = TrackedItem[i].transform.position;
        trackedAssets.assets[i].locations.scale = TrackedItem[i].transform.localScale;
        trackedAssets.assets[i].locations.rotation = TrackedItem[i].transform.eulerAngles;
    }

    // Get current transform from TrackedAsset
    void GetTrackedAssetTransform()
    {
        //if (SetObjectTransforms)
        //{
            for (int i = 0; i < trackedAssets.assets.Length; i++)
            {
                TrackedItem[i].transform.position = trackedAssets.assets[i].locations.location;
                TrackedItem[i].transform.localScale = trackedAssets.assets[i].locations.scale;
                TrackedItem[i].transform.eulerAngles = trackedAssets.assets[i].locations.rotation;
            }
        //}
    }
    // Save transforms to json

}