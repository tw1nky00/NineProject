using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for giving and recieving orders
/// </summary>
public class DeliveryManager : MonoBehaviour
{
    /// <summary>
    /// Occurs when a new recipe is spawned
    /// </summary>
    public event System.EventHandler OnRecipeSpawned;
    /// <summary>
    /// Occurs when the recipe is successefully delivered
    /// </summary>
    public event System.EventHandler OnRecipeCompleted;
    /// <summary>
    /// Occurs when the recipe that was ordered is delivered
    /// </summary>
    public event System.EventHandler OnRecipeSuccessed;
    /// <summary>
    /// Occurs when the recipe that was not ordered is delivered
    /// </summary>
    public event System.EventHandler OnRecipeFailed;


    /// <summary>
    /// The only instance of the DeliveryManager
    /// </summary>
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


    /// <summary>
    /// The list of recipes which are ordered right now
    /// </summary>
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
                    // Cycling through all ingridients in the Recipe

                    bool isIngridientFound = false;
                    
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
                    _waitedRecipesSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, System.EventArgs.Empty);
                    OnRecipeSuccessed?.Invoke(this, System.EventArgs.Empty);

                    return true;
                }
            }
        }

        // No matches at all
        // Player aint't delivered correct recipe
        OnRecipeFailed?.Invoke(this, System.EventArgs.Empty);

        return false;
    }
}
