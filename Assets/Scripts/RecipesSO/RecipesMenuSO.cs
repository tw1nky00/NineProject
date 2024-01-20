using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu()]
public class RecipesMenuSO : ScriptableObject
{
    [SerializeField] private List<RecipeSO> availableRecipes;

    public List<RecipeSO> AvailableRecipes { get => availableRecipes; }
}
