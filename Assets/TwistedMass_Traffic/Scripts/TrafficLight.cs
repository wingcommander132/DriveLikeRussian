using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Twisted Mass 2018 - Chris Coetzee

namespace tm_intersection
{

    public class TrafficLight : MonoBehaviour {

        //active state of the traffic light
        public bool active = false;

        //Does it function or should it blink red?
        public bool working = false;
        public int color = 0;

        //Traffic lighter manager and intersection main script, auto set on activation
        public TrafficLightManager tl_Manager;
        public Intersection intersection;

        //override times for light timers
        public float greenTime = 0.0f;
        public float orangeTime = 0.0f;
        public float redTime = 0.0f;

        //Point lights (or whatever you want, as long as it is a light)
        public Light green;
        public Light orange;
        public Light red;

        //is the Light_manager coroutine running
        bool lightmanrunning = false;

        //Array of wait times for each light color indexed as 1-3 => Green - Red
        float[] waitTimers = new float[4];

        //The status/color for the the light to follow the given one ("Status = 1" thus "nextStatusFor[1] = 2" etc.)
        int[] nextStatusFor = new int[4];

        public GameObject modelContainer;

        //Compas position on the intersection... Gets auto set to the gameobject tag  
        public string iPosition = "";

        //current group/pair light is assigned to
        public int currgroup = 0;

        //counting int to keep track of how many changes the light has done
        private int runs = 0;

        // Use this for initialization
    	void Start () {
            nextStatusFor[0] = 1;
            nextStatusFor[1] = 2;
            nextStatusFor[2] = 3;
            nextStatusFor[3] = 1;
    	}

        /* 
        Possible start statuses:
        0 => Random
        1 => Green
        2 => Orange
        3 => Red
        */
    	
        public void light_starter(TrafficLightManager tm, Intersection i, bool works, int startstatus = 0)
        {
            tl_Manager = tm;
            intersection = i;

            iPosition = gameObject.tag;

            if(iPosition == "IntersectionN" || iPosition == "IntersectionS")
            {
                currgroup = 1;
            }
            else
            if(iPosition == "IntersectionE" || iPosition == "IntersectionW")
            {
                currgroup = 2;
            }
            
            if (active && !lightmanrunning)
            {
                if (greenTime == 0.0f)
                {
                    greenTime = intersection.defaultGreenTime;
                }

                if (orangeTime == 0.0f)
                {
                    orangeTime = intersection.defaultOrangeTime;
                }

                if (redTime == 0.0f)
                {
                    redTime = intersection.defaultRedTime;
                }

                waitTimers[1] = greenTime;
                waitTimers[2] = orangeTime;
                waitTimers[3] = redTime;

                StartCoroutine(LightManage(startstatus));
            }

        }

        public void light_killer(bool hide = false)
        {
            active = false;
            StopCoroutine(LightManage(color));
            DisableLights();

            if(hide){
                modelContainer.SetActive(false);
            }
            else{
                working = false;
                StartCoroutine(LightManage(0));
            }

        }

    	// Update is called once per frame
    	void Update () {
    		
    	}

		void FixedUpdate()
		{
            if (!working && active && !lightmanrunning)
                StartCoroutine(LightManage(0));

            if(!active)
            {
                StopCoroutine(LightManage(0));
                DisableLights();
            }

            if(currgroup == tl_Manager.disabledGroup)
            {
                working = false;
            }
		}

        IEnumerator LightManage(int status)
        {
            runs++;

            lightmanrunning = true;

            color = status;

            if (working)
            {
                DisableLights();

                if (status == 0)
                {
                    status = Random.Range(1, 3);
                    if (status != 0)
                        status = 1;
                }


                if (currgroup == 1 && runs < 2)
                {
                    if (status == 1)
                        status = 3;
                    else
                    if (status == 3)
                        status = 1; 

                    if (status == 1)
                    {
                        green.enabled = true;
                    }
                    else
                    if (status == 2)
                    {
                        orange.enabled = true;
                    }
                    else
                    if (status == 3)
                    {
                        red.enabled = true;
                    }
                }
                else
                {
                    if (status == 1)
                    {
                        green.enabled = true;

                    }
                    else
                    if (status == 2)
                    {
                        orange.enabled = true;

                    }
                    else
                    if (status == 3)
                    {
                        red.enabled = true;
                    }
                }

                yield return new WaitForSeconds(waitTimers[status]);
                StartCoroutine(LightManage(nextStatusFor[status]));
            }
            else
            {
                lightmanrunning = true;
                color = status;
                DisableLights();
                yield return new WaitForSecondsRealtime(1.0f);
                red.enabled = true;
                yield return new WaitForSecondsRealtime(1.0f);
                red.enabled = false;
                StartCoroutine(LightManage(0));
            }
        
        }

        void DisableLights()
        {
            green.enabled = false;
            orange.enabled = false;
            red.enabled = false;
        }

	}
}