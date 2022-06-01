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

    Rigidbody rb;
    Vector3 force = Vector3.zero;

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
            Debug.Log("Jumping"); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        /*        Vector3 direction = Vector3.zero;

                direction.x = Input.GetAxis("Horizontal");
                direction.z = Input.GetAxis("Vertical");

                // view space
                Quaternion viewSpace = Quaternion.AngleAxis(viewTransform.rotation.eulerAngles.y, Vector3.up);
                direction = viewSpace * direction;

                // world space
                force = direction * maxForce;

                //force = viewSpace * force; 

                if (Input.GetButtonDown("Jump"))
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }*/

        /*        groundedPlayer = controller.isGrounded;
                if (groundedPlayer && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }

                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * playerSpeed);
                animator.SetFloat("Speed", move.magnitude);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }

                // Changes the height position of the player..
                if (Input.GetButtonDown("Jump") && groundedPlayer)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }

                playerVelocity.y += gravityValue * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);*/
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }
}
