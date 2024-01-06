using UnityEngine;

/// <summary>
/// The component of KitchenObjectParent is responsible for counters which you can place any kitchen object on
/// </summary>
public class ClearCounter : BaseCounter
{
    private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(PlayerController player)
    {
        if(!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                // Player is carrying something
                player.KitchenObject.KitchenObjectParent = this;
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
        Debug.Log("Does nothing");
    }
}
