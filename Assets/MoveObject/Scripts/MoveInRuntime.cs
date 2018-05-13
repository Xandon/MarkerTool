using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEditor;

public class MoveInRuntime : MonoBehaviour
{
    public SceneObjectTracking sot;
    //public int Index;
    public int HistoryIndex = 0;
    public Transform ClearLocation;

    private string FilePath;
    private string FileName;

    // Use this for initialization
    void Start()
    {
        sot = this.GetComponent<SceneObjectTracking>();
    }

    public void ClaerHover()
    {
        //Debug.Log("Clear button hover");
    }

    public void Clear()
    {
        for (int i = 0; i < sot.trackedAssets.assets.Length; i++)
        {
            sot.TrackedItem[i].transform.position = ClearLocation.position;
            sot.TrackedItem[i].transform.localScale = ClearLocation.localScale;
            sot.TrackedItem[i].transform.eulerAngles = ClearLocation.eulerAngles;
            sot.TrackedItem[i].SetActive(false);
        }
        //Debug.Log("GetTrackedAsset button pressed");
        //Debug.Log("Clear");
    }

    public void GetTrackedAssetHover()
    {
        //Debug.Log("GetTrackedAsset button hover");
    }

    public void GetTrackedAssetTransform()
    {
        for (int i = 0; i < sot.trackedAssets.assets.Length; i++)
        {
            sot.TrackedItem[i].transform.position = sot.trackedAssets.assets[i].locations.location;
            sot.TrackedItem[i].transform.localScale = sot.trackedAssets.assets[i].locations.scale;
            sot.TrackedItem[i].transform.eulerAngles = sot.trackedAssets.assets[i].locations.rotation;
            sot.TrackedItem[i].SetActive(true);
        }
        //Debug.Log("GetTrackedAsset button pressed");
        //Debug.Log("GetTrackedAssetTransform");
    }



    public void SaveHover()
    {
        //Debug.Log("Save button hover");
    }

    public void Save()
    {
        GetTrackedAssetTransform();

        string FileNameTime = System.DateTime.Now.ToString("yyyyMMdd") + "." + System.DateTime.Now.ToString("HH.mm.ss");
        //FileName = sot.trackedAssets.FileName + sot.trackedAssets.Count + ".json";
        //FileName = sot.trackedAssetHistory.FileName + FileNameTime + ".json";
        FileName = sot.trackedAssetHistory.FileName + ".json";
        sot.trackedAssetHistory.TrackHistorySaves[sot.trackedAssetHistory.Count].name = FileName;
        ///sot.trackedAssetHistory.Count += 1;

        FilePath = Path.Combine(Application.dataPath, FileName);

        File.WriteAllText(FilePath, JsonUtility.ToJson(sot.trackedAssets, true));
        Debug.Log("Saved: " + FileNameTime + " " + FilePath);
        //Debug.Log("Save button pressed");

    }



    public void LoadHover()
    {
        //Debug.Log("Load button hover");
    }

    public void Load()
    {
        //string jsonString = File.ReadAllText(FilePath);
        //JsonUtility.FromJsonOverwrite(jsonString, sot.trackedAssets);
        //Debug.Log("Load button pressed");
        //Debug.Log("Load disabled");
    }

    public void Quit()
    {
        Application.Quit();
        //Debug.Log("Quit");
    }


    public void SetObjectTransformHover()
    {
        //Debug.Log("SetObJectTransform button hover");
    }

    public void SetObjectsTransform()
    {

            //string indexFileName = myScript.sot.trackedAssets.FileName + HistoryIndex + ".json";
            string indexFileName = sot.trackedAssetHistory.TrackHistorySaves[sot.trackedAssetHistory.Count].name;
            string Filepath = Path.Combine(Application.dataPath, indexFileName);
            if (File.Exists(Filepath))
            {
                Debug.Log(indexFileName);

                string jsonString = File.ReadAllText(Filepath);
                //myScript.sot.trackedAssets.Index = HistoryIndex;
                JsonUtility.FromJsonOverwrite(jsonString, sot.trackedAssets);

                GetTrackedAssetTransform();
            }
            else
            {
                Debug.Log("NO FILE EXISTS: " + indexFileName);
            }
        //Debug.Log("SetObjectTransform button pressed");

    }
}