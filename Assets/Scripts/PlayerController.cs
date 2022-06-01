using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(1, 10)] float playerSpeed = 2;
    //[SerializeField][Range(1, 10)] float maxForce = 1;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float rotateSpeed = 20;
    [SerializeField] ForceMode forceMode;
    [SerializeField] Transform viewTransform;

    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    float airTime = 0; 

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * playerSpeed * Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        /*        // xz movement
                Vector3 direction = Vector3.zero;
                direction.x = Input.GetAxis("Horizontal");
                direction.z = Input.GetAxis("Vertical");
                direction = Vector3.ClampMagnitude(direction, 1);

                // convert direction from world space to view space
                Quaternion viewSpace = Quaternion.AngleAxis(viewTransform.rotation.eulerAngles.y, Vector3.up);
                direction = viewSpace * direction;

                // y movement
                animator.SetBool("isGrounded", controller.isGrounded);
                if (controller.isGrounded)
                {
                    airTime = 0;
                    if (velocity.y < 0) velocity.y = 0;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        velocity.y = jumpForce;
                    }
                }
                else
                {
                    airTime += Time.deltaTime;
                }
                velocity += Physics.gravity * Time.deltaTime;

                // move character (xyz)
                controller.Move(((direction * playerSpeed) + velocity) * Time.deltaTime);

                // face direction
                if (direction.magnitude > 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
                }

                animator.SetFloat("speed", (direction * playerSpeed).magnitude);
                animator.SetFloat("velocityY", velocity.y);
                animator.SetFloat("airTime", airTime);*/
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }
}
