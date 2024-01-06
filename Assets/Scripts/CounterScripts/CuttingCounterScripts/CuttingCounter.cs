using UnityEngine;

/// <summary>
/// A componenet of counter where player can cut and slice some products 
/// </summary>
public class CuttingCounter : BaseCounter
{
    /// <summary>
    /// Occurs when player cuts smth
    /// </summary>
    public event System.EventHandler OnCut;
    /// <summary>
    /// Occurs when the cutting progress is changed
    /// </summary>
    public event System.EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    /// <summary>
    /// EventArgs that contain information about new cutting progress value
    /// </summary>
    public class OnProgressChangedEventArgs : System.EventArgs
    {
        public float progressNormalized;
    }
    

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int _cuttingProgress; // The cutting progress


    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                // Player is carrying something
                if(HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
                {
                    // Player carrying smth that can be cut 
                    player.KitchenObject.KitchenObjectParent = this;
                    _cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOByInput(KitchenObject.KitchenObjectSO);

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
                    });
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is KitchenObject here
            if (player.HasKitchenObject)
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                KitchenObject.KitchenObjectParent = player;
            }
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
        if (HasKitchenObject && HasRecipeWithInput(KitchenObject.KitchenObjectSO))
        {
            // There is a KitchenObject here AND it can be cut

            _cuttingProgress++;
            OnCut?.Invoke(this, System.EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOByInput(KitchenObject.KitchenObjectSO);

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
            });

            if (_cuttingProgress >= cuttingRecipeSO.CuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(KitchenObject.KitchenObjectSO); // SO of an obj that's on counter

                KitchenObject.DestroySelf();

                // Spawning cut KitchenObject
                KitchenObject = KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOByInput(input);

        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOByInput(input);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.Output;
        }

        return null;
    }
    private CuttingRecipeSO GetCuttingRecipeSOByInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.Input == input)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
