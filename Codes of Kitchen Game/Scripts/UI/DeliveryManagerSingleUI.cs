using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform IngredientImage;

    private void Awake()
    {
        IngredientImage.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text=recipeSO.RecipeName;
        foreach(Transform child in iconContainer)
        {
            if(child==IngredientImage) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectsList)
        {
            Transform iconTransform= Instantiate(IngredientImage,iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite=kitchenObjectSO.sprite;
        }
    }
}
