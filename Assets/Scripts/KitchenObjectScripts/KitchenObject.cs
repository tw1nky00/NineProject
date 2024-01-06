using UnityEngine;

/// <summary>
/// The component of KitchenObject represents any food and other placeable objects
/// </summary>
public class KitchenObject : MonoBehaviour
{
    /// <summary>
    /// A reference to the scriptable object which the KitchenObject prefab belongs to
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    /// <summary>
    /// A reference to a counter where this kichen object is placed
    /// </summary>
    private IKitchenObjectParent _kitchenObjectParent;


    /// <summary>
    /// A reference to a counter where this kichen object is placed
    /// (when setting this property kitchen object stands into the right position)
    /// </summary>
    public IKitchenObjectParent KitchenObjectParent
    {
        get => _kitchenObjectParent;
        set
        {
            if (_kitchenObjectParent != null) // previous KitchenObjectParent
            {
                _kitchenObjectParent.ClearKitchenObject();
            }

            _kitchenObjectParent = value; // now the KitchenObjectParent's been changed

            if (_kitchenObjectParent.HasKitchenObject)
            {
                Debug.LogError("IKitchenObjectParent already has a KitchenObject");
            }

            _kitchenObjectParent.KitchenObject = this;

            // if i set the KitchenObjectParent, then it has been changed for the KitchenObject
            transform.parent = _kitchenObjectParent.KitchenObjectFollowTransform;
            transform.localPosition = Vector3.zero;
        }
    }
    /// <summary>
    /// A reference to the scriptable object which the KitchenObject prefab belongs to
    /// </summary>
    public KitchenObjectSO KitchenObjectSO { get => kitchenObjectSO; }


    /// <summary>
    /// Spawns a new KitchenObject
    /// </summary>
    /// <param name="kitchenObjectSO">Scribtable object of a KitchenObject to spawn</param>
    /// <param name="kitchenObjectParent">A parent which the new KitchenObject is going to belong to</param>
    /// <returns>The new KitchenObject</returns>
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        GameObject kitchenObjectSpawned = Instantiate(kitchenObjectSO.Prefab);

        KitchenObject kitchenObject = kitchenObjectSpawned.GetComponent<KitchenObject>();

        kitchenObject.KitchenObjectParent = kitchenObjectParent;

        return kitchenObject;
    }


    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }
}
