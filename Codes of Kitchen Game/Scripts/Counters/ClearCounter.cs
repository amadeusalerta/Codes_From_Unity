using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerController player)
    {
        if(!HasKitchenObject())
        {
           if( player.HasKitchenObject())
           {
            player.GetKitchenObject().SetKitchenObjectParent(this);
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
}
