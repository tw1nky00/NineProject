using UnityEngine;

/// <summary>
/// The component of PlatesCounter is responsible for contolling PlatesCounter 
/// </summary>
public class PlatesCounter : BaseCounter
{
    /// <summary>
    /// Occurs when a new plate is spawned
    /// </summary>
    public event System.EventHandler OnPlateSpawned;
    /// <summary>
    /// Occurs when the plate is taken
    /// </summary>
    public event System.EventHandler OnPlateRemoved;


    /// <summary>
    /// A kitchen object of plate
    /// </summary>
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    /// <summary>
    /// Time between spawning plates
    /// </summary>
    [SerializeField] private float spawnPlateTimerMax;
    /// <summary>
    /// The biggest amount of plates that can be placed on the counter
    /// </summary>
    [SerializeField] private int platesSpawnedAmountMax;

    private float spawnPlateTimer;
    private float platesSpawnedAmount;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject && platesSpawnedAmount > 0)
        {
            platesSpawnedAmount--;

            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

            OnPlateRemoved?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
