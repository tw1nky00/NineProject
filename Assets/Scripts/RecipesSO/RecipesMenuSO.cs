using System.Collections.Generic;
using UnityEngine;

namespace Scripts.RecipesSO
{
    // [CreateAssetMenu()]
    public class RecipesMenuSO : ScriptableObject
    {
        [SerializeField] private List<RecipeSO> availableRecipes;

        public List<RecipeSO> AvailableRecipes { get => availableRecipes; }
    }
}
