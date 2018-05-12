using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[ExecuteInEditMode]
public class SaveLoad : MonoBehaviour
{

    public TrackedAssets trackedAssets;
    public TrackedHistory trackedAssetHistory;
    private string FilePath;
    public string FileName;

    // Use this for initialization
    void Start()
    {
        FileName = trackedAssetHistory.FileName + trackedAssetHistory.Count + ".json" ;
        trackedAssetHistory.TrackHistorySaves[trackedAssetHistory.Count].name = FileName;
        trackedAssetHistory.Count += 1;
        if(FileName == null){
            FileName = "save.json"; 
        }

        FilePath = Path.Combine(Application.dataPath, FileName);
        //Load();
        Save();
        //Compare();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Save()
    {
        File.WriteAllText(FilePath, JsonUtility.ToJson(trackedAssets, true));

    }

    void Load()
    {
        string jsonString = File.ReadAllText(FilePath);
        JsonUtility.FromJsonOverwrite(jsonString, trackedAssets);
    }

    bool Compare() {
        
        if(File.Exists(FilePath)){
            string jsonStringCurrent = File.ReadAllText(FilePath);
            string jsonStringStored = JsonUtility.ToJson(trackedAssets);
            if (string.Equals(jsonStringCurrent, jsonStringStored))
            {
                Debug.Log("No change detected");
                return (false);
            }
            else
            {
                Debug.Log("Items moved, saving...");
                File.WriteAllText(FilePath, JsonUtility.ToJson(trackedAssets, true));
                return (true);
            }
        }
        else
        {
            Debug.Log("First save, saving...");
            Save();
            return (false);
        }
    }

}

[CustomEditor(typeof(SaveLoad))]
public class SaveLoadInEditGui : Editor
{
    //public TrackedAssets trackedAssets;
    public TrackedHistory trackedAssetHistory;
    public int HistoryIndex = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveLoad myScript = (SaveLoad)target;
        if (GUILayout.Button("Set Transform"))
        {
            string indexFileName = trackedAssetHistory.FileName + HistoryIndex + ".json";
            //this.Load();
            string jsonString = File.ReadAllText(indexFileName);
            trackedAssetHistory.Index = HistoryIndex;
            JsonUtility.FromJsonOverwrite(jsonString, trackedAssetHistory);
        }
    }
}