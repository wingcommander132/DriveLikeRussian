using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
	public Objective[] objList;
	public Objective currentObj;
	public bool hasObjective = false;
	private GameObject guider;
	// Use this for initialization
	void Start () {
		int len = GameObject.FindGameObjectsWithTag("PlayerObjective").Length;
		if(len > 1)
		{
			int cnt = 0;
			objList = new Objective[len];
			foreach(GameObject ob in GameObject.FindGameObjectsWithTag("PlayerObjective"))
			{
				objList[cnt] = ob.GetComponent<Objective>();
				cnt++;
			}
		}
		guider = GameObject.FindGameObjectWithTag("PlayerObjectiveGuide");

	}
	
	void ObjectiveAssign()
	{
		int objKey = Random.Range(0,(objList.Length-1));
		objList[objKey].activeObjective = true;
		hasObjective = true;
		currentObj = objList[objKey];
	}

	public void ObjectiveCompleted()
	{
		hasObjective = false;
		ObjectiveAssign();
	}

	// Update is called once per frame
	void Update () {
		if(!hasObjective || currentObj==null)
			ObjectiveAssign();
	}

	void FixedUpdate()
	{
		if(currentObj != null)
			guider.transform.LookAt(new Vector3(currentObj.transform.position.x,0,currentObj.transform.position.z));
	}
}
