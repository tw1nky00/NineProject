using Scripts.GeneralScripts;
using UnityEngine;

namespace Scripts.PlayerScripts
{
    /// <summary>
    /// Responsible for player's sounds
    /// </summary>
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private float footstepTimerMax = 0.1f;

        private PlayerController _playerController;

        private float _footstepTimer;


        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }
        private void Update()
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0f)
            {
                _footstepTimer = footstepTimerMax;

                if (_playerController.IsWalking)
                {
                    SoundManager.Instance.PlayFootsteps(_playerController.transform.position);
                }
            }
        }
    }
}
