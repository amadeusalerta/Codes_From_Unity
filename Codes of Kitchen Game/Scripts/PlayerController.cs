using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance{get;private set;}
    public static PlayerController instanceField;
    public static PlayerController GetInstanceField()
    {
        return instanceField;
    }
    public static void SetInstanceField(PlayerController instanceField)
    {
        PlayerController.instanceField=instanceField;
    }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs:EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField] private float MovementSpeed=6.5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking_01;
    private ClearCounter selectedCounter;
    private Vector3 lastInteractedDirection;

    private void Awake()
    {
        if(Instance!=null){Debug.LogError("There is more than Player Instance!");}
        Instance=this;
    }

    private void Start()
    {
        gameInput.OnInteractAction+=GameInput_OnInteractAction;
    }     

    private void GameInput_OnInteractAction(object sender,System.EventArgs e)
    {
        if (selectedCounter!=null)
        {
            selectedCounter.Interact();
        }
    }      
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
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();
    Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDirection != Vector3.zero)
    {
        lastInteractedDirection = moveDirection;
    }

    float interactDistance = 2f;
    RaycastHit raycastHit;

    // Önceki seçilen counter ile şu anki counter aynı değilse
    if (Physics.Raycast(transform.position, lastInteractedDirection, out raycastHit, interactDistance, countersLayerMask))
    {
        if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            if (clearCounter != selectedCounter)
            {
                SetSelectedCounter(clearCounter);
            }
        }
    }
    // Önceki seçilen counter ile şu anki counter aynı değilse
    else if (selectedCounter != null)
    {
        SetSelectedCounter(null);
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

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter=selectedCounter;

        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs{selectedCounter=selectedCounter});
    }
}
