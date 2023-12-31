using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance{get;private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax=4f;
    private int waitingRecipeMax=4;
    private int succesfulRecipesAmount;

    private void Awake()
    {
        Instance=this;
        waitingRecipeSOList=new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer-=Time.deltaTime;
        if(spawnRecipeTimer<=0f)
        {
            spawnRecipeTimer=spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count<waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO=recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.RecipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i=0;i<waitingRecipeSOList.Count;i++)
        {
            RecipeSO waitingRecipeSO=waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectsList.Count==plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe=true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsList)
                {
                    bool ingredientFound=false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO==recipeKitchenObjectSO)
                        {
                            ingredientFound=true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        plateContentsMatchesRecipe=false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    succesfulRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        Debug.Log("False Recipe");
    }

    public List<RecipeSO> GetWaitingRecipeSO()
    {
        return waitingRecipeSOList;
    }
    
    public int GetSuccesfulRecipesAmount()
    {
        return succesfulRecipesAmount;
    }
}
