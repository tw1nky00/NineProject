using UnityEngine;

/// <summary>
/// Responsible for orders displaying
/// </summary>
public class DeliveryManagerUI : MonoBehaviour
{
    /// <summary>
    /// Container for recipe blocks
    /// </summary>
    [SerializeField] private GameObject container;
    /// <summary>
    /// A recipe template
    /// </summary>
    [SerializeField] private GameObject recipeTemplate;


    private void Awake()
    {
        recipeTemplate.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }


    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        this.UpdateVisual();
    }
    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        this.UpdateVisual();
    }


    /// <summary>
    /// Updates the visual of DeliveryManagerUI
    /// </summary>
    private void UpdateVisual()
    {
        foreach (Transform recipeBlock in container.transform)
        {
            // Deleting all the blocks

            if (recipeBlock.gameObject == recipeTemplate) continue;
            Destroy(recipeBlock.gameObject);
        }

        foreach (RecipeSO waitedRecipe in DeliveryManager.Instance.WaitedRecipesSOList)
        {
            // Adding all the recipes waited

            GameObject currentRecipeBlockSpawned = Instantiate(recipeTemplate, container.transform);
            currentRecipeBlockSpawned.SetActive(true);
        }
    }
}
