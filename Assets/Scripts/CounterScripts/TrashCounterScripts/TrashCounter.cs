using UnityEngine;

/// <summary>
/// The component of trash counter
/// </summary>
public class TrashCounter : BaseCounter
{
    /// <summary>
    /// Occurs when a plate is trashed
    /// </summary>
    public static event System.EventHandler OnAnyThrownAway;


    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject)
        {
            Destroy(player.KitchenObject.gameObject);
            OnAnyThrownAway?.Invoke(this, System.EventArgs.Empty);
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
        Debug.Log("Does nothing");
    }
}
