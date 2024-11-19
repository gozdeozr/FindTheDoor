using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;
    
    [SerializeField] private CharacterController controller;
    [SerializeField] public float speed = 2f , gravity = -9.81f , jumpHeight = 3f;
    
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private Joystick movementJoystick;

    [SerializeField] private Button jumpButton;

    Vector3 velocity;

    bool isGrounded;

    public bool isSpeedUp = false;

    private bool isRunningAudioPlaying = false;

    public bool isAudioWooden = false;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }



    void Start()
    {
        controller = GetComponent<CharacterController>();
        AudioManager.Instance.walkingSound.pitch = 0.48f;
        
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
        
        if(isSpeedUp==false && z != 0){
            controller.Move(move *speed  * Time.deltaTime);
            if(z>0.5f||z<-0.5f||x>0.5f||x<-0.5f){
                AnimationManager.Instance.CharacterWalk();
                if (!isRunningAudioPlaying && !isAudioWooden) {
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.walkingSound);
                    isRunningAudioPlaying = true;
                }
            }
            
        }
        else if(isSpeedUp==true && z != 0){
            speed = 7f;
            controller.Move(move * speed* Time.deltaTime);
            if(z>0.5f||z<-0.5f||x>0.5f||x<-0.5f){
                AnimationManager.Instance.CharacterRun();
                Debug.Log("ses çalıştı");
                if (!isRunningAudioPlaying && !isAudioWooden) {
                    AudioManager.Instance.walkingSound.pitch = 0.8f;
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.walkingSound);
                    isRunningAudioPlaying = true;
            }

            }

        }
        else{
            controller.Move(move * speed* Time.deltaTime);
            AnimationManager.Instance.CharacterStop();
            AnimationManager.Instance.CharacterStopRun();
            AudioManager.Instance.walkingSound.Stop();
            isRunningAudioPlaying = false;
    }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    
}
