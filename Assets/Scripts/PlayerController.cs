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
    [SerializeField] Transform cameraTransform;

    //private CharacterController controller;
    //private Animator animator;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float gravityValue = -9.81f;

    //Vector3 gravity = Physics.gravity; 
    bool gravityFlipped = false; 

    //float airTime = 0; 

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    //Vector3 velocity = Vector3.zero;

    private void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        //animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(0, -9.8f, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (gravityFlipped == false) transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            else transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (gravityFlipped == false) transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            else transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
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
            if (gravityFlipped == false) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            else rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gravityFlipped == false)
            {
                Physics.gravity = new Vector3(0, 9.8f, 0);
                gravityFlipped = true;
                transform.eulerAngles += new Vector3(0, 0, 180);

                viewTransform.transform.eulerAngles += new Vector3(0, 0, 180);
                //cameraTransform.transform.eulerAngles += new Vector3(0, 0, 180);
            }
            else
            {
                Physics.gravity = new Vector3(0, -9.8f, 0);
                gravityFlipped = false;
                transform.eulerAngles -= new Vector3(0, 0, 180);

                viewTransform.transform.eulerAngles -= new Vector3(0, 0, 180);
                //cameraTransform.transform.eulerAngles -= new Vector3(0, 0, 180);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }
}
