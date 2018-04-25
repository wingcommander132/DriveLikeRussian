using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tm_intersection {

    //System supports statuses for tracking current traffic flow

    public class Intersection : MonoBehaviour {
        public int status = 0;
        public bool hasTrafficLight = true;
        public bool trafficLightsWorking = true;
        public bool group1Working = true;
        public bool group2Working = true;
        public int numPoints = 4;
        public float defaultGreenTime = 15.0f;
        public float defaultOrangeTime = 3.0f;
        public float defaultRedTime = 15.0f;
        public int groupallowed = 0;

        /*
        Groups/Tlightpairs
        0 = None/All
        1 = NorthSouth
        2 = EastWest
        */


    	// Use this for initialization
    	void Start () {
            if (hasTrafficLight)
            {

                GetComponent<TrafficLightManager>().TLManager_Start(true, numPoints, trafficLightsWorking, group1Working, group2Working);
                
            
            }
            
        }
    	
    	// Update is called once per frame
    	void Update () {
    		
    	}

    }
}