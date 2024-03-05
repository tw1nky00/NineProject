using Scripts.KitchenObjectScripts;
using UnityEngine;

namespace Scripts.UIScripts
{
    /// <summary>
    /// Responsible for displaying the plate icons
    /// </summary>
    public class PlateIconsUI : MonoBehaviour
    {
        /// <summary>
        /// The reference to the plate which this UI belongs to
        /// </summary>
        [SerializeField] private PlateKitchenObject plate;
        /// <summary>
        /// A reference to the icon template (must be already on the canvas)
        /// </summary>
        [SerializeField] private GameObject iconTemplate;


        private void Awake()
        {
            this.iconTemplate.SetActive(false); // Disabling the IconTemplate
        }
        private void Start()
        {
            this.plate.OnIngridientAdded += Plate_OnIngridientAdded;
        }


        private void Plate_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in this.transform) // Deleting all the icons on this canvas...
            {
                if (child.gameObject == iconTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (KitchenObjectSO ingridient in this.plate.IngridiendsList) // Spawning all the icons after deletion
            {
                GameObject iconGameObject = Instantiate(this.iconTemplate, this.transform);
                // The icon is positioned automatically owing to a Grid Layout Group

                iconGameObject.SetActive(true); // Enabling currently spawned icon

                iconGameObject.GetComponent<PlateIconsSingleUI>().SetImage(ingridient); // Setting an image for current iconGameObject
            }
        }
    }
}
