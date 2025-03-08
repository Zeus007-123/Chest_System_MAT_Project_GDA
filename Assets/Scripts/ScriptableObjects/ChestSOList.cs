using UnityEngine;

namespace ScriptableObjects
{

     /// <summary>
    /// ChestSOList class. Used to create Nested ScriptableObject for Chest Configurations.
   /// <summary>

    [CreateAssetMenu(fileName = "ChestSOList", menuName = "ScriptableObjects/ChestSOList")]
    public class ChestSOList : ScriptableObject
    {
        public ChestSO[] chestScriptableObjects;
    }
}