using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccessed += DeliveryManager_OnRecipeSuccessed;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }


    private void DeliveryManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipReferencesSO.DeliverySuccess, DeliveryCounter.Instance.transform.position);
    }
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipReferencesSO.DeliveryFail, DeliveryCounter.Instance.transform.position);
    }


    /// <summary>
    /// Plays a sound effect
    /// </summary>
    /// <param name="audioClip">A sound effect to play</param>
    /// <param name="position">A position, the sound is played from</param>
    /// <param name="volume">The volume of the sound</param>
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    /// <summary>
    /// Plays a random sound effect from an array
    /// </summary>
    /// <param name="audioClipsArray">An array of sfx, one of which is going to be played</param>
    /// <param name="position">A position, the sound is played from</param>
    /// <param name="volume">The volume of the sound</param>
    private void PlaySound(AudioClip[] audioClipsArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipsArray[Random.Range(0, audioClipsArray.Length - 1)], position, volume);
    }
}
