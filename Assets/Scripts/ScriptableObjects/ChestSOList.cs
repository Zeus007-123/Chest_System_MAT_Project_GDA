using UnityEngine;

namespace ScriptableObjects
{

    /*
        ChestSOList class. Used to create Nested ScriptableObject for Chest Configurations.
    */

    [CreateAssetMenu(fileName = "ChestSOList", menuName = "ScriptableObjects/ChestSOList")]
    public class ChestSOList : ScriptableObject
    {
        public ChestSO[] chestScriptableObjects;
    }
}