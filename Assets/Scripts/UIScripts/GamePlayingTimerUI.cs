using Scripts.GeneralScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UIScripts
{
    public class GamePlayingTimerUI : MonoBehaviour
    {
        [SerializeField] private Image timerImage;


        private GeneralGameManager GameManager { get => GeneralGameManager.Instance; }


        private void Start()
        {
            GameManager.OnStateChanged += GameManager_OnStateChanged;

            Hide();
        }
        private void Update()
        {
            if (gameObject.activeInHierarchy && GameManager.IsGamePlaying)
            {
                timerImage.fillAmount = GameManager.GamePlayingTimerValueNormalized;
            }
        }


        private void GameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (GameManager.IsGamePlaying)
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
}
