using UnityEngine;

namespace Scripts.GeneralScripts
{
    /// <summary>
    /// Responsible for game controlling: timers, game start etc.
    /// </summary>
    public class GeneralGameManager : MonoBehaviour
    {
        public static GeneralGameManager Instance { get; private set; }


        /// <summary>
        /// The state of game main timer
        /// </summary>
        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }


        /// <summary>
        /// Occurs when the state of the main game timer has changed
        /// </summary>
        public event System.EventHandler OnStateChanged;
        /// <summary>
        /// Occurs when the game has been paused
        /// </summary>
        public event System.EventHandler OnGamePaused;
        /// <summary>
        /// Occurs when the game has been unpaused
        /// </summary>
        public event System.EventHandler OnGameUnpaused;


        [SerializeField] private float countdownToStartTimerValue;
        [SerializeField] private float gamePlayingTimerValue;


        private State _state;

        private float _countdownToStartTimer;
        private float _gamePlayingTimer;

        private bool _isGamePaused;


        /// <summary>
        /// Returns true if the game is being played (not being prepared to play or gameover)  
        /// </summary>
        public bool IsGamePlaying { get => _state == State.GamePlaying; }
        /// <summary>
        /// Returns true if before the game timer is counting down
        /// </summary>
        public bool IsCountdown { get => _state == State.CountdownToStart; }
        /// <summary>
        /// Returns true if the game is over
        /// </summary>
        public bool IsGameOver { get => _state == State.GameOver; }
        /// <summary>
        /// The value of countdown timer
        /// </summary>
        public float CountdownTimerValue { get => _countdownToStartTimer; }
        /// <summary>
        /// Returns normalized value of GamePlaying timer
        /// </summary>
        public float GamePlayingTimerValueNormalized { get => _gamePlayingTimer / gamePlayingTimerValue; }


        private void Awake()
        {
            _countdownToStartTimer = countdownToStartTimerValue;
            _gamePlayingTimer = gamePlayingTimerValue;

            _state = State.WaitingToStart;
            OnStateChanged?.Invoke(this, System.EventArgs.Empty);

            Debug.Log(_state);

            Instance = this;
        }
        private void Start()
        {
            GameInputManager.Instance.OnPauseAction += GameInputManager_OnPauseAction;
            GameInputManager.Instance.OnInteractAction += GameInputManager_OnInteractAction;
        }
        private void Update()
        {
            switch (_state)
            {
                case State.WaitingToStart:
                    break;

                case State.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;
                    if (_countdownToStartTimer <= 0f)
                    {
                        _state = State.GamePlaying;
                        OnStateChanged?.Invoke(this, System.EventArgs.Empty);

                        Debug.Log(_state);
                    }

                    break;

                case State.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer <= 0f)
                    {
                        _state = State.GameOver;
                        OnStateChanged?.Invoke(this, System.EventArgs.Empty);

                        Debug.Log(_state);
                    }

                    break;

                case State.GameOver:
                    break;
            }
        }


        private void GameInputManager_OnPauseAction(object sender, System.EventArgs e)
        {
            TogglePauseGame();
        }
        private void GameInputManager_OnInteractAction(object sender, System.EventArgs e)
        {
            if (_state == State.WaitingToStart)
            {
                _state = State.CountdownToStart;
                OnStateChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }


        public void TogglePauseGame()
        {
            _isGamePaused = !_isGamePaused;

            if (_isGamePaused)
            {
                Time.timeScale = 0f;
                OnGamePaused?.Invoke(this, System.EventArgs.Empty);
            }
            else
            {
                Time.timeScale = 1f;
                OnGameUnpaused?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}