using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

    
[ExecuteInEditMode]
public class EditorMoveInEdit : MonoBehaviour
{
    public SceneObjectTracking sot;
    public int Index;

    private string FilePath;
    private string FileName;

    // Use this for initialization
    void Start()
    {
        sot = this.GetComponent<SceneObjectTracking>();

        if (sot.trackedAssetHistory.Count == 0)
        {
            Save();
        }else if (!EditorApplication.isPlaying)
        {
            Save();
        }
    }

    public void GetTrackedAssetTransform()
    {
            for (int i = 0; i < sot.trackedAssets.assets.Length; i++)
            {
                sot.TrackedItem[i].transform.position = sot.trackedAssets.assets[i].locations.location;
                sot.TrackedItem[i].transform.localScale = sot.trackedAssets.assets[i].locations.scale;
                sot.TrackedItem[i].transform.eulerAngles = sot.trackedAssets.assets[i].locations.rotation;
            }
            Debug.Log("Editor button pressed");
    }


    public void Save()
    {
        string FileNameTime = System.DateTime.Now.ToString("yyyyMMdd") + "." + System.DateTime.Now.ToString("HH.mm.ss");
        //FileName = sot.trackedAssets.FileName + sot.trackedAssets.Count + ".json";
        FileName = sot.trackedAssetHistory.FileName + FileNameTime + ".json";
        sot.trackedAssetHistory.TrackHistorySaves[sot.trackedAssetHistory.Count].name = FileName;
        sot.trackedAssetHistory.Count += 1;

        FilePath = Path.Combine(Application.dataPath, FileName);
        
        File.WriteAllText(FilePath, JsonUtility.ToJson(sot.trackedAssets, true));
        Debug.Log("Saved: "+ FileNameTime +" "+ FilePath);

    }


    void Load()
    {
        string jsonString = File.ReadAllText(FilePath);
        JsonUtility.FromJsonOverwrite(jsonString, sot.trackedAssets);
    }

}

[CustomEditor(typeof(EditorMoveInEdit))]
public class EditorMoveInEditGui : Editor
{
    public int HistoryIndex = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorMoveInEdit myScript = (EditorMoveInEdit)target;
        if (GUILayout.Button("Set Objects Transform: "))
        {
            HistoryIndex = myScript.Index;

            //string indexFileName = myScript.sot.trackedAssets.FileName + HistoryIndex + ".json";
            string indexFileName = myScript.sot.trackedAssetHistory.TrackHistorySaves[HistoryIndex].name;
            string Filepath = Path.Combine(Application.dataPath, indexFileName);
            if (File.Exists(Filepath)){
                Debug.Log(indexFileName);

                string jsonString = File.ReadAllText(Filepath);
                //myScript.sot.trackedAssets.Index = HistoryIndex;
                JsonUtility.FromJsonOverwrite(jsonString, myScript.sot.trackedAssets);

                myScript.GetTrackedAssetTransform();
            }else{
                Debug.Log("NO FILE EXISTS: "+indexFileName);
            }
        }
    }
}