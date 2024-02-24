/// <summary>
/// The component of delivery counter
/// </summary>
public class DeliveryCounter : BaseCounter
{
    /// <summary>
    /// The only instance of DeliveryCounter
    /// </summary>
    public static DeliveryCounter Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }


    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject)
        {
            if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plate))
            {
                // Only plates

                if (DeliveryManager.Instance.TryDeliver(plate))
                {
                    plate.DestroySelf();
                }
            }
        }
    }
}
