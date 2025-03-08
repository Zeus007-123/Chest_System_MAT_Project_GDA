using UnityEngine;
using ChestCore.MVC;

namespace ScriptableObjects
{

    /// <summary>
    /// ChestSO class. (Used to create ScriptableObjects)
    /// Used to Create Chest Configurations.
    /// <summary>

    [CreateAssetMenu(fileName = "ChestSO", menuName = "ScriptableObjects/ChestSO")]
    
    public class ChestSO : ScriptableObject
    {
        public Vector2Int CHEST_COINS_RANGE;
        public Vector2Int CHEST_GEMS_RANGE;
        public int MAX_UNLOCK_TIME;
        public int MAX_GEMS_TO_UNLOCK;
        public Sprite CHEST_SPRITE;
        public ChestType CHEST_TYPE;
    }
}
