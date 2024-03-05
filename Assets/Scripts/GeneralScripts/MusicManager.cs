using UnityEngine;

namespace Scripts.GeneralScripts
{
    /// <summary>
    /// Responsible for Music management
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";


        /// <summary>
        /// The only inctance of a MusicManager
        /// </summary>
        public static MusicManager Instance { get; private set; }


        private AudioSource _audioSource;


        /// <summary>
        /// The volume of music
        /// </summary>
        public float Volume { get; private set; } = 1f;


        private void Awake()
        {
            Instance = this;

            _audioSource = GetComponent<AudioSource>();

            Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
            _audioSource.volume = Volume;
        }


        /// <summary>
        /// Increases the volumeMultiplier in 10%. If the volumeMultiplier has got higher than 100%, it's set to 0
        /// </summary>
        public void ChangeVolume()
        {
            Volume += 0.1f;

            if (Volume > 1f)
            {
                Volume = 0;
            }

            _audioSource.volume = Volume;

            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, Volume);
            PlayerPrefs.Save();
        }
    }
}
