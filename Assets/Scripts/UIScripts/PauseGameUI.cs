using Scripts.GeneralScripts;
using Scripts.MainMenuScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UIScripts
{
    /// <summary>
    /// This component is responsible for pause menu's behaviour
    /// </summary>
    public class PauseGameUI : MonoBehaviour
    {
        public static PauseGameUI Instance { get; private set; }


        [SerializeField] private Button resumeButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button mainMenuButton;


        private void Awake()
        {
            Instance = this;

            resumeButton.onClick.AddListener(() =>
            {
                GeneralGameManager.Instance.TogglePauseGame();
            });
            optionsButton.onClick.AddListener(() =>
            {
                OptionsUI.Instance.Show();
                Hide();
            });
            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenu);
            });
        }
        private void Start()
        {
            GeneralGameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
            GeneralGameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

            Hide();
        }


        private void GameManager_OnGamePaused(object sender, System.EventArgs e)
        {
            Show();
        }
        private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
        {
            Hide();
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
