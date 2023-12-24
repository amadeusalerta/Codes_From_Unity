using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed=6.5f;
    private bool isWalking_01;
    void Update()
    {
        Vector2 inputVector=new Vector2(0,0);
        if (Input.GetKey(KeyCode.W)){
            inputVector.y=+1;
        }
                if (Input.GetKey(KeyCode.S)){
            inputVector.y=-1;
        }
                if (Input.GetKey(KeyCode.D)){
            inputVector.x=+1;
        }
                if (Input.GetKey(KeyCode.A)){
            inputVector.x=-1;
        }
        
        inputVector=inputVector.normalized;
        Vector3 moveDirection=new Vector3(inputVector.x,0f,inputVector.y);
        transform.position+=moveDirection*Time.deltaTime*MovementSpeed;

        isWalking_01=moveDirection!=Vector3.zero;
        float rotationSpeed=10f;
        transform.forward=Vector3.Slerp(transform.forward, moveDirection,Time.deltaTime*rotationSpeed);
        Debug.Log(Time.deltaTime);
    }

    public bool isWalking(){
        return isWalking_01;
    }
}
