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
