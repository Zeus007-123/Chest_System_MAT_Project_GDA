using UnityEngine;
using ScriptableObjects;
using Utilities;
using ChestCore.MVC;
using ChestCore.StateMachine;

namespace Services
{

    /*
        ChestCoreService MonoSingleton Class. Handles Creation of Chest GameObjects.
        Communicates with Other Services.
    */
    public class ChestCoreService : GenericMonoSingleton<ChestCoreService>
    {
        [SerializeField] ChestSOList chestConfigs;
        [SerializeField] ChestView ChestPrefab;
        [SerializeField] Transform ChestsParentTF;
        [SerializeField] int MAX_QUEUE_COUNT;
        private ChestPool chestPool;
        private ChestQueueingService chestQueueingService;

        private void Start()
        {
            chestPool = new ChestPool(4, ChestPrefab, ChestsParentTF);
            chestQueueingService = new ChestQueueingService(MAX_QUEUE_COUNT);
        }

        /*
            Fetches Chest Configuration & GameObject associated from the ChestPool.
            Also Sets View Attributes, Model Configuration & Resets State Machine.
        */
        public (GameObject, ChestSO) FetchChestFromPool()
        {
            ChestView chestObject = chestPool.GetChestItem();
            if (chestObject != null)
            {
                ChestSO chestConfig = FetchRandomChestConfiguration();
                chestObject.GetChestController().GetChestModel().SetChestConfiguration(chestConfig);
                chestObject.GetChestController().SetViewAttributes();
                chestObject.GetChestController().GetChestSM().ResetSM();
                return (chestObject.gameObject, chestConfig);
            }
            return (null, null);
        }

        /*
            Returns ChestScriptableObject from ChestScriptableObjectList which contains different ChestModel Configurations.
        */
        public ChestSO FetchRandomChestConfiguration()
        {
            int index = Random.Range(0, chestConfigs.chestScriptableObjects.Length);
            return chestConfigs.chestScriptableObjects[index];
        }

        /*
            Returns Chest GameObject to Pool. Disables the Chest GameObject.
        */
        public void ReturnChestToPool(ChestView chestView)
        {
            chestPool.ReturnChestItem(chestView);
        }

        /*
            Triggers PopUp & Uses Different Services to Handle Audio, Events, ChestSlots.
        */
        public void TriggerPopUp(ChestController chestController)
        {
            ChestStateMachine chestSM = chestController.GetChestSM();
            int COINS = chestController.GetChestModel().CHEST_COINS;
            int GEMS = chestController.GetChestModel().CHEST_GEMS;
            int GEMS_TO_UNLOCK = (int)chestController.GetChestModel().GEMS_TO_UNLOCK;
            ChestState CHEST_STATE = chestSM.currentChestStateEnum;
            ChestType chestType = chestController.GetChestModel().CHEST_TYPE;
            if (CHEST_STATE == ChestState.UNLOCKED)
            {
                SoundService.Instance.PlayAudio(SoundType.CHEST_UNLOCKED);
                ReturnChestToPool(chestController.GetChestView());
                ChestSlotService.Instance.ResetChestSlot(chestController.GetChestView().transform.parent);
            }
            EventService.Instance.InvokeChestIsClickedEvent(COINS, GEMS, GEMS_TO_UNLOCK, CHEST_STATE, chestType, chestController.GetChestView().gameObject);
        }

        /*
            Unlocks Chest Instantly. Sets the UNLOCK TIME to 0. Executed when UNLOCK NOW button is Clicked.
        */
        public void UnlockChest(GameObject ChestGameObject)
        {
            ChestController chestController = ChestGameObject.GetComponent<ChestView>().GetChestController();
            chestController.GetChestModel().UpdateUnlockTime(chestController.GetChestModel().UNLOCK_TIME);
        }

        /*
            Dequeues Chest From Waiting Queue : Which starts the Timer.
        */
        public void DequeueChestFromWaitingQueue()
        {
            chestQueueingService.DequeueChest();
        }

        /*
            Returns boolean specifying is it Possible to Add Chest to Waiting Queue.
        */
        public bool isChestQueueingPosssible()
        {
            return chestQueueingService.isChestQueueingPosssible();
        }

        /*
            Adds Chest GameObject to Waiting Queue.
            Also Starts Unlocking the First Chest if no chest is currently Unlocking.
        */
        public void AddInQueue(GameObject chestObject)
        {
            chestQueueingService.AddInQueue(chestObject);
        }

    }
}
