using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigedbody;
    int floorMask;
    float camRayLength = 100f;

     void Awake()
    {
        floorMask = LayerMask.GetMask("floor");
        anim = GetComponent<Animator>();
        playerRigedbody = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;           
        }

        Move(h, v);
        Turning();
        Animating(h, v);

    }

    void Move (float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigedbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

 
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);  

            //playerRotation at the newRotation
            playerRigedbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);

        anim.SetFloat("Speed", speed);

    }
    
    
}