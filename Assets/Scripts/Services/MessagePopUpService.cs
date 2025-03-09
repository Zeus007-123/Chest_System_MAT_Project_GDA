using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;
using ChestCore.MVC;
using ChestCore.StateMachine;

namespace Services
{

    /*
        MessagePopUpService MonoSingleton Class. Handles All the logic & functionality of PopupUI.
    */
    public class MessagePopUpService : GenericMonoSingleton<MessagePopUpService>
    {
        [SerializeField] GameObject PopUpUI;
        [SerializeField] Button OkButton;
        [SerializeField] Button UnlockButton;
        [SerializeField] Button QueueButton;
        [SerializeField] Button CancelButton;
        [SerializeField] TextMeshProUGUI unlockButtonText;
        [SerializeField] TextMeshProUGUI alertText;
        [SerializeField] TextMeshProUGUI queueFullText;
        [SerializeField] TextMeshProUGUI unlockText;
        [SerializeField] TextMeshProUGUI chestContentText;
        [SerializeField] TextMeshProUGUI chestTitleText;

        /*
            Subscribing to onSlotsFull, OnChestIsSpawned, onChestIsClicked, onNotEnoughCoinsGems Event.
        */
        private void OnEnable()
        {
            EventService.Instance.onSlotsFull += OnSlotsFull;
            EventService.Instance.onChestIsSpawned += OnChestSpawnedIsSuccesful;
            EventService.Instance.onChestIsClicked += OnChestButtonIsClicked;
            EventService.Instance.onNotEnoughCoinsGems += DisplayNotEnoughResources;
        }

        /*
            OnQueueButtonIsClicked Method. Gets Executed when Queue Button is Clicked.
        */
        public void OnQueueButtonIsClicked(GameObject ChestGameObject)
        {
            ClearPopUp();
            if (ChestCoreService.Instance.isChestQueueingPosssible())
            {
                ChestCoreService.Instance.AddInQueue(ChestGameObject);
            }
            else
            {
                PopUpUI.SetActive(true);
                queueFullText.text = "WAITING QUEUE IS FULL. ONLY 2 QUEUES POSSIBLE.";
                OkButton.gameObject.SetActive(true);
                queueFullText.gameObject.SetActive(true);
            }
        }

        /*
            OnUnlockButtonIsClicked Method. Gets Executed when Unlock Button is Clicked.    
        */
        public void OnUnlockButtonIsClicked(GameObject ChestGameObject, int GEMS_TO_UNLOCK)
        {
            SoundService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            if (UIService.Instance.GEM_COUNT >= GEMS_TO_UNLOCK)
            {
                ChestCoreService.Instance.UnlockChest(ChestGameObject);
                EventService.Instance.InvokeCollectCoinsGemsEvent(0, -GEMS_TO_UNLOCK);
                ClearPopUp();
            }
            else
            {
                ClearPopUp();
                DisplayNotEnoughResources();
            }
        }

        /*
            Clears the PopupUI & Disables all Gameobjects.
        */
        public void ClearPopUp()
        {
            SoundService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            PopUpUI.SetActive(false);
            QueueButton.onClick.RemoveAllListeners();
            QueueButton.gameObject.SetActive(false);
            UnlockButton.onClick.RemoveAllListeners();
            UnlockButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            OkButton.gameObject.SetActive(false);
            alertText.gameObject.SetActive(false);
            chestContentText.gameObject.SetActive(false);
        }

        /*
            Displays Slots are Full Popup.
        */
        public void OnSlotsFull()
        {
            PopUpUI.SetActive(true);
            OkButton.gameObject.SetActive(true);
            alertText.text = "ALL SLOTS ARE FULL. NO EXTRA SLOT AVAILABLE.";
            alertText.gameObject.SetActive(true);
        }

        /*
            Displays Not Enough Coins / Gems Popup.
        */
        public void DisplayNotEnoughResources()
        {
            PopUpUI.SetActive(true);
            OkButton.gameObject.SetActive(true);
            alertText.text = "NOT ENOUGH COINS / GEMS. TRY AGAIN LATER.";
            alertText.gameObject.SetActive(true);
        }

