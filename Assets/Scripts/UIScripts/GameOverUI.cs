using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredCountText;

    private GeneralGameManager GameManager { get => GeneralGameManager.Instance; }


    private void Start()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.IsGameOver)
        {
            Show();


            recipesDeliveredCountText.text = DeliveryManager.Instance.SuccessefulDeliversCount.ToString();
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
