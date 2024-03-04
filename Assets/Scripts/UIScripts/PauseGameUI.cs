using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component is responsible for pause menu's behaviour
/// </summary>
public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GeneralGameManager.Instance.TogglePauseGame();
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


    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
