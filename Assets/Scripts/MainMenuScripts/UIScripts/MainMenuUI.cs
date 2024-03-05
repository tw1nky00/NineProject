using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MainMenuScripts.UIScripts
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;


        private void Awake()
        {
            playButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.Game);
            });

            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            Time.timeScale = 1f;
        }
    }
}
