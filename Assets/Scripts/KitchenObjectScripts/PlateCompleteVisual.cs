using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }


    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;


    private void Start()
    {
        plate.OnIngridientAdded += Plate_OnIngridientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void Plate_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (e.ingridient == kitchenObjectSOGameObject.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
