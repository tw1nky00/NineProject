using UnityEngine;

/// <summary>
/// Responsible for most of the sfx
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// The only instance of SoundManager
    /// </summary>
    public static SoundManager Instance { get; private set; }


    [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccessed += DeliveryManager_OnRecipeSuccessed;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        BaseCounter.OnAnyKitchenObjectDropped += BaseCounter_OnAnyKitchenObjectPut;

        TrashCounter.OnAnyThrownAway += TrashCounter_OnAnyThrownAway;

        PlayerController.Instance.OnKitchenObjectPickedUp += PlayerController_OnKitchenObjectPickedUp;
    }

    
    private void TrashCounter_OnAnyThrownAway(object sender, System.EventArgs e)
    {
        var trashCounter = sender as TrashCounter;

        PlaySound(audioClipReferencesSO.Trash, trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyKitchenObjectPut(object sender, System.EventArgs e)
    {
        var counter = sender as BaseCounter;

        PlaySound(audioClipReferencesSO.ObjectDrop, counter.transform.position);
    }
    private void PlayerController_OnKitchenObjectPickedUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipReferencesSO.ObjectPickUp, PlayerController.Instance.transform.position);
    }
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        var cuttingCounter = sender as CuttingCounter;

        PlaySound(audioClipReferencesSO.Chop, cuttingCounter.transform.position);
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
        PlaySound(audioClipsArray[Random.Range(0, audioClipsArray.Length - 1)], position, volume);
    }

    /// <summary>
    /// Plays footsteps when the player is walking
    /// </summary>
    /// <param name="position">Position, where the sound should be played from</param>
    public void PlayFootsteps(Vector3 position)
    {
        PlaySound(audioClipReferencesSO.Footstep, position);
    }
}
