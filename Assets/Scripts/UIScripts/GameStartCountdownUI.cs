using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    private GeneralGameManager GameManager { get => GeneralGameManager.Instance; }


    private void Start()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy && GameManager.IsCountdown)
        {
            countdownText.text = Mathf.Ceil(GameManager.CountdownTimerValue).ToString();
        }
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.IsCountdown)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
