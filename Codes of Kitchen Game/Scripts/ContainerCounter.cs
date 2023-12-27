using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerController player)
    {
        Transform kitchenObjectTransfrom=Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
    }

}
