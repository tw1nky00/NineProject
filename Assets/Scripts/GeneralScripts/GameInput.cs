using UnityEngine;

/// <summary>
/// The component of GameInput is responsible for handling and returning input information
/// </summary>
public class GameInput : MonoBehaviour
{
    /// <summary>
    /// An event that occurs when the interact button is pressed
    /// </summary>
    public event System.EventHandler OnInteractAction;
    /// <summary>
    /// An event that occurs when the alternate interact button is pressed
    /// </summary>
    public event System.EventHandler OnInteractAlternateAction;

    /// <summary>
    /// An instance of PlayerInputActions that provides tools for getting input
    /// </summary>
    private PlayerInputActions _playerInputActions;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable(); // let's turn this stuff on :)

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }


    /// <summary>
    /// Occurs when the alternate interaction button is performed
    /// </summary>
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, System.EventArgs.Empty);
    }

    /// <summary>
    /// Occurs when the interaction button is performed
    /// </summary>
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, System.EventArgs.Empty);
    }

    /// <summary>
    /// Returns a direction to move using InputActions
    /// </summary>
    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
