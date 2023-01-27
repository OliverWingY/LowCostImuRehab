using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 0;
	[SerializeField] private float walkSpeed = 0;
	[SerializeField] private float runSpeed = 0;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	[SerializeField] private float gravity;
	[SerializeField] private float groundDistance;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private bool isCharGrounded = false;
	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		//HandleIsGrounded();
		//HandleGravity();

		GetReferences();
		InitVariables();
	}

	private void Update()
	{
		HandleRunning();
		HandleMovement();
	}

	private void HandleMovement()
	{
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveZ = Input.GetAxisRaw("Vertical");

		moveDirection = new Vector3(moveX, 0, moveZ);
		moveDirection = moveDirection.normalized;

		controller.Move(moveDirection * moveSpeed * Time.deltaTime);
	}

	private void HandleRunning()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			moveSpeed = runSpeed;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			moveSpeed = walkSpeed;
		}
	}

	/*private void HandleIsGrounded()
	{
		isCharGrounded = Physics.CheckSphere(transform.position, groundDistance);
	}

	private void HandleGravity()
	{
		if (isCharGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}*/

	private void GetReferences()
	{
		controller = GetComponent<CharacterController>();
	}

	private void InitVariables()
	{
		moveSpeed = walkSpeed;
	}
}
