using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour {
    private NavMeshAgent navAgt;
    public GameObject[] wayPoints;
    public Vector3 dest;
    private NavMeshPath path;
    private Vector3[] pathTurns;
    public GameObject turnTriggerPrefab;
    private bool navigatingToPoint = false;
    private bool navigatingPath = false;
    private float direction;
    // Use this for initialization
    void Start() {
        navAgt = GetComponent<NavMeshAgent>();
        wayPoints = GameObject.FindGameObjectsWithTag("EnemyDestination");
        path = navAgt.path;
        if (!navigatingPath)
            StartCoroutine(Navigate());
    }

    public IEnumerator Navigate()
    {
        if (navigatingPath == false)
        {
            navigatingPath = true;
            print("newpath");
            dest = wayPoints[Random.Range(0, wayPoints.Length-1)].transform.position;

            navAgt.SetDestination(dest);

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

            yield return new WaitForSeconds(1.0f);
            yield return new WaitUntil(() => navAgt.remainingDistance <= 0.1f && navAgt.remainingDistance >= 0.0f);
            navigatingPath = false;
        }
    }

    void OnCollisionEnter(Collision colis)
    {
        
    }
    

	// Update is called once per frame
	void Update() { 
        if(transform.rotation.eulerAngles.y >= 95 && transform.rotation.eulerAngles.y <= 100)
        {
            direction = 1;
            navAgt.areaMask = 48;
        }
        else
        if(transform.rotation.eulerAngles.y >= 170  && transform.rotation.eulerAngles.y <= 190)
        {
            direction = -1;
            navAgt.areaMask = 40;
        }
        else
        if(transform.rotation.eulerAngles.y >= 260 && transform.rotation.eulerAngles.y <= 280)
        {
            direction = -2;
            navAgt.areaMask = 48;
        }
        else
        if(transform.rotation.eulerAngles.y >= 350 && transform.rotation.eulerAngles.y <= 10)
        {
            direction = -2;
            navAgt.areaMask = 40;
        }
        print(navAgt.remainingDistance);
        
        if (!navigatingPath)
        {
            StartCoroutine(Navigate());

            
        }
        
    }
            
}
