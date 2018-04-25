using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarInput : MonoBehaviour {
	private CarController m_Car;
	public float h = 0.0f;
	public float v = 1.0f;
	public float handbrake = 0.0f;
	// Use this for initialization
	void Start () {
		m_Car = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {

		m_Car.Move(h, v, v, 0);
	}

	public void BrakePressed()
	{
		v = -1.0f;
	}
	
	public void BrakeReleased()
	{
		v = 1.0f;
	}

	public void LeftPressed()
	{
		h = -1.0f;
	}

	public void LeftReleased()
	{
		h = 0.0f;
	}

	public void RightPressed()
	{
		h = 1.0f;
	}

	public void RightReleased()
	{
		h = 0.0f;
	}

}
