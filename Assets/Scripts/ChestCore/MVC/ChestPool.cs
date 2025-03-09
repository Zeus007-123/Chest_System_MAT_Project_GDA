using UnityEngine;

namespace ChestCore.MVC
{

    /*
        ObjectPool Class for Chest. Handles Pool Generation, Get and Return Item Functionality. 
    */
    public class ChestPool
    {
        private ChestView[] chestPool;

        /*
            Constructor to Generate Pool of Chest Gameobjects. 
            Takes poolCount, Prefab and Parent Transform as parameters.
            Also implements Connections between ChestModel, ChestView & ChestController.
        */
        public ChestPool(int poolCount, ChestView chestPrefab, Transform parentTF)
        {
            chestPool = new ChestView[poolCount];
            for (int i = 0; i < poolCount; i++)
            {
                ChestView chestView = GameObject.Instantiate<ChestView>(chestPrefab, parentTF);
                ChestModel chestModel = new ChestModel();
                ChestController chestController = new ChestController(chestModel, chestView);
                chestModel.SetChestController(chestController);
                chestView.SetChestController(chestController);
                chestView.gameObject.SetActive(false);
                chestPool[i] = chestView;
            }
        }

        /*
            Fetches the ChestItem if it isn't active. If no objects are available, returns Null.
            Returning NULL acts as Slots Full Mechanism.
        */
        public ChestView GetChestItem()
        {
            for (int i = 0; i < chestPool.Length; i++)
            {
                if (!chestPool[i].gameObject.activeInHierarchy)
                    return chestPool[i];
            }
            return null;
        }

        /*
            Returns the ChestView reference back to Pool. Deactivates the GameObject.
        */
        public void ReturnChestItem(ChestView chestView)
        {
            chestView.gameObject.SetActive(false);
        }
    }

}
