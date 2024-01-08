using UnityEngine;

/// <summary>
/// The component that is responsible for making the object look at the camera
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    /// <summary>
    /// Do you want a progress bar to fill form left to right or from right to left?
    /// </summary>
    private enum LookAtMode
    {
        Normal,
        Inverted,
        Forward,
        ForwardInverted
    }


    /// <summary>
    /// LookAt mode
    /// </summary>
    [SerializeField] private LookAtMode lookAtMode;


    private void LateUpdate()
    {
        switch (lookAtMode)
        {
            case LookAtMode.Normal:
                transform.LookAt(Camera.main.transform);
                break;
            case LookAtMode.Inverted:
                Vector3 directionFromCamera = transform.position - Camera.main.transform.forward;
                transform.LookAt(transform.position + directionFromCamera);
                break;
            case LookAtMode.Forward:
                transform.forward = Camera.main.transform.forward;
                break;
            case LookAtMode.ForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
