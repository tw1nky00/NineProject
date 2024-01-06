using UnityEngine;

/// <summary>
/// The component of PlayerAnimator is responsible for player's animations
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    /// <summary>
    /// A reference to the Animator that is responsible for player's animations 
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// A reference to the player
    /// </summary>
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = transform.GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING, _playerController.IsWalking);
    }
}
