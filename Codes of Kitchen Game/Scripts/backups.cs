using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backups : MonoBehaviour
{
    /*private void HandleInteractions()
    {
        Vector2 inputVector=gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection=new Vector3(inputVector.x,0f,inputVector.y);

        if (moveDirection!=Vector3.zero)
        {
            lastInteractedDirection=moveDirection;
        }
        float interactDistance=2f;
        RaycastHit raycastHit;
        if ( Physics.Raycast(transform.position,lastInteractedDirection,out raycastHit,interactDistance,countersLayerMask))
        {

         if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
         {
            if (clearCounter!=selectedCounter)
            {
                SetSelectedCounter(clearCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
         }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }*/

    /*private void HandleInteractions()
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
*/
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void Interact(PlayerController player)
    {
        if(!HasKitchenObject())
        {
           if( player.HasKitchenObject())
           {
            if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            cuttingProgress=0;
            }
           }
           else
           {
           }
        }
        else
        {
            if(player.HasKitchenObject())
            {
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(PlayerController player)
    {
        if(HasKitchenObject()&&HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            if(cuttingProgress>=cuttingRecipeSO.cuttingProgressMax)
            {
            KitchenObjectSO outputKitchenObjectSO=GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO,this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO!=null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO!=null)
        {
            return cuttingRecipeSO.input;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input==inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

*/