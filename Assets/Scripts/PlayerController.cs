using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(1, 10)] float playerSpeed = 2;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float rotateSpeed = 20;
    [SerializeField] ForceMode forceMode;
    [SerializeField] Transform viewTransform;
    [SerializeField] Transform cameraTransform;
    PlayerInput playerInput;

    bool gravityFlipped = false; 

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    Vector2 input = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        Physics.gravity = new Vector3(0, -9.8f, 0);

        Debug.Log("Current Control Scheme: " + playerInput.currentControlScheme);
    }

    void Update()
    {
        MovePlayer(); 
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }

    public void OnFlipGravity()
    {
        Debug.Log("Gravity flipped!");

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

    public void OnJump()
    {
        Debug.Log("Jumped!"); 

        if (gravityFlipped == false) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        else rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
    }

    public void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
        Debug.Log("Move Pressed");
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(input.x, 0.0f, input.y);

        if (playerInput.currentControlScheme == "KeyboardMouse")
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
        }

        if (playerInput.currentControlScheme == "Gamepad")
        {
            if (Gamepad.current.leftStick.right.wasPressedThisFrame)
            {
                if (gravityFlipped == false) transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                else transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Gamepad.current.leftStick.left.wasPressedThisFrame)
            {
                if (gravityFlipped == false) transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
                else transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            if (Gamepad.current.leftStick.up.wasPressedThisFrame)
            {
                transform.position += transform.forward * playerSpeed * Time.deltaTime;
            }
            if (Gamepad.current.leftStick.down.wasPressedThisFrame)
            {
                transform.position += -transform.forward * playerSpeed * Time.deltaTime;
            }
        }
    }


}
