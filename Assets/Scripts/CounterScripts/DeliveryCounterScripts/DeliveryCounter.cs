using UnityEngine;

/// <summary>
/// The component of delivery counter
/// </summary>
public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject)
        {
            if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plate))
            {
                // Only plates
                plate.DestroySelf();
            }
        }
    }
}