        /*
            OnChestButtonIsClicked Method. Gets Executed whenever Chest is Clicked.
            COINS, GEMS, GEMS_TO_UNLOCK, CHEST_STATE & ChestType along with GameObject are Added to Handle All States.
        */
        public void OnChestButtonIsClicked(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState CHEST_STATE, ChestType chestType, GameObject chestObject)
        {
            SoundService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            if (CHEST_STATE == ChestState.LOCKED)
            {
                ChestLockedStatePopUp(chestObject);
            }
            else if (CHEST_STATE == ChestState.UNLOCKING)
            {
                ChestUnlockingStatePopUp(GEMS_TO_UNLOCK, chestObject);
            }
            else if (CHEST_STATE == ChestState.UNLOCKED)
            {
                ChestUnlockedStatePopUp(COINS, GEMS, chestType);
            }
        }

        /*
            Displays Popup UI when Chest is Clicked in LOCKED State.
            Enables Queue & Cancel Buttons.
        */
        private void ChestLockedStatePopUp(GameObject chestObject)
        {
            PopUpUI.SetActive(true);
            alertText.text = "CHEST IS LOCKED. QUEUE UNLOCKING CHEST ?";
            alertText.gameObject.SetActive(true);
            QueueButton.onClick.AddListener(() => { OnQueueButtonIsClicked(chestObject); });
            QueueButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
        }

        /*
            Displays Popup UI when Chest is Clicked in UNLOCKING State.
            Enables Unlock Now & Cancel Buttons.
        */
        private void ChestUnlockingStatePopUp(int GEMS_TO_UNLOCK, GameObject chestObject)
        {
            PopUpUI.SetActive(true);
            unlockText.text = "UNLOCK CHEST FOR " + GEMS_TO_UNLOCK + " GEMS ?";
            unlockText.gameObject.SetActive(true);
            unlockButtonText.text = GEMS_TO_UNLOCK.ToString();
            UnlockButton.onClick.AddListener(() => { OnUnlockButtonIsClicked(chestObject, GEMS_TO_UNLOCK); });
            UnlockButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
        }

        /*
            Displays Popup UI when Chest is Clicked in Unlocked State.
            Displays COINS, GEMS obtained.
        */
        private void ChestUnlockedStatePopUp(int COINS, int GEMS, ChestType chestType)
        {
            chestTitleText.text = GetChestTypeText(chestType) + " CHEST OPENED !!";
            chestContentText.text = "COINS FOUND : " + COINS + "\nGEMS FOUND  :   " + GEMS;
            chestContentText.text = chestContentText.text.Replace("\\n", "\n");
            PopUpUI.SetActive(true);
            chestContentText.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(true);
            EventService.Instance.InvokeCollectCoinsGemsEvent(COINS, GEMS);
        }

        /*
            Gets the ChestType Text by taking ChestType as Input.
        */
        private string GetChestTypeText(ChestType chestType)
        {
            string ChestTypeText = "";
            if (chestType == ChestType.COMMON)
            {
                ChestTypeText = "COMMON";
            }
            else if (chestType == ChestType.RARE)
            {
                ChestTypeText = "RARE";
            }
            else if (chestType == ChestType.EPIC)
            {
                ChestTypeText = "EPIC";
            }
            else if (chestType == ChestType.LEGENDARY)
            {
                ChestTypeText = "LEGENDARY";
            }
            return ChestTypeText;
        }

        /*
            Displays Popup when Chest is Spawned Successfully.
            Displays Chest Type & Coin , gem Range.
        */
        public void OnChestSpawnedIsSuccesful(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType)
        {
            string ChestTypeText = GetChestTypeText(chestType);
            chestTitleText.text = ChestTypeText + " CHEST FOUND !!";
            chestContentText.text = "COINS RANGE : " + COIN_RANGE.x + " - " + COIN_RANGE.y + "\nGEMS RANGE  :   " + GEM_RANGE.x + " - " + GEM_RANGE.y;
            chestContentText.text = chestContentText.text.Replace("\\n", "\n");
            PopUpUI.SetActive(true);
            chestContentText.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(true);
        }

        /*
            Unsubscribing to onSlotsFull, onChestIsSpawned, onChestIsClicked, onNotEnoughCoinsGems Event.
        */
        private void OnDisable()
        {
            EventService.Instance.onSlotsFull -= OnSlotsFull;
            EventService.Instance.onChestIsSpawned -= OnChestSpawnedIsSuccesful;
            EventService.Instance.onChestIsClicked -= OnChestButtonIsClicked;
            EventService.Instance.onNotEnoughCoinsGems -= DisplayNotEnoughResources;

        }
    }

}