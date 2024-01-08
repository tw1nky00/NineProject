using UnityEngine;

/// <summary>
/// The component of StoveCounter is responsible for controlling stove counters (you can fry there)
/// </summary>
public class StoveCounter : BaseCounter
{
    /// <summary>
    /// Occurs when the state of StoveCounter is changed
    /// </summary>
    public event System.EventHandler<OnStateChangedEventArgs> OnStateChanged;
    /// <summary>
    /// EventArgs that contains information about a new state 
    /// </summary>
    public class OnStateChangedEventArgs : System.EventArgs
    {
        public State state;
    }


    /// <summary>
    /// The state of the counter and of the object placed on it
    /// </summary>
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO _currentFryingRecipeSO;
    private BurningRecipeSO _currentBurningRecipeSO;
    private State _state;

    private float _fryingTimer;
    private float _burningTimer;


    private void Start()
    {
        _state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject)
        {
            switch (_state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    if (_fryingTimer >= _currentFryingRecipeSO.FryingTimerMax)
                    {
                        // Fried

                        KitchenObject.DestroySelf();

                        KitchenObject.SpawnKitchenObject(_currentFryingRecipeSO.Output, this);

                        _state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });

                        _burningTimer = 0f;

                        _currentBurningRecipeSO = GetBurningRecipeSOByInput(KitchenObject.KitchenObjectSO);
                    }

                    Debug.Log(_fryingTimer);

                    break;

                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    if (_burningTimer >= _currentBurningRecipeSO.BurningTimerMax)
                    {
                        // Burned

                        KitchenObject.DestroySelf();

                        KitchenObject.SpawnKitchenObject(_currentBurningRecipeSO.Output, this);

                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    }

                    break;

                case State.Burned:
                    break;
            }
        }
    }


    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.KitchenObject.KitchenObjectSO))
                {
                    // Player carrying smth that can be fried 
                    player.KitchenObject.KitchenObjectParent = this;

                    _currentFryingRecipeSO = GetFryingRecipeSOByInput(KitchenObject.KitchenObjectSO);

                    _state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });

                    _fryingTimer = 0f;
                }
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

                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOByInput(input);

        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOByInput(input);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.Output;
        }

        return null;
    }
    private FryingRecipeSO GetFryingRecipeSOByInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.Input == input)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOByInput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.Input == input)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
