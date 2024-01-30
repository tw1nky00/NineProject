using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for single block in DeliveryManagerUI
/// </summary>
public class DeliveryManagerSingleUI : MonoBehaviour
{
    /// <summary>
    /// The recipe name
    /// </summary>
    [SerializeField] private TextMeshProUGUI recipeNameText;


    /// <summary>
    /// Sets the RecipeSO to be displayed to this single UI block
    /// </summary>
    /// <param name="recipe"></param>
    public void SetRecipeSO(RecipeSO recipe)
    {
        this.recipeNameText.text = recipe.RecipeName;
    }
}
