using Scripts.KitchenObjectScripts;
using Scripts.PlayerScripts;
using UnityEngine;

namespace Scripts.CounterScripts.ContainerCounterScripts
{
    /// <summary>
    /// A component of counter that gives products to the player 
    /// </summary>
    public class ContainerCounter : BaseCounter
    {
        /// <summary>
        /// Fired when player has grabbed a kitchen object
        /// </summary>
        public event System.EventHandler OnPlayerGrabbedObject;


        /// <summary>
        /// The Scriptable Object which contains the information about the kitchen object being spawned
        /// </summary>
        [SerializeField] private KitchenObjectSO kitchenObjectSO;


        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject)
            {
                // Player is carrying something

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, System.EventArgs.Empty);
            }
        }
        public override void InteractAlternate(PlayerController player)
        {
            Debug.Log("Does nothing");
        }
    }
}