using UnityEngine;

namespace Scripts.CounterScripts.ContainerCounterScripts
{
    /// <summary>
    /// The component which contains logic for the visual of container counter
    /// </summary>
    public class ContainerCounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";


        /// <summary>
        /// The reference to the container counter which this visual belongs to
        /// </summary>
        [SerializeField] private ContainerCounter containerCounter;

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {
            // Subscribing on an event when grabbing a new object
            containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        }


        /// <summary>
        /// Occurs when player grabs a new KitchenObject
        /// </summary>
        private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}