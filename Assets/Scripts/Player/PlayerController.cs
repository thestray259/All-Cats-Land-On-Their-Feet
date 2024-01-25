using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(1, 10)] float playerSpeed = 2;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float rotateSpeed = 20;
    [SerializeField] ForceMode forceMode;
    [SerializeField] Transform viewTransform;
    PlayerInput playerInput;

    bool gravityFlipped = false;
    public float gravity;

    Rigidbody rb;
    Vector3 force = Vector3.zero;
    Vector2 input = Vector2.zero;

    public float playerZRotation;
    private Vector3 velocity;

    //grounded and jump
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        Physics.gravity = new Vector3(0, -9.8f, 0);

        //viewTransform = (viewTransform == null) ? Camera.main.transform : viewTransform;

        Debug.Log("Current Control Scheme: " + playerInput.currentControlScheme);
    }

    void Update()
    {
        if (playerInput.currentControlScheme == "KeyboardMouse") MovePlayer();
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
            transform.Translate(new Vector3(0, 0, input.y * playerSpeed * Time.deltaTime));

            input = playerInput.actions["Turn"].ReadValue<Vector2>();
            if (gravityFlipped == false) transform.Rotate(new Vector3(0, input.x * rotateSpeed * Time.deltaTime, 0));
            else transform.Rotate(new Vector3(0, -input.x * rotateSpeed * Time.deltaTime, 0));
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        Debug.Log("isGrounded: " + isGrounded);
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
    }

    public void OnFlipGravity()
    {
        gravityFlipped = !gravityFlipped;
        velocity.y = 0;
        Physics.gravity *= -1;
        transform.eulerAngles += new Vector3(0, 0, 180);

        viewTransform.transform.eulerAngles += new Vector3(0, 0, 180);
        viewTransform.transform.LookAt(transform.position);
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            if (gravityFlipped == false) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            else rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);            
        }
    }

    public void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }

    void MovePlayer()
    {
        // xz movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1);

        // convert direction from world space to view space
        Quaternion viewSpace = Quaternion.AngleAxis(viewTransform.rotation.eulerAngles.y, Vector3.up);
        direction = viewSpace * direction;

        // face direction
        //if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

        if (playerInput.currentControlScheme == "KeyboardMouse")
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (gravityFlipped == false) transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
                else transform.Rotate(rotateSpeed * Time.deltaTime * -Vector3.up);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (gravityFlipped == false) transform.Rotate(rotateSpeed * Time.deltaTime * -Vector3.up);
                else transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
            }

            // add checks for if gravity is flipped
            // rn it's changing the orientation of the cat when it is flipped
            if (gravityFlipped == false)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
                    transform.position += playerSpeed * Time.deltaTime * transform.forward;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
                    transform.position += playerSpeed * Time.deltaTime * transform.forward;
                }
            }
            else
            {
                //direction.y *= -1;
                Vector3 flipDirection = new Vector3(direction.x, 0);
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
                    transform.position += playerSpeed * Time.deltaTime * transform.forward;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
                    transform.position += playerSpeed * Time.deltaTime * transform.forward;
                }
            }
        }
    }
}
