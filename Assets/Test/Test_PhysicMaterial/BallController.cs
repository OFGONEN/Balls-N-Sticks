using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	Rigidbody ball_rb;
	Transform ball_t;
	public float jump_power = 5f;
	public float movement_speed = 0.5f;

	// Start is called before the first frame update
	void Start()
	{
		ball_rb = GetComponent<Rigidbody>();
		ball_t = GetComponent<Transform>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		/* Why: I use ForceMode.Impulse
		* because I think stick in that game we produce has impulsive interaction with balls.*/
		if (ball_t.position.y < 0.8f)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				ball_rb.AddForce(new Vector3(0, jump_power, 0), ForceMode.Impulse);

			if (Input.GetKey(KeyCode.A))
				ball_rb.AddForce(new Vector3(-movement_speed, 0, 0), ForceMode.Impulse);
			if (Input.GetKey(KeyCode.D))
				ball_rb.AddForce(new Vector3(movement_speed, 0, 0), ForceMode.Impulse);
			if (Input.GetKey(KeyCode.W))
				ball_rb.AddForce(new Vector3(0, 0, movement_speed), ForceMode.Impulse);
			if (Input.GetKey(KeyCode.S))
				ball_rb.AddForce(new Vector3(0, 0, -movement_speed), ForceMode.Impulse);
		}
	}
}
