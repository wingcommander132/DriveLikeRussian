using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject cam;
    Quaternion forcerot;
	// Use this for initialization
	void Start () {
        cam = transform.GetComponentInChildren<Camera>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
        
	}

    public void camera_turn(bool turning)
    {
        if(turning)
        {
            forcerot = cam.transform.rotation;
            //cam.transform.
        }
    }

    //IEnumerator Waitforfix()
    //{
        
    //}
}
