using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const float SPEED = 3f;

	private Rigidbody2D _rigidBody;
	private Animator _animator;

	// Use this for initialization
	void Start ()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_animator.SetFloat("Angle", 0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void FixedUpdate()
	{
		// velocity
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (input.magnitude > 1f)
		{
			input = input.normalized;
		}
		_rigidBody.velocity = input * SPEED;
		
		// animation
		_animator.speed = input.magnitude;
		if (_animator.speed > 0f)
		{
			float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
			_animator.SetFloat("Angle", angle);
		}
	}
}
