using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tm_intersection {

public class TrafficLightManager : MonoBehaviour {
        public bool active = false;
        public bool working = false;
        public TrafficLight[] tLights;
        public int disabledGroup = 0;
        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (active)
            {

            }
        }

        public void TLManager_Start(bool wrx = true, int numlights = 4, bool lightswork = true, bool group1 = true, bool group2 = true)
        {
            active = true;
            working = lightswork;

            if (!working)
            {
                if (!group1)
                    disabledGroup = 1;

                if (!group2)
                    disabledGroup = 2;

                if (!group1 && !group2)
                    disabledGroup = 0;
            }
            

            tLights = new TrafficLight[numlights];
            tLights = GetComponentsInChildren<TrafficLight>();
            foreach (TrafficLight tl in tLights)
            {
                tl.light_starter(this, GetComponent<Intersection>(), true, 0);
            }
        }
    }
}
