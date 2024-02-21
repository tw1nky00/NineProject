using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    /// An icon container
    /// </summary>
    [SerializeField] private GameObject iconContainer;
    /// <summary>
    /// An ingridient icon template
    /// </summary>
    [SerializeField] private GameObject ingridientTemplate;


    private void Start()
    {
        this.ingridientTemplate.SetActive(false);
    }


    /// <summary>
    /// Sets the RecipeSO to be displayed to this single UI block
    /// </summary>
    /// <param name="recipe"></param>
    public void SetRecipeSO(RecipeSO recipe)
    {
        this.recipeNameText.text = recipe.RecipeName;

        foreach (Transform icon in iconContainer.transform)
        {
            // Deleting all the icons

            if (icon == this.ingridientTemplate) continue;
            Destroy(icon.gameObject);
        }


        foreach (KitchenObjectSO ingridient in recipe.IngridientsList)
        {
            // Spawning all the ingridient icons
            GameObject currentIngridientIconSpawned = Instantiate(this.ingridientTemplate, this.iconContainer.transform);
            currentIngridientIconSpawned.SetActive(true);

            // Setting an image
            currentIngridientIconSpawned.GetComponent<Image>().sprite = ingridient.Sprite;
            currentIngridientIconSpawned.GetComponent<Image>().enabled = true;
        }
    }
}
