using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tutorial on Brackeys yt "THIRD PERSON MOVEMENT in Unity"
public class BasicPlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;
    
    public float walkSpeed = 6f;

    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        float xmove = Input.GetAxisRaw("Horizontal");
        float zmove = Input.GetAxisRaw("Vertical");
        Vector3 turnDirection = new Vector3(xmove, 0f, zmove).normalized;

        if (turnDirection.magnitude >= 0.1f){
            float targetDirection = Mathf.Atan2(turnDirection.x, turnDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // ^ get the direction of the target in radians using atan2 (Tan is y/x), the converted to degrees using Mathf.Rad2Deg, then make it according to the camera's angle around the y axis
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDirection, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDirection = Quaternion.Euler(0f, targetDirection, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
        }


        // transform.Translate(0,0, zmove);
        // transform.Translate(xmove,0,0);
        // turnDirection = (xmove * transform.right + zmove * transform.forward).normalized;
        // transform.Translate(turnDirection);


    }
}
