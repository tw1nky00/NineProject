using Cinemachine;
using UnityEngine;

/// <summary>
/// The component of CinemachineCameraController which controls shaking virtual cameras
/// </summary>
public class CinemachineCameraController : MonoBehaviour
{
    /// <summary>
    /// The instance of the player
    /// </summary>
    [SerializeField] private PlayerController _player;
    /// <summary>
    /// A referecnce to the virtual cam that is shaking when it is active 
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void Update()
    {
        _virtualCamera.gameObject.SetActive(_player.IsWalking);
    }
}
