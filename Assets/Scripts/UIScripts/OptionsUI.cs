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


        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button backButton;

        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;


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
        }
        private void Start()
        {
            GeneralGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            UpdateVisual();

            Hide();
        }


        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            Hide();
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            
        }

        private void UpdateVisual()
        {
            soundEffectsText.text = $"Sound effects: {Mathf.Round(SoundManager.Instance.Volume * 10f)}";
            musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.Volume * 10f)}";
        }
    }
}
