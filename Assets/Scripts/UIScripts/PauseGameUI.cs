using System.Reflection.Emit;
using UnityEngine;

public class PauseGameUI : MonoBehaviour
{
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
