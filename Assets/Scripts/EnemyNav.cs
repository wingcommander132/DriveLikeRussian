using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour {
    private NavMeshAgent navAgt;
    public GameObject[] wayPoints;
    public Vector3 dest;
    private Vector3[] pathTurns;
    public GameObject turnTriggerPrefab;
    private bool navigatingToPoint = false;
    private bool navigatingPath = false;
    private float direction;
    public bool stopped = false;
    public bool inIntersection = false;
    public GameObject stoppedTrigger;
    public brake_light brakeL;
    // Use this for initialization
    void Start() {
        brakeL = GetComponent<brake_light>();
        navAgt = GetComponent<NavMeshAgent>();
        wayPoints = GameObject.FindGameObjectsWithTag("EnemyDestination");
        if (!navigatingPath)
            StartCoroutine(Navigate());
        
        print(navAgt.areaMask);
    }

    public IEnumerator Navigate()
    {
        if (navigatingPath == false)
        {
            navigatingPath = true;
            //print("newpath");
            dest = wayPoints[Random.Range(0, wayPoints.Length-1)].transform.position;

            navAgt.SetDestination(dest);
            /* 
            pathTurns = navAgt.path.corners;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("NavPathTurn"))
            {
                Destroy(obj);
            }

            foreach (Vector3 turn in pathTurns)
            {
                Transform turnPnt = gameObject.transform;
                turnPnt.rotation = new Quaternion(0, 0, 0, 0);
                turnPnt.localScale = new Vector3(1, 1, 1);
                turnPnt.position = turn;

                Instantiate(turnTriggerPrefab, turnPnt);
            }

            */


            yield return new WaitForSeconds(1.0f);
            yield return new WaitUntil(() => navAgt.remainingDistance <= 0.1f && navAgt.remainingDistance >= 0.0f);
            navigatingPath = false;
        }
    }


    public void stopMovement(bool stop)
    {
        if(stop == true)
        {
            stopped = true;
            navAgt.isStopped = true;
            stoppedTrigger.SetActive(true);
            foreach (Light l in brakeL.lights)
            {
                l.enabled = true;
            }
        }
        else
        {
            stopped = false;
            navAgt.isStopped = false;
            StartCoroutine(triggerOff(stoppedTrigger));
            foreach (Light l in brakeL.lights)
            {
                l.enabled = false;
            }
        }
    }

    IEnumerator triggerOff(GameObject trig)
    {
        yield return new WaitForSeconds(1.5f);
        stoppedTrigger.SetActive(false);
    }
    
    void OnCollisionEnter(Collision colis)
    {
        
    }
    

	// Update is called once per frame
	void Update() { 
        int lookdir = Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90.0f);

        if(lookdir == 1)
        {
            navAgt.areaMask = 48;
        }
        else
        if(lookdir == 2)
        {
            navAgt.areaMask = 48;
        }
        else
        if(lookdir == 3)
        {
            navAgt.areaMask = 40;
        }
        else
        if(lookdir == 0 || lookdir == 4)
        {
            navAgt.areaMask = 40;
        }
        
    }

    void OnTriggerEnter(Collider trigger)
    {
        if(trigger.gameObject.tag == "Intersection")
        {
            inIntersection = true;
        }

        if(trigger.gameObject.tag == "StoppedNPC")
        {
            stopMovement(true);
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if(trigger.gameObject.tag == "StoppedNPC")
        {
            stopMovement(false);
        }

        if(trigger.gameObject.tag == "Intersection")
        {
            inIntersection = false;
        }

    }

    void FixedUpdate()
    {
        
        if(stopped == true)
        {
            navAgt.isStopped = true;
        }
        else
            navAgt.isStopped = false;
        

        int lookdir = Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90.0f);

        if(lookdir == 1)
        {
            navAgt.areaMask = 48;
        }
        else
        if(lookdir == 2)
        {
            navAgt.areaMask = 48;
        }
        else
        if(lookdir == 3)
        {
            navAgt.areaMask = 40;
        }
        else
        if(lookdir == 0 || lookdir == 4)
        {
            navAgt.areaMask = 40;
        }
        //print(navAgt.remainingDistance);
        
        if (!navigatingPath && !stopped)
        {
            StartCoroutine(Navigate());
        }

    }
            
}
