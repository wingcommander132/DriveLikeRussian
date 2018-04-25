using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionHandler : MonoBehaviour {
	private GameObject[] junctions;
	public int numJunctions = 0;
	public int carsWaiting = 0;
	public List<EnemyNav> que = new List<EnemyNav>();
	private int curlistpos = 0;
	private bool managingtrafic = false;
	// Use this for initialization
	void Start () {
		junctions = new GameObject[transform.childCount];

		int cnt = 0;
		while(cnt != transform.childCount)
		{
			junctions[cnt] = transform.GetChild(cnt).gameObject;
			cnt++;
		}

		numJunctions = junctions.Length;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		carsWaiting = que.Count;

		if(carsWaiting > 0 && !managingtrafic)
		{
			StartCoroutine(manageTraffic());
		}
		else
		if(carsWaiting <= 0)
		{
			que.Clear();
			managingtrafic = false;
		}
	}

	IEnumerator manageTraffic()
	{
		EnemyNav currNPC = default(EnemyNav);
		managingtrafic = true;
		if(que.Count > 0)
		{	
				currNPC = que[0];
				currNPC.stopMovement(false);
				yield return new WaitUntil(()=> currNPC.inIntersection == false);
				yield return new WaitForSeconds(0.3f);
				que.Remove(currNPC);
		}

		yield return managingtrafic = false;
	}

	void OnTriggerEnter(Collider coly)
	{
		if(coly.tag == "Player")
		{
			carsWaiting++;
		}
	}

	void OnTriggerExit(Collider coly)
	{
		if(coly.tag == "Player")
		{
			carsWaiting--;
		}
	}

}
