using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public CharacterController controller;

	//camera
	public Transform camera;
	public Transform viewTransform;

	//player stats
	public float speed = 6f;
	public float jumpForce = 5f;
	public float playerZRotation;
	private Vector3 velocity;

	//gravity
	public bool gravityFlipped;
	public float gravity;

	//grounded and jump
	public float groundDistance = 0.4f;
	public Transform groundCheck;
	public LayerMask groundMask;
	bool isGrounded;

	public Rigidbody rb;

	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

	void Start()
	{
		//Physics.gravity = new Vector3(0, -9.8f, 0);
	}

	void Update()
	{
		//might need to remove raw
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

		//rb.velocity += Physics.gravity * Time.fixedDeltaTime;

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		Debug.Log(isGrounded);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		if (direction.magnitude > 0.1)
		{
			//returns angle for player to rotate with movement
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
			//smooths player rotation rotation
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0, angle, playerZRotation);

			Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			controller.Move(moveDirection.normalized * speed * Time.deltaTime);
		}

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			OnFlipGravity();
		}

		if (Game.Instance.gameMode == Game.GameMode.ALT_GRAVITY)
		{
			velocity.z += gravity * Time.deltaTime;
		}
		else
		{
			velocity.y += gravity * Time.deltaTime;
		}

		controller.Move(velocity * Time.deltaTime);
	}

	public void OnFlipGravity()
	{
		Debug.Log("gravity flipped");
		
		if (gravityFlipped == false)
		{
			velocity.y = 0;
			gravity *= -1;
			playerZRotation += 180;
			
			viewTransform.transform.eulerAngles += new Vector3(0, 0, 180);
			viewTransform.transform.LookAt(transform.position);
		}
		else
		{
			velocity.y = 0;
			gravity *= -1;
			playerZRotation -= 180;

			viewTransform.transform.eulerAngles -= new Vector3(0, 0, 180);
			viewTransform.transform.LookAt(transform.position);
			
		}
	}
}
