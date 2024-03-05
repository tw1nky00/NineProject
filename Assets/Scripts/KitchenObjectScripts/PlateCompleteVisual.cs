using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.KitchenObjectScripts
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        /// <summary>
        /// The struct for me to get the info about the gameobject and its KitchenObjectSO easier
        /// </summary>
        [Serializable]
        public struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }


        /// <summary>
        /// The reference to the plate which this visual belongs to
        /// </summary>
        [SerializeField] private PlateKitchenObject plate;
        /// <summary>
        /// The list of KitchenObjectSO_GameObject that i need to activate when corresponding ingridient is added
        /// </summary>
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;


        private void Start()
        {
            plate.OnIngridientAdded += Plate_OnIngridientAdded;

            foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
            {
                // Deactivating all the ingridients' visuals
                kitchenObjectSOGameObject.gameObject.SetActive(false);
            }
        }

        private void Plate_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
        {
            foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
            {
                if (e.ingridient == kitchenObjectSOGameObject.kitchenObjectSO)
                {
                    // Activating if the corresponding ingridient was added
                    kitchenObjectSOGameObject.gameObject.SetActive(true);
                }
            }
        }
    }
}