using UnityEngine;

/// <summary>
/// The component of PlayerController is responsible for controlling
/// </summary>
public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    /// <summary>
    /// The only instance of the player that can exist
    /// </summary>
    public static PlayerController Instance { get; private set; }


    /// <summary>
    /// An event that occurs when the player select another counter or leave the counter
    /// </summary>
    public event System.EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;


    /// <summary>
    /// Information about a new counter the player's looking at
    /// </summary>
    public class OnSelectedCounterChangedEventArgs : System.EventArgs
    {
        public BaseCounter selectedCounter;
    }


    /// <summary>
    /// Speed of the player
    /// </summary>
    [SerializeField] private float moveSpeed = 7f;
    /// <summary>
    /// Speed of player's rotation
    /// </summary>
    [SerializeField] private float rotationSpeed = 10f;
    /// <summary>
    /// Distance from which the player can interact with some objects
    /// </summary>
    [SerializeField] private float interactDistance = 2f;
    /// <summary>
    /// Layer mask with counters
    /// </summary>
    [SerializeField] private LayerMask countersLayerMask;
    /// <summary>
    /// A reference to the GameInput instance
    /// </summary>
    [SerializeField] private GameInput gameInputManager;
    /// <summary>
    /// The transform of the point where the kitchen object, that belongs to the player, is
    /// </summary>
    [SerializeField] private Transform kitchenObjectHoldPoint;

    /// <summary>
    /// Is the player moving?
    /// </summary>
    private bool _isWalking;
    /// <summary>
    /// Last direction of movement
    /// </summary>
    private Vector3 _lastInteractDirection;
    /// <summary>
    /// Counter that the player is looking at 
    /// </summary>
    private BaseCounter _selectedCounter;
    /// <summary>
    /// The kitchen object that the player holds
    /// </summary>
    private KitchenObject _kitchenObject;


    /// <summary>
    /// Is the player walking?
    /// </summary>
    public bool IsWalking { get => _isWalking; }

    public KitchenObject KitchenObject { get => _kitchenObject; set => _kitchenObject = value; }
    public Transform KitchenObjectFollowTransform { get => kitchenObjectHoldPoint; }
    public bool HasKitchenObject { get => _kitchenObject != null; }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PLayerController instance!");
        }

        Instance = this;
    }
    private void Start()
    {
        gameInputManager.OnInteractAction += GameInputManager_OnInteractAction;
        gameInputManager.OnInteractAlternateAction += GameInputManager_OnInteractAlternateAction;
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    /// <summary>
    /// Contains all the logic that is responsible for movement and collisions
    /// </summary>
    private void HandleMovement()
    {
        var inputVector = gameInputManager.GetMovementVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y); // direction to move

        // some float values about player
        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = 0.7f;
        var playerHeight = 2f;

        // making a ray that works like a collider
        var canMove = !Physics.CapsuleCast(transform.position,
                                          transform.position + Vector3.up * playerHeight,
                                          playerRadius,
                                          moveDirection,
                                          moveDistance);

        // this block is called when the player cannot go towards the direction
        if (!canMove)
        {
            // Cannot move towards moveDirection

            // Attempt only X movement
            var moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position,
                                           transform.position + Vector3.up * playerHeight,
                                           playerRadius,
                                           moveDirectionX,
                                           moveDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else // this block is called when the player cannot move only X so we're checking if the player is able to move on the Z
            {
                // Cannot move only on the X

                // Attempt only Z movement
                var moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,
                                               transform.position + Vector3.up * playerHeight,
                                               playerRadius,
                                               moveDirectionZ,
                                               moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        // the logic here is as simple as it is: if we can move, we move ;)
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        // if the moveDirection, after all changes is not zero vector
        _isWalking = moveDirection != Vector3.zero;

        // setting forward side of our hero (i need this for his rotation)
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// Contains all the code that is responsible for handling collisions with interactable gameObjects is here
    /// </summary>
    private void HandleInteractions()
    {
        var inputVector = gameInputManager.GetMovementVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y); // direction to move

        // saving direction if it's not zero vector
        if (moveDirection != Vector3.zero)
            _lastInteractDirection = moveDirection;

        // if the player has touched sth that player can interact with
        if (Physics.Raycast(transform.position, 
                            _lastInteractDirection, 
                            out RaycastHit raycastHit, 
                            interactDistance,
                            countersLayerMask))
        {
            // if the player has touched a clear counter 
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter) // if a selected counter has been changed
                {
                    SetSelectedCounter(baseCounter);
                }  
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    /// <summary>
    /// Sets the counter the player is looking at now
    /// </summary>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                                               {
                                                   selectedCounter = _selectedCounter,
                                               });
    }

    /// <summary>
    /// Occurs when the interaction button is pressed
    /// </summary>
    private void GameInputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }
    /// <summary>
    /// Occurs when the alternate interaction button is pressed
    /// </summary>
    private void GameInputManager_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
}
