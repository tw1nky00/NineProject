using UnityEngine;

/// <summary>
/// Basic class of all the counters
/// </summary>
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    /// <summary>
    /// Occurs when a KithcenObject is placed on the counter
    /// </summary>
    public static event System.EventHandler OnAnyKitchenObjectDropped;


    /// <summary>
    /// The transform of the top of the counter (the parent for a kitchen object)
    /// </summary>
    [SerializeField] private Transform counterTopPoint;

    /// <summary>
    /// The kitchen object placed on the counter
    /// </summary>
    private KitchenObject _kitchenObject;


    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set
        {
            _kitchenObject = value;

            if (_kitchenObject != null)
                OnAnyKitchenObjectDropped?.Invoke(this, System.EventArgs.Empty);
        }
    }
    public Transform KitchenObjectFollowTransform { get => counterTopPoint; }
    public bool HasKitchenObject { get => _kitchenObject != null; }


    public static void ResetStaticData()
    {
        OnAnyKitchenObjectDropped = null;
    }


    /// <summary>
    /// The metod which contains the way the player interacts with counter
    /// </summary>
    public virtual void Interact(PlayerController player)
    {
        Debug.LogError("Not able to interact with BaseCounter instance (but you can interact with children)");
    }
    /// <summary>
    /// The metod which contains the way the player alternatively interacts with counter
    /// </summary>
    public virtual void InteractAlternate(PlayerController player)
    {
        // Debug.LogError("Not able to interact alternatively with BaseCounter instance (but you can interact with children)");
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
}
