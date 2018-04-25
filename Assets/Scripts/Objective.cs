using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
	public string locationName = "";
	public string objectiveDescription = "";
	public bool activeObjective = false;
	public GameObject marker;
	public ObjectiveManager objMan;
	private Collider thisCol;
	// Use this for initialization
	void Start () {
		thisCol = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		if(activeObjective == true)
		{
			thisCol.enabled = true;
			marker.SetActive(true);
			marker.transform.Rotate(Vector3.up);
		}
		else
		{
			thisCol.enabled = false;
			marker.SetActive(false);
		}
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			objMan.ObjectiveCompleted();
			objCompleted();
		}
	}

	void objCompleted()
	{
		activeObjective = false;
	}
}
