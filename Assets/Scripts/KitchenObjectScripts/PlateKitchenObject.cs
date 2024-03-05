using System.Collections.Generic;
using UnityEngine;

namespace Scripts.KitchenObjectScripts
{
    /// <summary>
    /// The component of PlateKitchenObject represents plates
    /// </summary>
    public class PlateKitchenObject : KitchenObject
    {
        /// <summary>
        /// Occurs when a new ingridient added on the plate
        /// </summary>
        public event System.EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
        /// <summary>
        /// Contains information which KitchenObjectSo was added
        /// </summary>
        public class OnIngridientAddedEventArgs : System.EventArgs
        {
            public KitchenObjectSO ingridient;
        }


        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

        /// <summary>
        /// The list which contains an information obut the ingridients placed on this plate
        /// </summary>
        private List<KitchenObjectSO> _ingridiendsList;



        private void Awake()
        {
            _ingridiendsList = new List<KitchenObjectSO>();
        }


        /// <summary>
        /// Tries to add a new ingridient to this plate
        /// </summary>
        /// <param name="ingridient">An ingridient to add</param>
        /// <returns>True if the addition was successful, otherwise - False</returns>
        /// <remarks>The only ingridient of each KitchenObjectSO can be added</remarks>
        public bool TryAddIngridient(KitchenObjectSO ingridient)
        {
            if (_ingridiendsList.Contains(ingridient) || !validKitchenObjectSOList.Contains(ingridient))
            {
                return false;
            }

            _ingridiendsList.Add(ingridient);

            OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs
            {
                ingridient = ingridient
            });

            return true;
        }


        /// <summary>
        /// A list of ingridients added to the plate
        /// </summary>
        public List<KitchenObjectSO> IngridiendsList { get => _ingridiendsList; }
    }
}
