using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] Transform view;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnRate;

    Vector3 velocity = Vector3.zero;
    float airTime = 0;

    void Start()
    {
        view = (view == null) ? Camera.main.transform : view;
    }

    void Update()
    {
        // xz movement
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = Vector3.ClampMagnitude(direction, 1);

        // convert direction from world space to view space
        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
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
        controller.Move(((direction * speed) + velocity) * Time.deltaTime);

        // face direction
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnRate * Time.deltaTime);
        }

        animator.SetFloat("speed", (direction * speed).magnitude);
        animator.SetFloat("velocityY", velocity.y);
        animator.SetFloat("airTime", airTime);
    }

/*    private void OnGUI()
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);

        GUI.color = Color.black;
        GUI.Label(new Rect(screen.x, Screen.height - screen.y, 300, 20), controller.velocity.ToString());
        GUI.Label(new Rect(screen.x, Screen.height - screen.y - 20, 300, 20), controller.isGrounded.ToString());
    }*/

    public void LeftFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Instantiate(go, bone.position, bone.rotation); 
        print("left"); 
    }

    public void RightFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        Instantiate(go, bone.position, bone.rotation);
        print("right"); 
    }
}
