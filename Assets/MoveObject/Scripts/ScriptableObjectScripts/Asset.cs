using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Assets : ScriptableObject
{
    //public string FileName;
    //public int Count;
    //public int Index = 0;
    //public Save[] Saves;
    public Asset[] assets;
}

[Serializable]
public class Asset
{
    public string name;
    public Location locations;
}

[Serializable]
public struct Location
{
    public Vector3 location;
    public Vector3 rotation;
    public Vector3 scale;
}

//[Serializable]
//public class Save
//{
//    public string name;
//}

