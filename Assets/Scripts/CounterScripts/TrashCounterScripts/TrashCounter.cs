using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject)
        {
            Destroy(player.KitchenObject.gameObject);
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
        Debug.Log("Does nothing");
    }
}
