using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for giving and recieving orders
/// </summary>
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    

    /// <summary>
    /// List of available recipes
    /// </summary>
    [SerializeField] private RecipesMenuSO menu;
    /// <summary>
    /// Time between spawning orders
    /// </summary>
    [SerializeField] private float spawnTimerMax;
    /// <summary>
    /// The maximum value of orders spawned
    /// </summary>
    [SerializeField] private int waitedRecipesMax;

    private List<RecipeSO> _waitedRecipesSOList;

    private float _spawnTimer;


    public List<RecipeSO> WaitedRecipesSOList { get => _waitedRecipesSOList; }


    private void Awake()
    {
        Instance = this;

        _waitedRecipesSOList = new List<RecipeSO>();
        _spawnTimer = spawnTimerMax;
    }
    private void Update()
    {
        if (_waitedRecipesSOList.Count < waitedRecipesMax)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnTimer = spawnTimerMax;

                if (_waitedRecipesSOList.Count < waitedRecipesMax)
                {
                    RecipeSO newWaitedRecipeSO = menu.AvailableRecipes[Random.Range(0, menu.AvailableRecipes.Count)];

                    _waitedRecipesSOList.Add(newWaitedRecipeSO);
                    this.OnRecipeSpawned?.Invoke(this, System.EventArgs.Empty);


                    Debug.Log(newWaitedRecipeSO.RecipeName);
                }
            }
        }
    }


    /// <summary>
    /// Delivers an order to a customer
    /// </summary>
    /// <param name="plate">A plate with an order</param>
    /// <returns>Was the order delivered or not?</returns>
    public bool TryDeliver(PlateKitchenObject plate)
    {
        for (int i = 0; i < _waitedRecipesSOList.Count; i++)
        {
            RecipeSO recipe = _waitedRecipesSOList[i];
            
            if (recipe.IngridientsList.Count == plate.IngridiendsList.Count)
            {
                // Has the same number of ingridients

                bool plateContentMatchesRecipe = true;

                foreach (KitchenObjectSO recipeIngridient in recipe.IngridientsList)
                {
                    bool isIngridientFound = false;

                    // Cycling through all ingridients in the Recipe
                    foreach (KitchenObjectSO plateIngridient in plate.IngridiendsList)
                    {
                        // Cycling through all ingridients at the plate
                        if (recipeIngridient == plateIngridient)
                        {
                            isIngridientFound = true;
                            break;
                        }
                    }

                    if (!isIngridientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    Debug.Log("Player has delivered the correct recipe!");
                    _waitedRecipesSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, System.EventArgs.Empty);
                    return true;
                }
            }
        }

        // No matches at all
        // Player aint't delivered correct recipe
        Debug.Log("Player aint't delivered correct recipe");

        return false;
    }
}
