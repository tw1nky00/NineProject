using UnityEngine;

public interface IHasProgress
{
    /// <summary>
    /// Occurs when the cutting progress is changed
    /// </summary>
    public event System.EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    /// <summary>
    /// EventArgs that contain information about new cutting progress value
    /// </summary>
    public class OnProgressChangedEventArgs : System.EventArgs
    {
        public float progressNormalized;
    }
}
