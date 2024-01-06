using UnityEngine;

/// <summary>
/// The component of SelectedCounterVisual is responsible for changing the visual of the counter
/// </summary>
public class SelectedCounterVisual : MonoBehaviour
{
    /// <summary>
    /// The counter where the SelectedCounterVisual component is
    /// </summary>
    [SerializeField] private BaseCounter baseCounter;
    /// <summary>
    /// The game object with counter's mesh
    /// </summary>
    [SerializeField] private GameObject[] visualGameObjectsArray;


    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }


    /// <summary>
    /// The method is called when the counter player is looking at has been changed
    /// (whether it's selected another counter or left the counter) 
    /// </summary>
    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
            Show();
        else
            Hide();
    }

    /// <summary>
    /// Shows the gameObject of selected counter visual
    /// </summary>
    private void Show()
    {
        foreach (var visualGameObject in visualGameObjectsArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Hides the gameObject of selected counter visual
    /// </summary>
    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjectsArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
