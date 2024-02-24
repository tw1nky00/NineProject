using UnityEngine;

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


    [SerializeField] private float waitingToStartTimerValue = 5f;
    [SerializeField] private float countdownToStartTimerValue = 4f;
    [SerializeField] private float gamePlayingTimerValue = 10f;


    private State _state;

    private float _waitingToStartTimer;
    private float _countdownToStartTimer;
    private float _gamePlayingTimer;


    /// <summary>
    /// Returns true if the game is being played (not being prepared to play or gameover)  
    /// </summary>
    public bool IsGamePlaying { get => _state == State.GamePlaying; }
    /// <summary>
    /// Returns true if before the game timer is counting down
    /// </summary>
    public bool IsCountdown { get => _state == State.CountdownToStart; }
    public float CountdownTimerValue { get => _countdownToStartTimer; }


    private void Awake()
    {
        _waitingToStartTimer = waitingToStartTimerValue;
        _countdownToStartTimer = countdownToStartTimerValue;
        _gamePlayingTimer = gamePlayingTimerValue;

        _state = State.WaitingToStart;
        OnStateChanged?.Invoke(this, System.EventArgs.Empty);

        Debug.Log(_state);

        Instance = this;
    }
    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer <= 0f)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, System.EventArgs.Empty);

                    Debug.Log(_state);
                }

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

}
