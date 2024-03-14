using Scripts.GeneralScripts;
using UnityEngine;

namespace Scripts.CounterScripts.StoveCounterScripts
{
    /// <summary>
    /// Responsible for StoveCounter's sounds
    /// </summary>
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private AudioSource _audioSource;
        private bool _shouldPlay;
        private float _warningSoundTimer;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        }
        private void Update()
        {
            if (_shouldPlay)
            {
                _warningSoundTimer -= Time.deltaTime;
                if (_warningSoundTimer <= 0f)
                {
                    float warningSoundTimerMax = 0.2f;
                    _warningSoundTimer = warningSoundTimerMax;

                    SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
                }
            }
        }


        private void StoveCounter_OnProgressChanged(object sender, UIScripts.IHasProgress.OnProgressChangedEventArgs e)
        {
            var showProgressValue = 0.5f;
            _shouldPlay = stoveCounter.IsFried && e.progressNormalized >= showProgressValue;
        }
        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            bool shouldPlay = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

            if (shouldPlay)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Pause();
            }
        }
    }
}
