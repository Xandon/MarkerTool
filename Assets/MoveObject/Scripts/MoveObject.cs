using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public string Name;
    public GameObject Object;
    public GameObject target;

    public Vector3 location;
    public Vector3 rotation;
    public Vector3 scale;

	// Use this for initialization
	void Start () {
        location = target.transform.position;
        rotation = target.transform.eulerAngles;
        scale = target.transform.localScale;
	}

	void Update()
	{
        Object.transform.position = location;
        Object.transform.eulerAngles = rotation;
        Object.transform.localScale = scale;

        location = target.transform.position;
        rotation = target.transform.eulerAngles;
        scale = target.transform.localScale;

	}
}
