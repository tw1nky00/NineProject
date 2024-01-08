using UnityEngine;

/// <summary>
/// The ScriptableObject of FryingRecipeSO contains information how to fry some products
/// </summary>
[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO input;
    [SerializeField] private KitchenObjectSO output;
    [SerializeField] private float fryingTimerMax;

    public KitchenObjectSO Output { get => output; }
    public KitchenObjectSO Input { get => input; }
    public float FryingTimerMax { get => fryingTimerMax; }
}
