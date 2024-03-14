using Scripts.GeneralScripts;
using TMPro;
using UnityEngine;

namespace Scripts.UIScripts
{
    /// <summary>
    /// Responsible for tutorial UI
    /// </summary>
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyMoveUp;
        [SerializeField] private TextMeshProUGUI keyMoveDown;
        [SerializeField] private TextMeshProUGUI keyMoveLeft;
        [SerializeField] private TextMeshProUGUI keyMoveRight;
        [SerializeField] private TextMeshProUGUI keyInteract;
        [SerializeField] private TextMeshProUGUI keyInteractAlternate;


        private void Start()
        {
            GameInputManager.Instance.OnBindingRebind += GameInputManager_OnBindingRebind;
            GeneralGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

            UpdateVisual();
            Show();
        }


        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (GeneralGameManager.Instance.IsCountdown)
            {
                Hide();
            }
        }
        private void GameInputManager_OnBindingRebind(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            keyMoveUp.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.MoveUp);
            keyMoveDown.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.MoveDown);
            keyMoveLeft.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.MoveLeft);
            keyMoveRight.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.MoveRight);
            keyInteract.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.Interact);
            keyInteractAlternate.text = GameInputManager.Instance.GetBindingString(GameInputManager.Binding.InteractAlternate);
        }
        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}