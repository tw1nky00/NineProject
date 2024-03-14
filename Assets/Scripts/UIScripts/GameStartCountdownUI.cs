using Scripts.GeneralScripts;
using TMPro;
using UnityEngine;

namespace Scripts.UIScripts
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        private const string NUMBER_POPUP_TRIGGER = "NumberPopup";


        [SerializeField] private TextMeshProUGUI countdownText;

        private Animator _animator;
        private int previousCountdownNumber;


        private GeneralGameManager GameManager { get => GeneralGameManager.Instance; }


        private void Start()
        {
            _animator = GetComponent<Animator>();

            GameManager.OnStateChanged += GameManager_OnStateChanged;

            Hide();
        }
        private void Update()
        {
            if (gameObject.activeInHierarchy && GameManager.IsCountdown)
            {
                var countdownNumber = Mathf.CeilToInt(GameManager.CountdownTimerValue);
                countdownText.text = countdownNumber.ToString();
                
                if (previousCountdownNumber != countdownNumber)
                {
                    previousCountdownNumber = countdownNumber;
                    _animator.SetTrigger(NUMBER_POPUP_TRIGGER);
                    SoundManager.Instance.PlayCountdownSound();
                }
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
}
