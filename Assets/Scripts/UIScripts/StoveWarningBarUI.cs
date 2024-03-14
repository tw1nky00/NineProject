using Scripts.CounterScripts.StoveCounterScripts;
using UnityEngine;

namespace Scripts.UIScripts
{
    public class StoveWarningBarUI : MonoBehaviour
    {
        private const string IS_FLASHING = "IsFlashing";


        [SerializeField] private StoveCounter stoveCounter;

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

            _animator.SetBool(IS_FLASHING, false);
        }


        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            var showProgressValue = 0.5f;
            var shouldShow = stoveCounter.IsFried && e.progressNormalized >= showProgressValue;

            _animator.SetBool(IS_FLASHING, shouldShow);
        }
    }
}
