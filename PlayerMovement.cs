using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 2f , gravity = -9.81f , jumpHeight = 3f, Xspeed = 3f;
    
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private Joystick movementJoystick;

    [SerializeField] private Button jumpButton;

    Vector3 velocity;

    bool isGrounded;





    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpButton.onClick.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = movementJoystick.Horizontal;
        float z = movementJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;


        if(z>0.8f || Input.GetKey(KeyCode.LeftShift)){
            controller.Move(move * Xspeed * Time.deltaTime);
        }
        else{
            controller.Move(move * speed * Time.deltaTime);

        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Jump(){

        if(isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
