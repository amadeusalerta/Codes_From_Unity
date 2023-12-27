using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerController : MonoBehaviour,IKitchenObjectParent
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
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float MovementSpeed=6.5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField]private Transform kitchenObjectHoldPoint;
    private bool isWalking_01;
    private BaseCounter selectedCounter;
    private Vector3 lastInteractedDirection;
    private KitchenObject kitchenObject;

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
            selectedCounter.Interact(this);
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
        if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
        {
            if (baseCounter != selectedCounter)
            {
                SetSelectedCounter(baseCounter);
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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter=selectedCounter;

        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs{selectedCounter=selectedCounter});
    }
        public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject=kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject=null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject!=null;
    }
}
