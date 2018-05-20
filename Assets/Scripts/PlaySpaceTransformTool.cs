using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaySpaceTransformTool : MonoBehaviour {

    public SceneObjectTracking sot;
    public int CurrentObject = 0;
    public float MoveAmount = 0.1f;
    public int direction = 1;
    public TextMeshProUGUI Speed;

    // Use this for initialization
    void Start () {
        sot = this.GetComponent<SceneObjectTracking>();
        Speed.text = "Speed: " + MoveAmount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSpeed()
    {
        if (MoveAmount < 0.001f)
        {
            MoveAmount = 1.0f;
        }
        else
        {
            MoveAmount = MoveAmount * 0.1f;
        }

        Speed.text = "Speed: " + MoveAmount;
    }

    // move object (+-) x
    public void MoveTrackedTransformX(bool positiveX)
    {
        direction = 1;
        if (!positiveX)
            direction = -1;
        int i = CurrentObject;
        float moveX = sot.TrackedItem[i].transform.position.x + ( MoveAmount * direction );
        sot.TrackedItem[i].transform.position = new Vector3(moveX, sot.TrackedItem[i].transform.position.y, sot.TrackedItem[i].transform.position.z);
    }

    // move object (+-) y
    public void MoveTrackedTransformY(bool positiveY)
    {
        direction = 1;
        if (!positiveY)
            direction = -1;

        int i = CurrentObject;
        float moveY = sot.TrackedItem[i].transform.position.y + (MoveAmount * direction);
        sot.TrackedItem[i].transform.position = new Vector3(sot.TrackedItem[i].transform.position.x, moveY, sot.TrackedItem[i].transform.position.z);
    }

    // move object (+-) z
    public void MoveTrackedTransformZ(bool positiveZ)
    {
        direction = 1;
        if (!positiveZ)
            direction = -1;

        int i = CurrentObject;
        float moveZ = sot.TrackedItem[i].transform.position.z + (MoveAmount * direction);
        sot.TrackedItem[i].transform.position = new Vector3(sot.TrackedItem[i].transform.position.x,  sot.TrackedItem[i].transform.position.y, moveZ);
    }

    // rotate object (+-) y
    public void RotateTrackedTransformY(bool positiveY)
    {
        direction = 1;
        if (!positiveY)
            direction = -1;

        int i = CurrentObject;
        float rotateY = sot.TrackedItem[i].transform.eulerAngles.y + (MoveAmount * direction);

        sot.TrackedItem[i].transform.eulerAngles = new Vector3(sot.TrackedItem[i].transform.eulerAngles.x, rotateY, sot.TrackedItem[i].transform.eulerAngles.z);
    }

    // rotate object (+-) y
    //public void MoveTrackedTransformX(int i)
    //{
    //    float moveX = sot.TrackedItem[i].transform.position.x + MoveAmount;
    //    sot.TrackedItem[i].transform.position = new Vector3(moveX, sot.TrackedItem[i].transform.position.y, sot.TrackedItem[i].transform.position.z); ;
    //    //sot.TrackedItem[i].transform.localScale = sot.trackedAssets.assets[i].locations.scale;
    //    //sot.TrackedItem[i].transform.eulerAngles = sot.trackedAssets.assets[i].locations.rotation;
    //}


    // Set current transform to TrackedAsset data object
    //void SetTrackedAssetTransform(int i)
    //{
    //    trackedAssets.assets[i].locations.location = TrackedItem[i].transform.position;
    //    trackedAssets.assets[i].locations.scale = TrackedItem[i].transform.localScale;
    //    trackedAssets.assets[i].locations.rotation = TrackedItem[i].transform.eulerAngles;
    //}

    // Loads transforms from SceneObjectTracking scriptable object data
    public void GetTrackedAssetTransform(int i)
    {
            sot.TrackedItem[i].transform.position = sot.trackedAssets.assets[i].locations.location;
            sot.TrackedItem[i].transform.localScale = sot.trackedAssets.assets[i].locations.scale;
            sot.TrackedItem[i].transform.eulerAngles = sot.trackedAssets.assets[i].locations.rotation;
    }
}
