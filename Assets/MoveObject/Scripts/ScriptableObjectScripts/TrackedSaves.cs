using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrackedSaves : ScriptableObject
{
    public string FileName;
    public int Count;
    public int Index = 0;
    public TrackHistorySave[] TrackHistorySaves;
}

[Serializable]
public class TrackHistorySave
{
    public string name;
}

