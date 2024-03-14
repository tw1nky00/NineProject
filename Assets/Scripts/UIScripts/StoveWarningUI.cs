using Scripts.CounterScripts.StoveCounterScripts;
using UnityEngine;

namespace Scripts.UIScripts
{
    public class StoveWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

            Hide();
        }


        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            var showProgressValue = 0.5f;
            var shouldShow = stoveCounter.IsFried && e.progressNormalized >= showProgressValue;

            if (shouldShow)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}