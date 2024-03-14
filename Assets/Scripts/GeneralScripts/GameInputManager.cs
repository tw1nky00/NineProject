using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.GeneralScripts
{
    /// <summary>
    /// The component of InputManager is responsible for handling and returning input information
    /// </summary>
    public class GameInputManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_BINDINGS = "InputBindings";


        public static GameInputManager Instance { get; private set; }


        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            InteractAlternate
        }


        /// <summary>
        /// Occurs when a binding has been rebind
        /// </summary>
        public event System.EventHandler OnBindingRebind;
        /// <summary>
        /// An event that occurs when the interact button is pressed
        /// </summary>
        public event System.EventHandler OnInteractAction;
        /// <summary>
        /// An event that occurs when the alternate interact button is pressed
        /// </summary>
        public event System.EventHandler OnInteractAlternateAction;
        /// <summary>
        /// Occurs when the pausebutton is pressed
        /// </summary>
        public event System.EventHandler OnPauseAction;

        /// <summary>
        /// An instance of PlayerInputActions that provides tools for getting input
        /// </summary>
        private PlayerInputActions _playerInputActions;


        private void Awake()
        {
            Instance = this;

            _playerInputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }

            _playerInputActions.Player.Enable(); // let's turn this stuff on :)

            _playerInputActions.Player.Interact.performed += Interact_performed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
            _playerInputActions.Player.Pause.performed += Pause_performed;
        }
        private void OnDestroy()
        {
            _playerInputActions.Player.Interact.performed -= Interact_performed;
            _playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
            _playerInputActions.Player.Pause.performed -= Pause_performed;

            _playerInputActions.Dispose();
        }


        private void InteractAlternate_performed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, System.EventArgs.Empty);
        }
        private void Interact_performed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, System.EventArgs.Empty);
        }
        private void Pause_performed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, System.EventArgs.Empty);
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
        /// <summary>
        /// Gets a key of a binding
        /// </summary>
        /// <param name="binding">Binding which key has to be got</param>
        /// <returns>Binding key</returns>
        public string GetBindingString(Binding binding)
        {
            switch (binding)
            {
                case Binding.Interact:
                    return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();

                case Binding.InteractAlternate:
                    return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

                case Binding.MoveUp:
                    return _playerInputActions.Player.Move.bindings[1].ToDisplayString();

                case Binding.MoveDown:
                    return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                
                case Binding.MoveLeft:
                    return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                
                case Binding.MoveRight:
                    return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Rebinds a binding
        /// </summary>
        /// <param name="binding">A binding to be ribinded</param>
        /// <param name="onActionRebound">An action that should be done after finishing the rebinding</param>
        public void RebindBinding(Binding binding, System.Action onActionRebound)
        {
            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                default:
                case Binding.MoveUp:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;

                case Binding.MoveDown:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;

                case Binding.MoveLeft:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;

                case Binding.MoveRight:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;

                case Binding.Interact:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;

                case Binding.InteractAlternate:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 0;
                    break;
            }


            _playerInputActions.Player.Disable(); // Disabling the input map

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    _playerInputActions.Player.Enable();

                    onActionRebound();

                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();

                    OnBindingRebind?.Invoke(this, System.EventArgs.Empty);
                })
                .Start();
        }
    }
}