using UnityEngine;

namespace Scripts.GeneralSO
{
    /// <summary>
    /// Contains the in-game sounds (AudioClip) 
    /// </summary>
    [CreateAssetMenu()]
    public class AudioClipReferencesSO : ScriptableObject
    {
        [SerializeField] private AudioClip[] chop;
        [SerializeField] private AudioClip[] deliverySuccess;
        [SerializeField] private AudioClip[] deliveryFail;
        [SerializeField] private AudioClip[] footstep;
        [SerializeField] private AudioClip[] objectDrop;
        [SerializeField] private AudioClip[] objectPickUp;
        [SerializeField] private AudioClip[] trash;
        [SerializeField] private AudioClip[] warning;
        [SerializeField] private AudioClip stoveSizzle;

        public AudioClip[] Chop { get => chop; }
        public AudioClip[] DeliverySuccess { get => deliverySuccess; }
        public AudioClip[] DeliveryFail { get => deliveryFail; }
        public AudioClip[] Footstep { get => footstep; }
        public AudioClip[] ObjectDrop { get => objectDrop; }
        public AudioClip[] ObjectPickUp { get => objectPickUp; }
        public AudioClip[] Trash { get => trash; }
        public AudioClip[] Warning { get => warning; }
        public AudioClip StoveSizzle { get => stoveSizzle; }
    }
}