using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float gravity = -9.8f;
    private bool isGrounded;
    public float jumpingHeight = 3f;

    void Start()
    {
      characterController=GetComponent<CharacterController>(); 
    }

    void Update()
    {
        isGrounded=characterController.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y=gravity*Time.deltaTime;
        if (isGrounded && playerVelocity.y<0)
        {
            playerVelocity.y = -2;
        }
        characterController.Move(playerVelocity * Time.deltaTime);
        Debug.Log("It Works" + playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y=Mathf.Sqrt(jumpingHeight*-3.0f*gravity);
        }
    }
}
