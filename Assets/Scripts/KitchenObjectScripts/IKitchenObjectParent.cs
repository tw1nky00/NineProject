using UnityEngine;

namespace Scripts.KitchenObjectScripts
{
    /// <summary>
    /// An interface of objects that a kitchen object can belong to
    /// </summary>
    public interface IKitchenObjectParent
    {
        /// <summary>
        /// The kitchen object that belongs to this object
        /// </summary>
        public KitchenObject KitchenObject { get; set; }
        /// <summary>
        /// A transform of the point where the kitchen object should be
        /// </summary>
        public Transform KitchenObjectFollowTransform { get; }
        /// <summary>
        /// Does this object have a kitchen object?
        /// </summary>
        public bool HasKitchenObject { get; }

        /// <summary>
        /// Removes the reference to the kitchen object on the counter
        /// (use when getting or removing the kitchen object so the counter should be empty)
        /// </summary>
        public void ClearKitchenObject();
    }
}
