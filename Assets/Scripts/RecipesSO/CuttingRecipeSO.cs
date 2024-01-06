using UnityEngine;

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
