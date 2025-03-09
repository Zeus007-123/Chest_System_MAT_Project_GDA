using UnityEngine;

namespace ChestSlot
{
    /*
        Enum for ChestSlot State.
    */
    public enum ChestSlotType
    {
        EMPTY,
        OCCUPIED
    }

    /*
        ChestSlotController class. Attached with Gameobject to display ChestSlot State.
    */
    public class ChestSlotController : MonoBehaviour
    {
        public ChestSlotType CHEST_SLOT_STATUS;
        public GameObject EMPTY_TEXT;

        /*
            Switches EMPTY Text ON / OFF Based on CHEST_SLOT_STATUS.
        */
        private void Update()
        {
            if (CHEST_SLOT_STATUS == ChestSlotType.EMPTY && !EMPTY_TEXT.activeInHierarchy)
            {
                EMPTY_TEXT.SetActive(true);
            }
            else if (CHEST_SLOT_STATUS == ChestSlotType.OCCUPIED && EMPTY_TEXT.activeInHierarchy)
            {
                EMPTY_TEXT.SetActive(false);
            }
        }

    }

}