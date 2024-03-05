using Scripts.GeneralScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UIScripts
{
    /// <summary>
    /// Responsible for OptionsUI behaviour
    /// </summary>
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance { get; private set; }


        [Header("Main buttons")]
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button backButton;

        [Header("Main text")]
        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;

        [Header("Bindings buttons")]
        [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAlternateButton;

        [Header("Bindings text")]
        [SerializeField] private TextMeshProUGUI moveUpButtonText;
        [SerializeField] private TextMeshProUGUI moveDownButtonText;
        [SerializeField] private TextMeshProUGUI moveLeftButtonText;
        [SerializeField] private TextMeshProUGUI moveRightButtonText;
        [SerializeField] private TextMeshProUGUI interactButtonText;
        [SerializeField] private TextMeshProUGUI interactAlternateButtonText;

        [Header("")]
        [SerializeField] private GameObject pressToRebindKeyObject;


        // Local field just for me
        private GameInputManager InputManager { get => GameInputManager.Instance; }


        private void Awake()
        {
            Instance = this;

            soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            backButton.onClick.AddListener(() =>
            {
                Hide();
                PauseGameUI.Instance.Show();
            });

            moveUpButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.MoveUp); });
            moveDownButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.MoveDown); });
            moveLeftButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.MoveLeft); });
            moveRightButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.MoveRight); });
            interactButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.Interact); });
            interactAlternateButton.onClick.AddListener(() => { Rebind(GameInputManager.Binding.InteractAlternate); });
        }
        private void Start()
        {
            GeneralGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            UpdateVisual();

            HidePressToRebindKey();
            Hide();
        }


        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            Hide();
        }


        /// <summary>
        /// Reveals the UI
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Hides the UI
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            
        }


        private void ShowPressToRebindKey()
        {
            pressToRebindKeyObject.SetActive(true);
        }
        private void HidePressToRebindKey()
        {
            pressToRebindKeyObject.SetActive(false);
        }
        private void UpdateVisual()
        {
            soundEffectsText.text = $"Sound effects: {Mathf.Round(SoundManager.Instance.Volume * 10f)}";
            musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.Volume * 10f)}";

            moveUpButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.MoveUp);
            moveDownButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.MoveDown);
            moveLeftButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.MoveLeft);
            moveRightButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.MoveRight);
            interactButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.Interact);
            interactAlternateButtonText.text = InputManager.GetBindingString(GameInputManager.Binding.InteractAlternate);
        }

        private void Rebind(GameInputManager.Binding binding)
        {
            ShowPressToRebindKey();

            InputManager.RebindBinding(binding, () =>
            {
                HidePressToRebindKey();
                UpdateVisual();
            });
        }
    }
}
