using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionTrigger : MonoBehaviour {
	public IntersectionHandler intersectionHandle;
	// Use this for initialization
	void Start () {
		if(intersectionHandle == null)
			intersectionHandle = GetComponentInParent<IntersectionHandler>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coly)
	{
		if(coly.gameObject.tag == "NPC_Car")
		{
			if(intersectionHandle.que.Contains(coly.gameObject.GetComponent<EnemyNav>()) == false)
			{
				intersectionHandle.carsWaiting++;
				intersectionHandle.que.Add(coly.gameObject.GetComponent<EnemyNav>());
				
				if(intersectionHandle.carsWaiting > 1)
				{
					coly.gameObject.GetComponent<EnemyNav>().stopMovement(true);
				}
				else
				{
					coly.gameObject.GetComponent<EnemyNav>().stopMovement(false);
				}
			}
		}
	}
}
