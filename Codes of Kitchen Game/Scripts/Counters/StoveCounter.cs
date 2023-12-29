using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs:EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipe[] fryingRecipesSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipesSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipe fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;
    private void Update()
    {

        if(HasKitchenObject())
        {
          switch (state)
          {
            case State.Idle:
            break;
            case State.Frying:
            fryingTimer+=Time.deltaTime;
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=fryingTimer/fryingRecipeSO.fryingTimerMax});
            if(fryingTimer>fryingRecipeSO.fryingTimerMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output,this);

                state=State.Fried;
                burningTimer=0f;
                burningRecipeSO=GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{state=state});
            }
            break;
            case State.Fried:
            burningTimer+=Time.deltaTime;
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=burningTimer/burningRecipeSO.burningTimerMax});
            if(burningTimer>burningRecipeSO.burningTimerMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(burningRecipeSO.output,this);

                state=State.Burned;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{state=state});
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=0f});
            }
            break;
            case State.Burned:
            break;
          }

        }
    }

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO=GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state=State.Frying;
                    fryingTimer=0f;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{state=state});
                }

                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=fryingTimer/fryingRecipeSO.fryingTimerMax});
            }
            else
            {
                // Handle when the player doesn't have a kitchen object
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
               if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){GetKitchenObject().DestroySelf();
                state=State.Idle;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{state=state});
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=0f});}
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state=State.Idle;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{state=state});
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=0f});
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipe fryingRecipeSO = GetFryingRecipeWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipe fryingRecipeSO = GetFryingRecipeWithInput(inputKitchenObjectSO);
        return fryingRecipeSO?.output;
    }

    private FryingRecipe GetFryingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipe fryingRecipeSO in fryingRecipesSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipesSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
