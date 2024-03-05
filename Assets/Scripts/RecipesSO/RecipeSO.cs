using Scripts.KitchenObjectScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.RecipesSO
{
    /// <summary>
    /// The ScriptableObject of RecipeSO contains information what ingridients are used in a dish
    /// </summary>
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] private List<KitchenObjectSO> ingridientsList;
        [SerializeField] private string recipeName;

        public List<KitchenObjectSO> IngridientsList { get => ingridientsList; }
        public string RecipeName { get => recipeName; }
    }
}
