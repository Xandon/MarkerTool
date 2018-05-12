using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class MarkerPlacement : MonoBehaviour {

    public string triggerButton1 = "14";
    public string triggerButton2 = "15";
    public string changeColor1 = "8";
    public string changeColor2 = "9";
    public string zOffsetForward1 = "18";
    public string zOffsetForward2 = "19";
    public string zOffsetBack1 = "16";
    public string zOffsetBack2 = "17";


    [SerializeField] Text positionText;
    [SerializeField] Material[] materials;
    [SerializeField] GameObject[] markerTemplates;
    [SerializeField] GameObject targetMarker;
    private Transform targetTransform;
    private Quaternion targetRotation;
    private Transform parentTransform;
    private Quaternion parentRotation;
    private Material currentMaterial;
    private string currentMarker;
    private int matCount = 0;
    private ArrayList markerPoints = new ArrayList();
    private float zOffset = 0;
    private GameObject colorSphereContainer;

    SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start () {
        matCount = 0;
        currentMaterial = materials[matCount];
        positionText.color = currentMaterial.color;
        currentMarker = markerTemplates[matCount].name;
        markerPoints.Clear();
        GameObject go = Instantiate(targetMarker) as GameObject;
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.eulerAngles = Vector3.zero;

        targetTransform = go.transform;
        targetRotation = go.transform.rotation;
        zOffset = 0;
        colorSphereContainer = GameObject.Find("Color_Sphere_Container");
        if (colorSphereContainer == null)
            colorSphereContainer = new GameObject();
        colorSphereContainer.transform.position = Vector3.zero;
        colorSphereContainer.transform.eulerAngles = Vector3.zero;

        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (Input.GetButtonDown(triggerButton1) || Input.GetButtonDown(triggerButton2) || device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = Instantiate(markerTemplates[matCount]) as GameObject;
            go.transform.parent = colorSphereContainer.transform;
            go.transform.parent.rotation = colorSphereContainer.transform.rotation;
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            Vector3 v = go.transform.position;
            Vector3 r = go.transform.eulerAngles;
            string pInfo = v.x + "," + v.y + "," + v.z + "," + r.x + "," + r.y + "," + r.z + "," + currentMarker;
            markerPoints.Add(pInfo);
            SaveToFile();
        }

        if (Input.GetButtonDown(changeColor1) || Input.GetButtonDown(changeColor2))
        {
            matCount++;
            if (matCount > materials.Length - 1)
                matCount = 0;
            currentMaterial = materials[matCount];
            positionText.color = currentMaterial.color;
            currentMarker = markerTemplates[matCount].name;
        }

        if (Input.GetButtonDown(zOffsetForward1) || Input.GetButtonDown(zOffsetForward2))
        {
            Debug.Log("Z offset forward");
            zOffset += 0.001f;
            Vector3 tPos = targetTransform.localPosition;
            tPos.z += 0.001f;
            targetTransform.localPosition = tPos;
        }
            
        if (Input.GetButtonDown(zOffsetBack1) || Input.GetButtonDown(zOffsetBack2))
        {
            Debug.Log("Z offset back");
            zOffset -= 0.001f;
            Vector3 tPos = targetTransform.localPosition;
            tPos.z -= 0.001f;
            targetTransform.localPosition = tPos;
        }
            



        //keep info display updated with hand current position
        if (positionText != null)
            positionText.text = transform.position.ToString();
    }

    void SaveToFile ()
    {
        string s = "";
        foreach(string sInfo in markerPoints)
        {
            s = s + sInfo + "#";
        }
        if (File.Exists("SavedMarkers.txt"))
            File.Delete("SavedMarkers.txt");
        File.WriteAllText("SavedMarkers.txt", s);
    }

    [MenuItem("Hyperspace/Build Markers",false)]
    static void Init()
    {
        if (File.Exists("SavedMarkers.txt"))
        {
            GameObject colorSphereContainer = GameObject.Find("Color_Sphere_Container");
            if (colorSphereContainer != null)
                Destroy(colorSphereContainer);
            colorSphereContainer = new GameObject();
            colorSphereContainer.transform.position = Vector3.zero;
            colorSphereContainer.transform.eulerAngles = Vector3.zero;
            string s = File.ReadAllText("SavedMarkers.txt");
            string[] points = s.Split('#');
            foreach (string point in points)
            {
                if (string.IsNullOrEmpty(point))
                    break;
                string[] bits = point.Split(',');
                float x = float.Parse(bits[0]);
                float y = float.Parse(bits[1]);
                float z = float.Parse(bits[2]);
                float rx = float.Parse(bits[3]);
                float ry = float.Parse(bits[4]);
                float rz = float.Parse(bits[5]);
                Vector3 pos = new Vector3(x, y, z);
                Vector3 rot = new Vector3(rx, ry, rz);
                GameObject go = Instantiate(Resources.Load("Prefabs/"+bits[6])) as GameObject;
                go.transform.parent = colorSphereContainer.transform;
                go.transform.position = pos;
                go.transform.eulerAngles = rot;
            }
        }
            
    }
}
