using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UIScripts
{
    public class ProgressBarUI : MonoBehaviour
    {
        /// <summary>
        /// Counter which the canvas belongs to
        /// </summary>

        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;

        private IHasProgress hasProgress;

        private void Start()
        {
            if (!hasProgressGameObject.TryGetComponent<IHasProgress>(out hasProgress))
            {
                Debug.LogError($"GameObject {hasProgressGameObject} does not have a component that implements IHasProgress");
            }


            hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

            barImage.fillAmount = 0f;

            Hide();
        }


        private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;

            if (e.progressNormalized == 0f || e.progressNormalized == 1f)
            {
                Hide();
            }
            else
            {
                Show();
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
