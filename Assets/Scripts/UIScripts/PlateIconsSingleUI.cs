using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for a single icon
/// </summary>
public class PlateIconsSingleUI : MonoBehaviour
{
    /// <summary>
    /// A reference to an image of this icon
    /// </summary>
    [SerializeField] private Image image;


    /// <summary>
    /// Sets an image for this icon
    /// </summary>
    /// <param name="kitchenObjectSO">A KitchenObjectSO that needs to be shown in the icon</param>
    public void SetImage(KitchenObjectSO kitchenObjectSO)
    {
        this.image.sprite = kitchenObjectSO.Sprite;
    }
}
