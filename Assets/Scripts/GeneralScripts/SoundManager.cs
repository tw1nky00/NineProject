using Scripts.CounterScripts.CuttingCounterScripts;
using Scripts.CounterScripts.DeliveryCounterScripts;
using Scripts.CounterScripts.TrashCounterScripts;
using Scripts.CounterScripts;
using Scripts.GeneralSO;
using Scripts.PlayerScripts;
using UnityEngine;

namespace Scripts.GeneralScripts
{
    /// <summary>
    /// Responsible for most of the sfx
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";


        /// <summary>
        /// The only instance of SoundManager
        /// </summary>
        public static SoundManager Instance { get; private set; }


        [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;


        /// <summary>
        /// The volume of sfx
        /// </summary>
        public float Volume { get; private set; } = 1f;


        private void Awake()
        {
            Instance = this;

            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
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
        /// <param name="volume">The volumeMultiplier of the sound</param>
        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
        /// <summary>
        /// Plays a random sound effect from an array
        /// </summary>
        /// <param name="audioClipsArray">An array of sfx, one of which is going to be played</param>
        /// <param name="position">A position, the sound is played from</param>
        /// <param name="volumeMultiplier">The volumeMultiplier of the sound</param>
        private void PlaySound(AudioClip[] audioClipsArray, Vector3 position, float volumeMultiplier = 1f)
        {
            PlaySound(audioClipsArray[Random.Range(0, audioClipsArray.Length - 1)], position, Volume * volumeMultiplier);
        }

        /// <summary>
        /// Plays footsteps when the player is walking
        /// </summary>
        /// <param name="position">Position, where the sound should be played from</param>
        public void PlayFootsteps(Vector3 position)
        {
            PlaySound(audioClipReferencesSO.Footstep, position);
        }
        /// <summary>
        /// Increases the volumeMultiplier in 10%. If the volumeMultiplier has got higher than 100%, it's set to 0
        /// </summary>
        public void ChangeVolume()
        {
            Volume += 0.1f;

            if (Volume > 1f)
            {
                Volume = 0f;
            }

            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, Volume);
            PlayerPrefs.Save();
        }
    }
}