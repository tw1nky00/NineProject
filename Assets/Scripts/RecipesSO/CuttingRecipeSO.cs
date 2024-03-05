using Scripts.KitchenObjectScripts;
using UnityEngine;

namespace Scripts.RecipesSO
{
    /// <summary>
    /// The ScriptableObject of cutting recipe contains information how to cut some products
    /// </summary>
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        [SerializeField] private KitchenObjectSO input;
        [SerializeField] private KitchenObjectSO output;
        [SerializeField] private int cuttingProgressMax;

        public KitchenObjectSO Output { get => output; }
        public KitchenObjectSO Input { get => input; }
        public int CuttingProgressMax { get => cuttingProgressMax; }
    }
}
