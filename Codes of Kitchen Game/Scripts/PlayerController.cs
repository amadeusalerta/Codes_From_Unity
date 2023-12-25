using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed=6.5f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking_01;
    private Vector3 lastInteractedDirection;
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool isWalking(){
        return isWalking_01;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector=gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection=new Vector3(inputVector.x,0f,inputVector.y);

        if (moveDirection!=Vector3.zero)
        {
            lastInteractedDirection=moveDirection;
        }
        float interactDistance=2f;
        RaycastHit raycastHit;
        if ( Physics.Raycast(transform.position,moveDirection,out raycastHit,interactDistance))
        {
            Debug.Log(raycastHit.transform);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector=gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection=new Vector3(inputVector.x,0f,inputVector.y);

        float moveDistance=MovementSpeed*Time.deltaTime;
        float playerSize=0.7f;
        float playerHeight=2f;
        bool canMove=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerSize,moveDirection,moveDistance);

        if(!canMove)
        {
            Vector3 movedirX=new Vector3(moveDirection.x,0,0).normalized;
            canMove=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerSize,movedirX,moveDistance);

            if (canMove)
            {
                moveDirection=movedirX;
            }
            else
            {
                Vector3 movedirZ=new Vector3(0,0,moveDirection.z).normalized;
                canMove=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerSize,movedirZ,moveDistance);

                if(canMove)
                {
                    moveDirection=movedirZ;
                }
                else
                {

                }
            }
        }

        if (canMove)
        {
            transform.position+=moveDirection*moveDistance;
        }

        isWalking_01=moveDirection!=Vector3.zero;

        float rotationSpeed=10f;
        transform.forward=Vector3.Slerp(transform.forward, moveDirection,Time.deltaTime*rotationSpeed);
    }
}
