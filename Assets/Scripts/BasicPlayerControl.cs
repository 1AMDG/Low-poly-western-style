using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement part on Brackeys yt "THIRD PERSON MOVEMENT in Unity"
public class BasicPlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    bool isIdle;
    bool isWalking;
    bool isRunning;
    bool isJumping;
    bool isCrouching;
    bool isRolling;
    
    public float walkSpeed = 1.25f;

    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    void Start(){
        
        isIdle = true;
        isWalking = false;
        isRunning = false;
        isJumping = false;
        isCrouching = false;
        isRolling = false;
        
    }

    void Update()
    {
        float xmove = Input.GetAxis("Horizontal");
        float zmove = Input.GetAxis("Vertical");
        Vector3 turnDirection = new Vector3(xmove, 0f, zmove).normalized;

        if (turnDirection.magnitude >= 0.1f){
            float targetDirection = Mathf.Atan2(turnDirection.x, turnDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // ^ get the direction of the target in radians using atan2 (Tan is y/x), the converted to degrees using Mathf.Rad2Deg, then make it according to the camera's angle around the y axis
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDirection, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDirection = Quaternion.Euler(0f, targetDirection, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
        }  

        if (xmove == 0 && zmove == 0){
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false); 
            isRunning = false;
            isIdle = true;
            isWalking = false;
            isRunning = false;
            isJumping = false;
            isCrouching = false;
            isRolling = false;
        }
        else if(((xmove != 0 || zmove != 0) || (xmove != 0 && zmove != 0)) && !Input.GetKey(KeyCode.LeftShift)) {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false); 
            isRunning = false;
            isIdle = false;
            isWalking = true;
            isRunning = false;
            isJumping = false;
            isCrouching = false;
            isRolling = false;
        }
        else if(((xmove != 0 || zmove != 0) || (xmove != 0 && zmove != 0)) && Input.GetKey(KeyCode.LeftShift)){
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            isRunning = true;
            isIdle = false;
            isWalking = false;
            isJumping = false;
            isCrouching = false;
            isRolling = false;

        }
        if (isRunning == true){
            walkSpeed = 2.5f;
        } else {
            walkSpeed = 1.25f;
        }
    }   
}
