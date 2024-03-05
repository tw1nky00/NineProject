using UnityEngine;

namespace Scripts.KitchenObjectScripts
{
    /// <summary>
    /// A type of the Scriptable Object of kitchen objects(not the component!)
    /// </summary>
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string objectName;

        public GameObject Prefab { get => prefab; }
        public Sprite Sprite { get => sprite; }
        public string ObjectName { get => objectName; }
    }

}