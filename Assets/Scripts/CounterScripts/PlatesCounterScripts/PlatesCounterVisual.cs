using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    /// <summary>
    /// Component of PlatesCounter this visual belongs to
    /// </summary>
    [SerializeField] private PlatesCounter platesCounter;
    /// <summary>
    /// A reference to the point KitchenObjects must be placed
    /// </summary>
    [SerializeField] private Transform counterTopPoint;
    /// <summary>
    /// Prefab with plate visual
    /// </summary>
    [SerializeField] private GameObject plateVisualPrefab;

    private List<GameObject> _plateVisualGameObjectList;


    private void Awake()
    {
        _plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        GameObject plateSpawned = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateSpawned.transform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);

        _plateVisualGameObjectList.Add(plateSpawned);
    }
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateToRemove = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
        _plateVisualGameObjectList.Remove(plateToRemove);
        Destroy(plateToRemove);
    }
}
